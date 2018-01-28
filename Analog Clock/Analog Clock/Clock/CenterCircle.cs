using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Analog_Clock.Clock
{
    public class CenterCircle
    {
        public CenterCircle(Panel clockArea)
        {
            var centerCircle = new Ellipse
            {
                Width = 10,
                Height = 10,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            clockArea.Children.Add(centerCircle);
            Canvas.SetLeft(centerCircle, clockArea.Width / 2 - centerCircle.Width / 2);
            Canvas.SetTop(centerCircle, clockArea.Height / 2 - centerCircle.Height / 2);
            Canvas.SetZIndex(centerCircle, clockArea.Children.Count);
        }
    }
}
