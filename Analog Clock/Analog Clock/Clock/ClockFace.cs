using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Analog_Clock.Clock
{
    public class ClockFace
    {
        public ClockFace(Panel clockArea)
        {
            var p = 0;
            var centerPoint = new Point(clockArea.Width / 2, clockArea.Height / 2);

            var outerCircleShadow = new Ellipse
            {
                Width = clockArea.Width,
                Height = clockArea.Height,
                Stroke = new SolidColorBrush(Colors.Gray),
                StrokeThickness = 5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Canvas.SetLeft(outerCircleShadow, centerPoint.X - outerCircleShadow.Width / 2 + 6.5);
            Canvas.SetTop(outerCircleShadow, centerPoint.Y - outerCircleShadow.Height / 2 + 6.5);
            clockArea.Children.Add(outerCircleShadow);

            var outerCircle = new Ellipse
            {
                Width = clockArea.Width,
                Height = clockArea.Height,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Canvas.SetLeft(outerCircle, centerPoint.X - outerCircle.Width / 2 + 4.5);
            Canvas.SetTop(outerCircle, centerPoint.Y - outerCircle.Height / 2 + 4.5);
            clockArea.Children.Add(outerCircle);

            outerCircle.Fill = new LinearGradientBrush
            {
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Colors.White, Offset = 0 },
                    new GradientStop { Color = Colors.Gray, Offset = 0.5 },
                    new GradientStop { Color = Colors.White, Offset = 1 }
                }
            };

            var clockDigits = 3;
            var rad = centerPoint.X - 10.0;

            for (var i = 0.0; i < 360.0; i += 6)
            {
                var angle = i * Math.PI / 180;
                var x = (int)(centerPoint.X + rad * Math.Cos(angle));
                var y = (int)(centerPoint.Y + rad * Math.Sin(angle));

                int smallCircle;
                Color smallCircleColor;

                if (p % 5 == 0)
                {
                    smallCircle = 10;
                    smallCircleColor = Colors.Orange;
                }
                else
                {
                    smallCircle = 5;
                    smallCircleColor = Colors.Blue;
                }

                if (p % 15 == 0)
                {
                    var tb = new TextBlock
                    {
                        Text = clockDigits.ToString(),
                        FontSize = 24
                    };

                    Canvas.SetLeft(tb, x);
                    Canvas.SetTop(tb, y);

                    switch (clockDigits)
                    {
                        case 3:
                            Canvas.SetLeft(tb, x - 20);
                            Canvas.SetTop(tb, y - 10);
                            break;
                        case 6:
                            Canvas.SetLeft(tb, x);
                            Canvas.SetTop(tb, y - 30);
                            break;
                        case 9:
                            Canvas.SetLeft(tb, x + 15);
                            Canvas.SetTop(tb, y - 10);
                            break;
                        case 12:
                            Canvas.SetLeft(tb, x - 10);
                            Canvas.SetTop(tb, y + 5);
                            break;
                    }

                    clockArea.Children.Add(tb);
                    clockDigits += 3;
                }

                p++;

                var innerPoints = new Ellipse
                {
                    Width = smallCircle,
                    Height = smallCircle,
                    Stroke = new SolidColorBrush(smallCircleColor),
                    Fill = new SolidColorBrush(smallCircleColor),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Canvas.SetLeft(innerPoints, x);
                Canvas.SetTop(innerPoints, y);
                clockArea.Children.Add(innerPoints);
            }
        }
    }
}
