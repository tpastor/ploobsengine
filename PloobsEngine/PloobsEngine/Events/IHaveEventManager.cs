using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Events
{
    public interface IHaveEventManager
    {
        EventManager EventsManager { get; }
    }
}
