using DesignPatternsSample.BuilderSample.BuilderInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.BuilderSample
{
    class SimpleRoomBuilder : IRoomBuilder
    {
        private Room roomToReturn = new Room("Simple");
        public Room Build()
        {
            return roomToReturn;
        }

        public void BuildBalcony()
        {
            roomToReturn.BalconyAvailable = false;
        }

        public void BuildBed()
        {
            roomToReturn.NumberOfBeds = 1;
        }

        public void BuildWifi()
        {
            roomToReturn.WiFiFreeOfCharge = false;
        }
    }
}
