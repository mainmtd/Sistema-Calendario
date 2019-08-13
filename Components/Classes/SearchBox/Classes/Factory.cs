using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente
{
    public static class Factory
    {
        private static IEventAggregator _eventAggregator;

        public static IEventAggregator CriaEventAggregator()
        {
            if (_eventAggregator == null)
                _eventAggregator = new EventAggregator();

            return _eventAggregator;
        }
    }
}
