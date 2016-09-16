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
    internal class HuePickerView : UIView
    {
        public HuePickerView()
        {
        }

        public nfloat Hue { get; set; }

        public event EventHandler HueChanged;

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var step = 1f / 6f;
            var locations = new nfloat[]
            {
                0.0f,
                step * 1f,
                step * 2f,
                step * 3f,
                step * 4f,
                step * 5f,
                1.0f
            };
            var colors = new CGColor[]
            {
                UIColor.Red.CGColor,
                new CGColor(1, 0, 1, 1),
                UIColor.Blue.CGColor,
                new CGColor(0, 1, 1, 1),
                UIColor.Green.CGColor,
                new CGColor(1, 1, 0, 1),
                UIColor.Red.CGColor
            };

            using (var colorSpace = CGColorSpace.CreateDeviceRGB())
            using (var gradiend = new CGGradient(colorSpace, colors, locations))
            {
                var context = UIGraphics.GetCurrentContext();
                context.DrawLinearGradient(gradiend, new CGPoint(rect.Size.Width, 0), new CGPoint(0, 0), CGGradientDrawingOptions.DrawsBeforeStartLocation);
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

            var p = pos.X;
            var b = Frame.Size.Width;

            if (p < 0)
                Hue = 0;
            else if (p > b)
                Hue = 1;
            else
                Hue = p / b;

            OnHueChanged();
        }

        protected virtual void OnHueChanged()
        {
            var handler = HueChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
