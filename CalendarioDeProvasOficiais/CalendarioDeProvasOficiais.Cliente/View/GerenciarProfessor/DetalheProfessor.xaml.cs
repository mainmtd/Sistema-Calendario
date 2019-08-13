using CalendarioDeProvasOficiais.Cliente.VM.GerenciarProfessor;
using CalendarioDeProvasOficiais.Comum.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarProfessor
{
    /// <summary>
    /// Interaction logic for DetalheProfessor.xaml
    /// </summary>
    public partial class DetalheProfessor : Window
    {
        DetalheProfessorVM _vm;
        public DetalheProfessor(Professor p, bool ehEdicao)
        {
            InitializeComponent();
            _vm = new DetalheProfessorVM(p, this, ehEdicao);
            this.DataContext = _vm;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
