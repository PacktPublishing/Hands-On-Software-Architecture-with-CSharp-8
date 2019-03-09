using DesignPatternsSample.BuilderSample;
using DesignPatternsSample.SingletonSample;
using System;

namespace DesignPatternsSample
{
    class Program
    {
        static void Main()
        {
            #region Builder Sample
            Console.WriteLine("Builder Sample");

            var directorRoom = new DirectorRooms(new SimpleRoomBuilder());
            var simpleRoom = directorRoom.Construct();
            simpleRoom.Describe();

            directorRoom = new DirectorRooms(new FamilyRoomBuilder());
            var familyRoom = directorRoom.Construct();
            familyRoom.Describe();

            #endregion

            #region Singleton Sample
            Console.WriteLine("Singleton Sample");
            SingletonDemo.Current.Message = "This message will be printed by the singleton sample created with a code snippet";
            SingletonDemo.Current.Print();
            #endregion


            Console.ReadKey();
        }
    }
}
