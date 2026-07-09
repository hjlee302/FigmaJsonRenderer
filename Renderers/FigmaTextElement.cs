using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FigmaJsonRenderer.Renderers
{
    public sealed class FigmaTextElement : FrameworkElement
    {
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                InvalidateVisual();
            }
        }

        public double FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                InvalidateVisual();
            }
        }

        public Brush Foreground
        {
            get => _foreground;
            set
            {
                _foreground = value;
                InvalidateVisual();
            }
        }

        public FontWeight FontWeight
        {
            get => _fontWeight;
            set
            {
                _fontWeight = value;
                InvalidateVisual();
            }
        }

        public TextAlignment TextAlignment
        {
            get => _textAlignment;
            set
            {
                _textAlignment = value;
                InvalidateVisual();
            }
        }

        public VerticalAlignment TextVerticalAlignment
        {
            get => _textVerticalAlignment;
            set
            {
                _textVerticalAlignment = value;
                InvalidateVisual();
            }
        }

        public double? LineHeight
        {
            get => _lineHeight;
            set
            {
                _lineHeight = value;
                InvalidateVisual();
            }
        }

        private string _text = "";
        private double _fontSize = 16;
        private Brush _foreground = Brushes.Black;
        private FontWeight _fontWeight = FontWeights.Normal;
        private TextAlignment _textAlignment = TextAlignment.Left;
        private VerticalAlignment _textVerticalAlignment = VerticalAlignment.Top;
        private double? _lineHeight = null;

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (string.IsNullOrEmpty(Text))
            {
                return;
            }

            double pixelsPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;

            FormattedText formattedText = new(
                Text,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(
                    new FontFamily("Segoe UI"),
                    FontStyles.Normal,
                    FontWeight,
                    FontStretches.Normal),
                FontSize,
                Foreground,
                pixelsPerDip)
            {
                TextAlignment = TextAlignment,
                MaxTextWidth = Math.Max(0, ActualWidth),
                MaxTextHeight = double.PositiveInfinity
            };

            double contentHeight = LineHeight ?? formattedText.Height;
            double y = TextVerticalAlignment switch
            {
                VerticalAlignment.Center => (ActualHeight - contentHeight) / 2,
                VerticalAlignment.Bottom => ActualHeight - contentHeight,
                _ => 0
            };

            drawingContext.DrawText(formattedText, new Point(0, y));
        }
    }
}
