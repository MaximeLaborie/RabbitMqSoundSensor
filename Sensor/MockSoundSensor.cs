using System;
using System.Timers;

namespace Sensor
{
    public class MockSoundSensor : ISoundSensor
    {
        public event EventHandler<short> PeakAcquired;

        private Random _random;

        public void Start(int sampleEveryMs)
        {
            _random = new Random();
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTick);
            aTimer.Interval = sampleEveryMs;
            aTimer.Enabled = true;
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            short val = (short)_random.Next(0, short.MaxValue - 1000);
            PeakAcquired?.Invoke(this, val);
        }
    }
}