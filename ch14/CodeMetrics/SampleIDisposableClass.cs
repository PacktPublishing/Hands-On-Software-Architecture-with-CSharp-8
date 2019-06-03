using System;

namespace CodeMetricsBadCode
{
    class SampleIDisposableClass : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
