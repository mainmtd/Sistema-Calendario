using System;
using System.Windows;
using System.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIToolbar : StackPanel
    {
        //private ResourceDictionary _rd;

        public GIToolbar()
        {
            DefaultStyleKey = typeof(GIToolbar);
            //_rd = new ResourceDictionary();
            //_rd.Source = new Uri("/NL.GI.ComponentesWPF.Cliente;component/Themes/GIToolbar.xaml", UriKind.Relative);
            //this.Style = (Style)_rd["ToolbarStyle"];
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //}
    }
}
