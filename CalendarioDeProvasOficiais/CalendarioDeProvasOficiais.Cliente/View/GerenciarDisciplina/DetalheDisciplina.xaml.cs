using CalendarioDeProvasOficiais.Cliente.VM.GerenciarDisciplina;
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

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarDisciplina
{
    /// <summary>
    /// Interaction logic for DetalheDisciplina.xaml
    /// </summary>
    public partial class DetalheDisciplina : Window
    {
        DetalheDisciplinaVM _vm;
        public DetalheDisciplina(Disciplina disciplina, bool ehEdicao, List<Professor> listProf)
        {
            InitializeComponent();
            _vm = new DetalheDisciplinaVM(disciplina, this, ehEdicao, listProf);
            DataContext = _vm;
        }
    }
}
