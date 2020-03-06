using NAudio.Wave;
using System;

namespace Sensor
{
    public class SoundSensor : ISoundSensor
    {
        private const int SampleRate = 8000;

        private WaveIn _waveIn;

        private short _peak;

        private int _sampleCount;

        private int _samplePerEvent;

        public event EventHandler<short> PeakAcquired;

        public void Start(int sampleEveryMs)
        {
            _samplePerEvent = (sampleEveryMs * SampleRate) / 1000;

            _waveIn = new WaveIn
            {
                DeviceNumber = 0,
                WaveFormat = new WaveFormat(SampleRate, 1) // 1 for mono
            };

            _waveIn.DataAvailable += OnDataAvailable;

            _waveIn.StartRecording();
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] buffer = e.Buffer;

            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                // samples are 16bit long, building them back from the buffer
                short sample = (short)((buffer[i + 1] << 8) | buffer[i]);
                UpdatePeak(sample);
            }
        }

        private void UpdatePeak(short value)
        {
            _sampleCount++;
            _peak = Math.Max(_peak, Math.Abs(value));
         
            if (_sampleCount >= _samplePerEvent)
            {
                PeakAcquired?.Invoke(this, _peak);
                _sampleCount = 0;
                _peak = 0;
            }
        }
    }
}