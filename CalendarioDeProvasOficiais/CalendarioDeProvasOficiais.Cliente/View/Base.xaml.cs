using CalendarioDeProvasOficiais.Cliente.VM;
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
using System.ComponentModel;
using System.Data.Common;
using CalendarioDeProvasOficiais.Comum.Objetos;
using System.Data;
using CalendarioDeProvasOficiais.Cliente.Classes;

namespace CalendarioDeProvasOficiais.Cliente.View
{
    /// <summary>
    /// Interaction logic for Base.xaml
    /// </summary>
    public partial class Base : Window
    {
        public Base()
        {
            InitializeComponent();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.7);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.6);
            this.DataContext = new BaseVM();

        }
    }
}
