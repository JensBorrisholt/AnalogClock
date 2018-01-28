using System;
using Windows.UI.Xaml;

namespace Analog_Clock.ViewModel
{
    public sealed class ClockViewModel : ViewModelBase
    {
        public double HourAngle { get; private set; }
        public double MinuteAngle { get; private set; }
        public double SecondAngle { get; private set; }
        public bool SweepdHand { get; set; }

        public void ClockStart()
        {
            var clockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(SweepdHand ? 10 : 1000)
            };
            clockTimer.Tick += (s, e) => UpdateAngels();
            clockTimer.Start();
        }

        private void UpdateAngels()
        {
            var time = DateTime.Now;
            HourAngle = 30 * time.Hour + (SweepdHand ? 0.5 * time.Minute : 0);
            MinuteAngle = 6 * time.Minute + (SweepdHand ? 0.1 * time.Second + 0.00006 * time.Millisecond : 0);
            SecondAngle = 6 * time.Second + (SweepdHand ? 0.006 * time.Millisecond : 0);
            OnPropertyChanged(nameof(HourAngle), nameof(MinuteAngle), nameof(SecondAngle));
        }

        public ClockViewModel(bool sweepHand = false)
        {
            SweepdHand = sweepHand;
            UpdateAngels();
            ClockStart();
        }
    }
}
