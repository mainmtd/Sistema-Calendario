using CalendarioDeProvasOficiais.Cliente.VM;
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

namespace CalendarioDeProvasOficiais.Cliente.View
{
    /// <summary>
    /// Interaction logic for Calendario.xaml
    /// </summary>
    public partial class GerencCalendario : UserControl
    {
        public GerencCalendario()
        {
            InitializeComponent();
            DataContext = new GerencCalendarioVM();
        }
    }
}
