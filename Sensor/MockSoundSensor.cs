using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor
{
    public class MockSoundSensor : ISoundSensor
    {
        public event EventHandler<short> PeakAcquired;

        public void Start(int sampleEveryMs)
        {

        }
    }
}
