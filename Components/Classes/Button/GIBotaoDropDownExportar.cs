using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIBotaoDropDownExportar : GIBotaoDropDown
    {
        public GIBotaoDropDownExportar()
        {
            DefaultStyleKey = typeof(GIBotaoDropDownExportar);
            Initialize();
        }

        protected void Initialize()
        {
            Windows8Palette.Palette.AccentColor = Colors.Red;

            try
            {
                ImageSourceConverter conversor = new ImageSourceConverter();

                Image = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/exportar.png");
                ImageOver = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/white/exportar.png");
                ImagePressed = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/pressed/exportar.png");
            }
            catch
            {
                //do nothing
            }
        }

        //public ICommand CmdPDF { get; set; }
        //public ICommand CmdExcel { get; set; }

        public static readonly DependencyProperty CommandPDFProperty = DependencyProperty.Register(
                "CommandPDF",
                typeof(ICommand),
                typeof(GIBotaoDropDownExportar),
                new UIPropertyMetadata(null));
        [Bindable(true)]
        public ICommand CommandPDF
        {
            get { return (ICommand)GetValue(CommandPDFProperty); }
            set { SetValue(CommandPDFProperty, value); /*CmdPDF = value;*/ }
        }


        public static readonly DependencyProperty CommandExcelProperty = DependencyProperty.Register(
                "CommandExcel",
                typeof(ICommand),
                typeof(GIBotaoDropDownExportar),
                new UIPropertyMetadata(null));
        [Bindable(true)]
        public ICommand CommandExcel
        {
            get { return (ICommand)GetValue(CommandExcelProperty); }
            set { SetValue(CommandExcelProperty, value); /*CmdExcel = value;*/ }
        }

        //public static readonly DependencyProperty VisibilityExcluirProperty = DependencyProperty.Register(
        //       "VisibilityExcluir",
        //       typeof(Visibility),
        //       typeof(GIGridToolbarCadastro),
        //       new PropertyMetadata(Visibility.Visible));

        //[Bindable(true)]
        //public Visibility VisibilityExcluir
        //{
        //    get { return (Visibility)GetValue(VisibilityExcluirProperty); }
        //    set { SetValue(VisibilityExcluirProperty, value); }
        //}


        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
                "Image",
                typeof(ImageSource),
                typeof(GIBotaoDropDownExportar),
                new UIPropertyMetadata(null));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public ImageSource ImageOver { get; set; }
        public ImageSource ImagePressed { get; set; }


    }
}
