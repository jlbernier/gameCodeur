using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    public interface ISubscriber
    {
        // Quand il est notifié
        void OnNotify(IPublisher publisher);
    }
}
