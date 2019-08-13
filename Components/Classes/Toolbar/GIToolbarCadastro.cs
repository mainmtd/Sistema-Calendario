using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIToolbarCadastro : GIGridToolbarCadastro
    {
        public GIToolbarCadastro()
        {
            this.DefaultStyleKey = typeof(GIToolbarCadastro);
            Initialize();
        }
    }
}
