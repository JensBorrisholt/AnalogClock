using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Analog_Clock.Clock
{
    public class ClockHand
    {
        private readonly RotateTransform handRotate;

        public ClockHand(Panel clockArea, double handWidth, double pctHandLength, Color handColor)
        {
            var centerPoint = new Point(clockArea.Width / 2, clockArea.Height / 2);
            var handLength = centerPoint.X / 100 * pctHandLength;

            var handRectangle = new Rectangle
            {
                Width = handWidth,
                Height = handLength,
                Stroke = new SolidColorBrush(handColor),
                Fill = new SolidColorBrush(handColor),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            handRotate = new RotateTransform
            {
                Angle = 0,
                CenterX = Canvas.GetLeft(handRectangle),
                CenterY = handRectangle.Height + handWidth / 2,
            };

            handRectangle.RenderTransform = handRotate;
            Canvas.SetTop(handRectangle, centerPoint.Y - handLength);
            Canvas.SetLeft(handRectangle, centerPoint.X);
            clockArea.Children.Add(handRectangle);
        }

        public void Bind(object dataContext, string propertyName)
        {
            var binding = new Binding
            {
                Source = dataContext,
                Path = new PropertyPath(propertyName),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            BindingOperations.SetBinding(handRotate, RotateTransform.AngleProperty, binding);
        }
    }
}
