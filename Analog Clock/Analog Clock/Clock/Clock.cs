using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Analog_Clock.ViewModel;

namespace Analog_Clock.Clock
{
    public class Clock : DependencyObject
    {
        private Panel owner;

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == OwnerProperty)
                ((Clock)d).SetOwner(e.NewValue as Panel);
        }

        private void SetOwner(Panel newOwner)
        {
            if (newOwner == null)
                return;

            owner = newOwner;
            var clockArea = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            owner.DataContext = new ClockViewModel { SweepdHand = SweepdHand };
            owner.SizeChanged += (s, e) => BuildClock(clockArea);
            owner.Children.Add(clockArea);
        }

        private void BuildClock(Canvas clockArea)
        {
            clockArea.Children.Clear();
            var diameter = Math.Min(owner.ActualWidth, owner.ActualHeight) * 0.98;
            clockArea.Children.Clear();
            clockArea.Width = diameter;
            clockArea.Height = diameter;
            new ClockFace(clockArea);
            new CenterCircle(clockArea);
            new ClockHand(clockArea, 1, 92, Colors.Red).Bind(owner.DataContext, "SecondAngle");
            new ClockHand(clockArea, 1, 86, Colors.Black).Bind(owner.DataContext, "MinuteAngle");
            new ClockHand(clockArea, 4, 82, Colors.Black).Bind(owner.DataContext, "HourAngle");
        }

        public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register
        (
            nameof(Owner),
            typeof(Panel),
            typeof(Clock),
            new PropertyMetadata(null, PropertyChanged)
        );

        public static readonly DependencyProperty SweepdHandProperty = DependencyProperty.Register
        (
            nameof(SweepdHand),
            typeof(bool),
            typeof(Clock),
            null
        );

        public bool SweepdHand
        {
            get => (bool)GetValue(SweepdHandProperty);
            set => SetValue(SweepdHandProperty, value);
        }

        public Panel Owner
        {
            get => (Panel)GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }
    }
}
