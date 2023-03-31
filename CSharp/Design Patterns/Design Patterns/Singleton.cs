using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonName
{
     public class Singleton
    {
        public string Value { get; set; }
        private static Singleton _instance;

        private Singleton()
        {

        }

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }

            return _instance;
        }
    }
}

