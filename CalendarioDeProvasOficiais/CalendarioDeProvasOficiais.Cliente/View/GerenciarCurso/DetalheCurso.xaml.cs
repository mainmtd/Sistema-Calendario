using CalendarioDeProvasOficiais.Cliente.VM.GerenciarCurso;
using CalendarioDeProvasOficiais.Cliente.VM.GerenciarProfessor;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarCurso
{
    /// <summary>
    /// Interaction logic for DetalheCurso.xaml
    /// </summary>
    public partial class DetalheCurso : Window
    {
        DetalheCursoVM _vm;
        public DetalheCurso(Curso c, bool ehEdicao)
        {
            InitializeComponent();
            _vm = new DetalheCursoVM(c, this, ehEdicao);
            this.DataContext = _vm;
        }
    }
}
