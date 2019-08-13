using CalendarioDeProvasOficiais.Cliente.VM;
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

namespace CalendarioDeProvasOficiais.Cliente.View
{
    /// <summary>
    /// Interaction logic for AlterarEscalacao.xaml
    /// </summary>
    public partial class AlterarEscalacao : Window
    {
        AlterarEscalacaoVM _vm;
        public AlterarEscalacao(Prova prova, int ano)
        {
            InitializeComponent();
            _vm = new AlterarEscalacaoVM(this, prova, ano);
            this.DataContext = _vm;
        }

        public bool Cancelou()
        {
            return _vm.Cancelou;
        }
    }
}
