using Microsoft.Practices.Prism.PubSubEvents;
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

namespace NL.GI.ComponentesWPF.Cliente
{
    /// <summary>
    /// Interaction logic for BuscaDeMateriais.xaml
    /// </summary>
    public partial class BuscaDeMateriais : UserControl
    {
        private IEventAggregator _eventAggregator = Factory.CriaEventAggregator();
        public BuscaDeMateriais(SearchFor s, Window w)
        {
            InitializeComponent();
            this.DataContext = new BuscaDeMateriaisVM(s, w);
            Texto.Focus();
        }

        private void Texto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _eventAggregator.GetEvent<EnterPressed>().Publish(true);
            }
        }
    }
}
