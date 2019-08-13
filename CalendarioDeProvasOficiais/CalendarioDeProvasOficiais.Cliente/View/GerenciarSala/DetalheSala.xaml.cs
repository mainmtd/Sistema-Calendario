using CalendarioDeProvasOficiais.Cliente.VM.GerenciarSala;
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

namespace CalendarioDeProvasOficiais.Cliente.View.GerenciarSala
{
    /// <summary>
    /// Interaction logic for DetalheSala.xaml
    /// </summary>
    public partial class DetalheSala : Window
    {
       DetalheSalaVM _vm;
       public DetalheSala(Sala p, bool ehEdicao)
        {
            InitializeComponent();
            _vm = new DetalheSalaVM(p, this, ehEdicao);
            this.DataContext = _vm;
        }
    }
}
