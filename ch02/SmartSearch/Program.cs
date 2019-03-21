using System;

namespace SmartSearch
{
    class Program
    {
        static string[] fruits = new string[]{
            "Apples", "Apricots", "Avocados",
            "Bananas", "Boysenberries", "Blueberries", "Bing Cherry", "Blackberries",
            "Cherries", "Cantaloupe", "Crab apples", "Clementine", "Cucumbers",
            "Meloms", "Pears", "Grapes", "Strawberries", 
        };
        static void Main(string[] args)
        {
            var sd = new SmartDDictionary<string>(m => m, fruits);
            bool finished = false;
            while(!finished)
            {
                var search = Console.ReadLine();
                Console.WriteLine();
                foreach (var fruit in sd.Search(search, 5))
                {
                    Console.WriteLine(fruit);
                }
                Console.WriteLine("finished ?");
                finished = (Console.ReadKey().KeyChar == 'y');
            }
        }
    }
}
