using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.BuilderSample.BuilderInterface
{
    public interface IRoomBuilder
    {
        void BuildWifi();
        void BuildBed();
        void BuildBalcony();
        Room Build();
    }
}
