using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.CommandSample.Receiver
{
    class Package
    {
        private int likes;
        private int dislikes;
        private int loves;
        public string Name { get; private set; }

        public Package(string name)
        {
            Name = name;
        }
        public void Like()
        {
            likes++;
            if (likes == 1)
                Console.WriteLine($"There is {likes} like to package {Name}");
            if (likes > 1)
                Console.WriteLine($"There are {likes} likes to package {Name}");
        }

        public void Dislike()
        {
            dislikes++;
            if (dislikes == 1)
                Console.WriteLine($"There is {dislikes} dislike to package {Name}");
            if (dislikes > 1)
                Console.WriteLine($"There are {dislikes} dislikes to package {Name}");
        }

        public void Love()
        {
            loves++;
            if (loves == 1)
                Console.WriteLine($"There is {loves} love to package {Name}");
            if (dislikes > 1)
                Console.WriteLine($"There are {loves} loves to package {Name}");
        }
    }
}
