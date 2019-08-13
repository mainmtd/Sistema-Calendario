using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente.Classes.Grid
{
    public class ExportEvent : PubSubEvent<ExportType> { }

    public enum ExportType
    {
        Excel,
        PDF
    }
}
