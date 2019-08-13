using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NL.GI.ComponentesWPF.Cliente.Classes.Grid
{
    /// <summary>
    /// Interaction logic for UCExportarTela.xaml
    /// </summary>
    public partial class UCExportarTela : UserControl
    {
        UCExportarTelaVM _vm;
        public UCExportarTela()
        {
            InitializeComponent();

            _vm = new UCExportarTelaVM();

            this.DataContext = _vm;
        }


    }
}
