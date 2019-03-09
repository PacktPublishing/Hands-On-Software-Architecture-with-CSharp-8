using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.SingletonSample
{

    public sealed class SingletonDemo
    {
        private readonly static SingletonDemo _instance = new SingletonDemo();

        public static SingletonDemo Current
        {
            get
            {
                return _instance;
            }
        }

        private SingletonDemo()
        {
            //Implent here the initialization of your singleton
        }

        public string Message { get; set; }

        public void Print()
        {
            Console.WriteLine(Message);
        }
    }
}
