using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    public interface IPublisher
    {
        // Inscrire un client
        void Register(ISubscriber subscriber);

        // Désinscription d'un client
        void Unregister(ISubscriber subscriber);

        // Notifie tous les clients
        void Notify();
    }
}