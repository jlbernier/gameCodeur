using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Observer
{
    public class Magasin : IPublisher
    {
        public bool Stock { get; set; }
        private List<ISubscriber> _subscribers;



        public Magasin()
        {
            _subscribers = new List<ISubscriber>();
            Stock = false;
        }



        public void Register(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            Debug.WriteLine($"Magasin : Inscription du nouveau client.");
        }



        public void Unregister(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            Debug.WriteLine("Magasin : Désinscription d'un client.");
        }



        public void Notify()
        {
            Debug.WriteLine("Magasin : Notification de tous les clients.");
            foreach (ISubscriber subscriber in _subscribers)
            {
                subscriber.OnNotify(this);
            }
        }



        public void DoSomething()
        {
            Debug.WriteLine("Magasin : Changement des stocks.");
            float rand = new Random().Next(2);
            Stock = true;
            if (rand == 0)
            {
                Stock = false;
            }

            Debug.WriteLine($"Magasin : Etat des stocks : {Stock} ");

            if (Stock)
            {
                Notify();
            }
        }
    }
}
