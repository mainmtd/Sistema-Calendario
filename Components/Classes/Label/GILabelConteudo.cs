using System.Windows;
using System.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GILabelConteudo : Label
    {
        public GILabelConteudo()
        {
            this.DefaultStyleKey = typeof(GILabelConteudo);
        }

        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRequired.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRequiredProperty =
            DependencyProperty.Register("IsRequired", typeof(bool), typeof(GILabelConteudo), new PropertyMetadata(false));
    }
}
