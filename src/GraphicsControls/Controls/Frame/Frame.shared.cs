﻿using System.Graphics;
using GraphicsControls.Extensions;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    [ContentProperty(nameof(Content))]
    public class Frame : GraphicsView
    {
        public static new readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(XColor), typeof(Frame), XColor.Default);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(XColor), typeof(Frame), XColor.Default);

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(Frame), default(CornerRadius));

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(Frame), true);

        public new XColor BackgroundColor
        {
            get { return (XColor)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public XColor BorderColor
        {
            get { return (XColor)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            var scale = width * 100 / (width - (CanvasDefaults.DefaultShadowBlur * 2)) / 100;
            Scale = scale;
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawShadow(canvas, dirtyRect);
            DrawBackground(canvas, dirtyRect);
            DrawBorder(canvas, dirtyRect);
        }

        void DrawBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor();

            var x = dirtyRect.X + CanvasDefaults.DefaultShadowBlur;
            var y = dirtyRect.Y + CanvasDefaults.DefaultShadowBlur;

            var height = dirtyRect.Height - CanvasDefaults.DefaultShadowBlur * 2;
            var width = dirtyRect.Width - CanvasDefaults.DefaultShadowBlur * 2;

            canvas.FillRoundedRectangle(x, y, width, height, (float)CornerRadius.TopLeft, (float)CornerRadius.TopRight, (float)CornerRadius.BottomLeft, (float)CornerRadius.BottomRight);

            canvas.RestoreState();
        }

        void DrawBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.StrokeColor = BorderColor.ToGraphicsColor();

            var x = dirtyRect.X + CanvasDefaults.DefaultShadowBlur;
            var y = dirtyRect.Y + CanvasDefaults.DefaultShadowBlur;

            var height = dirtyRect.Height - CanvasDefaults.DefaultShadowBlur * 2;
            var width = dirtyRect.Width - CanvasDefaults.DefaultShadowBlur * 2;

            canvas.DrawRoundedRectangle(x, y, width, height, (float)CornerRadius.TopLeft, (float)CornerRadius.TopRight, (float)CornerRadius.BottomLeft, (float)CornerRadius.BottomRight);

            canvas.RestoreState();
        }

        void DrawShadow(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor();

            if (HasShadow)
                canvas.SetShadow(SizeF.Zero, CanvasDefaults.DefaultShadowBlur, CanvasDefaults.DefaultShadowColor);

            var x = dirtyRect.X + CanvasDefaults.DefaultShadowBlur;
            var y = dirtyRect.Y + CanvasDefaults.DefaultShadowBlur;

            var height = dirtyRect.Height - (CanvasDefaults.DefaultShadowBlur * 2);
            var width = dirtyRect.Width - (CanvasDefaults.DefaultShadowBlur * 2);

            canvas.FillRoundedRectangle(x, y, width, height, (float)CornerRadius.TopLeft, (float)CornerRadius.TopRight, (float)CornerRadius.BottomLeft, (float)CornerRadius.BottomRight);

            canvas.RestoreState();
        }
    }
}