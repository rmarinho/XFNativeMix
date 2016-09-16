/*
 * This code is licensed under the terms of the MIT license
 *
 * Copyright (C) 2012 Yiannis Bourkelis & Matthew Leibowitz
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights 
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished
 * to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all 
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGPoint = System.Drawing.PointF;
using CGSize = System.Drawing.SizeF;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace AdvancedColorPicker
{
    internal class SaturationBrightnessPickerView : UIView
    {
        public SaturationBrightnessPickerView()
        {
        }

        public nfloat Hue { get; set; }

        public nfloat Saturation { get; set; }

        public nfloat Brightness { get; set; }

        public event EventHandler ColorPicked;

        public override void Draw(CGRect rect)
        {
            using (var colorSpace = CGColorSpace.CreateDeviceRGB())
            {
                var context = UIGraphics.GetCurrentContext();
                var gradLocations = new nfloat[] { 0.0f, 1.0f };

                var gradColors = new[] { UIColor.FromHSBA(Hue, 1, 1, 1).CGColor, new CGColor(1, 1, 1, 1) };
                using (var gradient = new CGGradient(colorSpace, gradColors, gradLocations))
                {
                    context.DrawLinearGradient(gradient, new CGPoint(rect.Size.Width, 0), new CGPoint(0, 0), CGGradientDrawingOptions.DrawsBeforeStartLocation);
                }

                gradColors = new[] { new CGColor(0, 0, 0, 0), new CGColor(0, 0, 0, 1) };
                using (var gradient = new CGGradient(colorSpace, gradColors, gradLocations))
                {
                    context.DrawLinearGradient(gradient, new CGPoint(0, 0), new CGPoint(0, rect.Size.Height), CGGradientDrawingOptions.DrawsBeforeStartLocation);
                }
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            HandleTouches(touches, evt);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            HandleTouches(touches, evt);
        }

        private void HandleTouches(NSSet touches, UIEvent evt)
        {
            var touch = (UITouch)evt.TouchesForView(this).AnyObject;
            var pos = touch.LocationInView(this);

            var w = Frame.Size.Width;
            var h = Frame.Size.Height;

            if (pos.X < 0)
                Saturation = 0;
            else if (pos.X > w)
                Saturation = 1;
            else
                Saturation = pos.X / w;

            if (pos.Y < 0)
                Brightness = 1;
            else if (pos.Y > h)
                Brightness = 0;
            else
                Brightness = 1 - (pos.Y / h);

            OnColorPicked();
        }

        protected virtual void OnColorPicked()
        {
            var handler = ColorPicked;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
