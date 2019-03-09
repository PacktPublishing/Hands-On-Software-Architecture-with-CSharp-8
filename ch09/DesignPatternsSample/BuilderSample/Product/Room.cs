using System;

namespace DesignPatternsSample.BuilderSample
{
    public class Room
    {
        public Room(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public bool WiFiFreeOfCharge { get; set; }
        public int NumberOfBeds { get; set; }
        public bool BalconyAvailable { get; set; }

        public void Describe()
        {
            var wifi= WiFiFreeOfCharge ? "is" : "is not";
            var balcony = BalconyAvailable ? "is" : "is not";
            Console.WriteLine($"{Name} room");
            Console.WriteLine($"Number of bed(s): {NumberOfBeds}");
            Console.WriteLine($"There {wifi} a balcony.");
            Console.WriteLine($"This room {wifi} wi-fi Free");
            Console.WriteLine("");
        }

    }
}