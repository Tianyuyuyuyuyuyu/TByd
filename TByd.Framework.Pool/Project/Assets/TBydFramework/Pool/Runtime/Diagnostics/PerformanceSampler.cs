using System;
using System.Collections.Generic;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    public class PerformanceSampler
    {
        private readonly Queue<double> _samples;
        private readonly int _maxSamples;
        private double _sum;
        private double _max;
        private double _min;
        
        public PerformanceSampler(int maxSamples = 100)
        {
            _maxSamples = maxSamples;
            _samples = new Queue<double>(_maxSamples);
            Reset();
        }

        public void AddSample(double value)
        {
            if (_samples.Count >= _maxSamples)
            {
                _sum -= _samples.Dequeue();
            }
            
            _samples.Enqueue(value);
            _sum += value;
            _max = Math.Max(_max, value);
            _min = Math.Min(_min, value);
        }

        public void Reset()
        {
            _samples.Clear();
            _sum = 0;
            _max = double.MinValue;
            _min = double.MaxValue;
        }

        public (double average, double min, double max) GetStats()
        {
            if (_samples.Count == 0)
                return (0, 0, 0);
                
            return (_sum / _samples.Count, _min, _max);
        }
    }
} 