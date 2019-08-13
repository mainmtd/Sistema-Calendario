using CalendarioDeProvasOficiais.Cliente.VM.GerenciarCurso;
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

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarCurso
{
    /// <summary>
    /// Interaction logic for CadastroCursoGrid.xaml
    /// </summary>
    public partial class CadastroCursoGrid : UserControl
    {
        public CadastroCursoGrid()
        {
            InitializeComponent();
            DataContext = new CadastroCursoGridVM();
        }
    }
}
