using CalendarioDeProvasOficiais.Cliente.VM.GerenciarTurma;
using CalendarioDeProvasOficiais.Comum.Objetos;
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
using System.Windows.Shapes;

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarTurma
{
    /// <summary>
    /// Interaction logic for DetalheTurma.xaml
    /// </summary>
    public partial class DetalheTurma : Window
    {
        DetalheTurmaVM _vm;
        public DetalheTurma(Turma t, bool ehEdicao, List<Disciplina> disciplina)
        {
            InitializeComponent();
            _vm = new DetalheTurmaVM(t, this, ehEdicao, disciplina);
            this.DataContext = _vm;
        }
    }
}
