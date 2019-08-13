using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    [TemplatePart(Name = "BtnLocalizar", Type = typeof(GIBotaoComum))]
    [TemplatePart(Name = "TxtLocalizar", Type = typeof(GIInputText))]
    public class GIBotaoDropDownLocalizar : GIBotaoDropDown
    {
        private GIBotaoComum _btnLocalizar;
        private GIInputText _txtLocalizar;
        public GIBotaoDropDownLocalizar()
        {
            DefaultStyleKey = typeof(GIBotaoDropDownLocalizar);
            Initialize();
        }

        protected void Initialize()
        {
            Windows8Palette.Palette.AccentColor = Colors.Red;

            try
            {
                ImageSourceConverter conversor = new ImageSourceConverter();

                Image = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/buscar.png");
                ImageOver = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/white/buscar.png");
                ImagePressed = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/pressed/buscar.png");
            }
            catch
            {
                //do nothing
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyTemplates();
        }

        private void ApplyTemplates()
        {
            GetComponents();

            if (_btnLocalizar != null)
                _btnLocalizar.Content = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("Buscar");

            if (_txtLocalizar != null)
            {
                _txtLocalizar.PreviewKeyDown += _txtLocalizar_PreviewKeyDown;
                _txtLocalizar.Focus();
                TextBox = _txtLocalizar;
            }
                
        }

        void _txtLocalizar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (_btnLocalizar != null)
                    CommandLocalizar.Execute(null);
            }
        }


        private void GetComponents()
        {
            _btnLocalizar = GetTemplateChild("BtnLocalizar") as GIBotaoComum;
            _txtLocalizar = GetTemplateChild("TxtLocalizar") as GIInputText;
        }

        public static readonly DependencyProperty CommandLocalizarProperty = DependencyProperty.Register(
                "CommandLocalizar",
                typeof(ICommand),
                typeof(GIBotaoDropDownLocalizar),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandLocalizar
        {
            get { return (ICommand)GetValue(CommandLocalizarProperty); }
            set { SetValue(CommandLocalizarProperty, value); }
        }

        public static readonly DependencyProperty FindTextProperty = DependencyProperty.Register(
                "FindText",
                typeof(String),
                typeof(GIBotaoDropDownLocalizar),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public bool IsTextFocused 
        {
            get { return (bool)GetValue(IsTextFocusedProperty); }
            set 
            { 
                SetValue(IsTextFocusedProperty, value);

                if (IsTextFocused)
                    _txtLocalizar.Focus();


            }
        }


        public static readonly DependencyProperty TextBoxProperty = DependencyProperty.Register(
                "TextBox",
                typeof(GIInputText),
                typeof(GIBotaoDropDownLocalizar),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public GIInputText TextBox
        {
            get { return (GIInputText)GetValue(TextBoxProperty); }
            set { SetValue(TextBoxProperty, value); }
        }


        public static readonly DependencyProperty IsTextFocusedProperty = DependencyProperty.Register(
                "IsTextFocused",
                typeof(bool),
                typeof(GIBotaoDropDownLocalizar),
                new UIPropertyMetadata(true));

        [Bindable(true)]
        public String FindText
        {
            get { return (String)GetValue(FindTextProperty); }
            set { SetValue(FindTextProperty, value); }
        }

        public static readonly DependencyProperty BtnEnabledProperty = DependencyProperty.Register(
                "BtnEnabled",
                typeof(bool),
                typeof(GIBotaoDropDownLocalizar),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public bool BtnEnabled
        {
            get { return (bool)GetValue(BtnEnabledProperty); }
            set { SetValue(BtnEnabledProperty, value); }
        }


        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
               "Image",
               typeof(ImageSource),
               typeof(GIBotaoDropDownLocalizar),
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
