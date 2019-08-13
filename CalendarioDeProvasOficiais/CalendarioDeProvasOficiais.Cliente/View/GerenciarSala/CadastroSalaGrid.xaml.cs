using CalendarioDeProvasOficiais.Cliente.VM.GerenciarSala;
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

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarSala
{
    /// <summary>
    /// Interaction logic for CadastroSalaGrid.xaml
    /// </summary>
    public partial class CadastroSalaGrid : UserControl
    {
        public CadastroSalaGrid()
        {
            InitializeComponent();
            DataContext = new CadastroSalaGridVM();
        }
    }
}
