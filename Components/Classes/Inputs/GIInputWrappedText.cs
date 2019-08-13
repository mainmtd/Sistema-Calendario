using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIInputWrappedText : TextBox
    {
        public GIInputWrappedText()
        {
            this.DefaultStyleKey = typeof(GIInputWrappedText);
        }
        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            if (MaxLength <= 0)
                return;

            try
            {
                //Tamanho máximo de caracteres
                if (this.Text.Length > (MaxLength - 1))
                    e.Handled = true;
            }
            catch
            {
                return;
            }
        }
    }
}
