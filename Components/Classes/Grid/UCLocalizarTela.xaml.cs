using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NL.GI.ComponentesWPF.Cliente.Classes.Grid
{
    /// <summary>
    /// Interaction logic for UCLocalizarTela.xaml
    /// </summary>
    public partial class UCLocalizarTela : UserControl
    {
        UCLocalizarTelaVM _vm;
        Action OnCancelar;
        
        public UCLocalizarTela()
        {
            InitializeComponent();
            _vm = new UCLocalizarTelaVM();
            this.DataContext = _vm;
            //this.txt.Focusable = true;
            //this.txt.Focus();
            //FocusManager.SetFocusedElement(gd, txt);
            this.txt.Focus();
        }

        public void Initialize(Action onCancelar)
        {
            OnCancelar = onCancelar;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OnCancelar();
        }

        private void GIInputText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                _vm.CmdLocalizar.Execute(null);


            //btn.isClicked = trur;
        }
    }
}
