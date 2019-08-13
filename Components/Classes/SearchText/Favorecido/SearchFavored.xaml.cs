using Microsoft.Practices.Prism.PubSubEvents;
using NL.GI.ComponentesWPF.Cliente;
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
using Telerik.Windows.Controls;
//using UIControls.Eventos;

namespace NL.GI.ComponentesWPF.Cliente
{
    /// <summary>
    /// Interaction logic for BuscaDeFavorecidos.xaml
    /// </summary>
    ///     
    public partial class SearchFavored : UserControl
    {
        SearchFavoredVM vm;

        public SearchFavored(SearchFor s)
        {
            InitializeComponent();
            Texto.Focus();
            vm = new SearchFavoredVM(s);
            this.DataContext = vm;            
        }

        public SearchFavoredVM GetVM() 
        {
            return vm;
        }

    }
}
