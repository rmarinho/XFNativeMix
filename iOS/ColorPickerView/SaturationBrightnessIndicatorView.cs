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
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

using CGPoint = System.Drawing.PointF;
using CGSize = System.Drawing.SizeF;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace AdvancedColorPicker
{
    internal class SaturationBrightnessIndicatorView : UIView
    {
        public SaturationBrightnessIndicatorView()
        {
            BackgroundColor = UIColor.Clear;
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            nfloat margins = 4;
            var drawRect = new CGRect(rect.X + margins, rect.Y + margins, rect.Width - margins * 2, rect.Height - margins * 2);

            var context = UIGraphics.GetCurrentContext();
            context.AddEllipseInRect(drawRect);
            context.AddEllipseInRect(drawRect.Inset(4, 4));
            context.SetFillColor(UIColor.Black.CGColor);
            context.SetStrokeColor(UIColor.White.CGColor);
            context.SetLineWidth(0.5f);
            context.ClosePath();
            context.SetShadow(new CGSize(1, 2), 4);
            context.DrawPath(CGPathDrawingMode.EOFillStroke);
        }
    }
}
