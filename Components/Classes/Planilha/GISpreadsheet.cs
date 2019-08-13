using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GISpreadsheet: RadSpreadsheet
    {
        public GISpreadsheet()
        {
            DefaultStyleKey = typeof(GISpreadsheet);
        }



        public static readonly DependencyProperty WorkbookBindableProperty = DependencyProperty.Register
            ("WorkbookBindable",
            typeof(Workbook),
            typeof(GISpreadsheet),
            new PropertyMetadata(null, OnWorkbookChanged));

        [Bindable(true)]
        public Workbook WorkbookBindable
        {
            get { return (Workbook)GetValue(WorkbookBindableProperty); }
            set { SetValue(WorkbookBindableProperty, value); }
        }


        private static void OnWorkbookChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GISpreadsheet spreadsheet = d as GISpreadsheet;
            if (spreadsheet != null)
            {
                spreadsheet.Workbook = (Workbook)e.NewValue;
            }
        }

    }
}
