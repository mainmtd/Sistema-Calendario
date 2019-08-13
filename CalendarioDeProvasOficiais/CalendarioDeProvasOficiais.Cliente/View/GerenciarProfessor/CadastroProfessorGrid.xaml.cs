using CalendarioDeProvasOficiais.Cliente.VM.GerenciarProfessor;
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

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarProfessor
{
    /// <summary>
    /// Interaction logic for CadastroProfessorGrid.xaml
    /// </summary>
    public partial class CadastroProfessorGrid : UserControl
    {
        public CadastroProfessorGrid()
        {
            InitializeComponent();
            this.DataContext = new CadastroProfessorGridVM();
        }
    }
}
