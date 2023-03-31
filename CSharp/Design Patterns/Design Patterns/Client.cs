using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Observer
{
    public class Client : ISubscriber
    {
        public string Name { get; private set; }



        public Client(string name)
        {
            Name = name;
        }



        public void OnNotify(IPublisher magasin)
        {
            Debug.WriteLine($"{Name} : Le magasin a reçu du stock.");
        }
    }
}
