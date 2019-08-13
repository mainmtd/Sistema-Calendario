using System;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIFiltroPadrao : RadExpander
    {
        public GIFiltroPadrao()
        {
            DefaultStyleKey = typeof(GIFiltroPadrao);

            //Colocar a cor do selecionado vermelha como padrão (padrão original é azul)
            Windows8Palette.Palette.AccentColor = Colors.Red;

        }

    }
}
