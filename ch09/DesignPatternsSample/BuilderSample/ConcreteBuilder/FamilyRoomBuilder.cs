using DesignPatternsSample.BuilderSample.BuilderInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.BuilderSample
{
    class FamilyRoomBuilder : IRoomBuilder
    {
        private Room roomToReturn = new Room("Family");
        public Room Build()
        {
            return roomToReturn;
        }

        public void BuildBalcony()
        {
            roomToReturn.BalconyAvailable = true;
        }

        public void BuildBed()
        {
            roomToReturn.NumberOfBeds = 3;
        }

        public void BuildWifi()
        {
            roomToReturn.WiFiFreeOfCharge = true;
        }
    }
}
