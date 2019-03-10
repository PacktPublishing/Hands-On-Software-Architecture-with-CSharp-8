using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsSample.SingletonSample
{

    public sealed class SingletonDemo
    {
        #region This is the Singleton definition
        private static SingletonDemo _instance;
        public static SingletonDemo Current
        {
            get
            {
                if (_instance == null)
                    _instance = new SingletonDemo();
                return _instance;
            }
        }
        #endregion

        public string Message { get; set; }

        public void Print()
        {
            Console.WriteLine(Message);
            Console.WriteLine("");
        }
    }
}
