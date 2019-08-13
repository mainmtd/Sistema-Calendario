using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIInputCheckbox : CheckBox
    {
        public GIInputCheckbox()
        {
            this.DefaultStyleKey = typeof(GIInputCheckbox);
            Windows8Palette.Palette.AccentColor = Colors.Red;
        }
    }
}
