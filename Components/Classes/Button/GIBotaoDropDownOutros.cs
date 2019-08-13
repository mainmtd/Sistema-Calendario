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
    [TemplatePart(Name = "Acessos", Type = typeof(GIBotaoAcao))]
    [TemplatePart(Name = "MudarSenha", Type = typeof(GIBotaoAcao))]

    public class GIBotaoDropDownOutros : GIBotaoDropDown
    {

        private GIBotaoAcao _acessos;
        private GIBotaoAcao _mudarSenha;
        public GIBotaoDropDownOutros()
        {
            DefaultStyleKey = typeof(GIBotaoDropDownOutros);
            Initialize();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AplicaComponentes();
        }

        private void AplicaComponentes()
        {
            CapturaControles();

            if (_acessos == null || _mudarSenha == null)
                return;

            _acessos.Content = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("Acessos");
            _mudarSenha.Content = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("MudarSenha");

        }


        private void CapturaControles()
        {
            _acessos = GetTemplateChild("Acessos") as GIBotaoAcao;
            _mudarSenha = GetTemplateChild("MudarSenha") as GIBotaoAcao;
        }

        protected void Initialize()
        {
            Windows8Palette.Palette.AccentColor = Colors.Red;

            try
            {
                ImageSourceConverter conversor = new ImageSourceConverter();

                //Image = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/exportar.png");
                //ImageOver = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/white/exportar.png");
                //ImagePressed = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/pressed/exportar.png");
            }
            catch
            {
                //do nothing
            }
        }


        #region Acesso


        public static readonly DependencyProperty CommandAcessoProperty = DependencyProperty.Register(
                "CommandAcesso",
                typeof(ICommand),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandAcesso
        {
            get { return (ICommand)GetValue(CommandAcessoProperty); }
            set { SetValue(CommandAcessoProperty, value); }
        }

        public static readonly DependencyProperty ImageAcessoProperty = DependencyProperty.Register(
                "ImageAcesso",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImageAcesso
        {
            get { return (ImageSource)GetValue(ImageAcessoProperty); }
            set { SetValue(ImageAcessoProperty, value); }
        }


        public static readonly DependencyProperty ImageOverAcessoProperty = DependencyProperty.Register(
                "ImageOverAcesso",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImageOverAcesso
        {
            get { return (ImageSource)GetValue(ImageOverAcessoProperty); }
            set { SetValue(ImageOverAcessoProperty, value); }
        }



        public static readonly DependencyProperty ImagePressedAcessoProperty = DependencyProperty.Register(
                "ImagePressedAcesso",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImagePressedAcesso
        {
            get { return (ImageSource)GetValue(ImagePressedAcessoProperty); }
            set { SetValue(ImagePressedAcessoProperty, value); }
        }

        #endregion

        #region Senha (Usuario)

        public static readonly DependencyProperty CommandMudarSenhaUsuarioProperty = DependencyProperty.Register(
                "CommandMudarSenhaUsuario",
                typeof(ICommand),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandMudarSenhaUsuario
        {
            get { return (ICommand)GetValue(CommandMudarSenhaUsuarioProperty); }
            set { SetValue(CommandMudarSenhaUsuarioProperty, value); }
        }



        public static readonly DependencyProperty ImageMudarSenhaUsuarioProperty = DependencyProperty.Register(
                "ImageMudarSenhaUsuario",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImageMudarSenhaUsuario
        {
            get { return (ImageSource)GetValue(ImageMudarSenhaUsuarioProperty); }
            set { SetValue(ImageMudarSenhaUsuarioProperty, value); }
        }


        public static readonly DependencyProperty ImageOverMudarSenhaUsuarioProperty = DependencyProperty.Register(
                "ImageOverMudarSenhaUsuario",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImageOverMudarSenhaUsuario
        {
            get { return (ImageSource)GetValue(ImageOverMudarSenhaUsuarioProperty); }
            set { SetValue(ImageOverMudarSenhaUsuarioProperty, value); }
        }



        public static readonly DependencyProperty ImagePressedMudarSenhaUsuarioProperty = DependencyProperty.Register(
                "ImagePressedMudarSenhaUsuario",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImagePressedMudarSenhaUsuario 
        {
            get { return (ImageSource)GetValue(ImagePressedMudarSenhaUsuarioProperty); }
            set { SetValue(ImagePressedMudarSenhaUsuarioProperty, value); }
        }

        #endregion

        #region Senha (Email)

        public static readonly DependencyProperty CommandMudarSenhaEmailProperty = DependencyProperty.Register(
                "CommandMudarSenhaEmail",
                typeof(ICommand),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandMudarSenhaEmail
        {
            get { return (ICommand)GetValue(CommandMudarSenhaEmailProperty); }
            set { SetValue(CommandMudarSenhaEmailProperty, value); }
        }

        public static readonly DependencyProperty ImageMudarSenhaEmailProperty = DependencyProperty.Register(
                "ImageMudarSenhaEmail",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImageMudarSenhaEmail
        {
            get { return (ImageSource)GetValue(ImageMudarSenhaEmailProperty); }
            set { SetValue(ImageMudarSenhaEmailProperty, value); }
        }


        public static readonly DependencyProperty ImageOverMudarSenhaEmailProperty = DependencyProperty.Register(
                "ImageOverMudarSenhaEmail",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImageOverMudarSenhaEmail
        {
            get { return (ImageSource)GetValue(ImageOverMudarSenhaEmailProperty); }
            set { SetValue(ImageOverMudarSenhaEmailProperty, value); }
        }

        public static readonly DependencyProperty ImagePressedMudarSenhaEmailProperty = DependencyProperty.Register(
                "ImagePressedMudarSenhaEmail",
                typeof(ImageSource),
                typeof(GIBotaoDropDownOutros),
                new UIPropertyMetadata(null));

        [Bindable(true)]
        public ImageSource ImagePressedMudarSenhaEmail
        {
            get { return (ImageSource)GetValue(ImagePressedMudarSenhaEmailProperty); }
            set { SetValue(ImagePressedMudarSenhaEmailProperty, value); }
        }

        #endregion
    }
}
