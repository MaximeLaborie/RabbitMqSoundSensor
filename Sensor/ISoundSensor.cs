using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor
{
    public interface ISoundSensor
    {
        event EventHandler<short> PeakAcquired;

        void Start(int sampleEveryMs);
    }
}
