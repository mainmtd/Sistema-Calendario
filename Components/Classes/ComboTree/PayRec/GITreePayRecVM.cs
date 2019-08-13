using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente.Classes.ComboTree.PayRec
{
    public class GITreePayRecVM
    {

        public GITreePayRecVM()
        {
         
        }

        public List<SomeHierarchyViewModel> Items { get; set; }

        public SomeHierarchyViewModel SelectedItem { get; set; }

    }
}
