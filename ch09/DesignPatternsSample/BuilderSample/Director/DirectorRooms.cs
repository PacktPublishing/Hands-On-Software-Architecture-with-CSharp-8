using DesignPatternsSample.BuilderSample.BuilderInterface;
using System;

namespace DesignPatternsSample.BuilderSample
{
    public class DirectorRooms
    {
        IRoomBuilder _roomBuilder;
        public DirectorRooms(IRoomBuilder roomBuilder)
        {
            _roomBuilder = roomBuilder;
        }
        public Room Construct()
        {
            _roomBuilder.BuildBalcony();
            _roomBuilder.BuildWifi();
            _roomBuilder.BuildBed();
            return _roomBuilder.Build();
        }

    }
}