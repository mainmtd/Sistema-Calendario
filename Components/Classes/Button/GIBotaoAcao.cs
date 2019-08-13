using System;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIBotaoAcao : Telerik.Windows.Controls.RadButton
    {
        public GIBotaoAcao()
        {
            DefaultStyleKey = typeof(GIBotaoAcao);
        }

        #region Propriedades de Depenencia

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
                "Image",
                typeof(ImageSource),
                typeof(GIBotaoAcao),
                new UIPropertyMetadata(null));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageOverProperty = DependencyProperty.Register(
                "ImageOver",
                typeof(ImageSource),
                typeof(GIBotaoAcao),
                null);

        public ImageSource ImageOver
        {
            get { return (ImageSource)GetValue(ImageOverProperty); }
            set { SetValue(ImageOverProperty, value); }
        }

        public static readonly DependencyProperty ImagePressedProperty = DependencyProperty.Register(
                "ImagePressed",
                typeof(ImageSource),
                typeof(GIBotaoAcao),
                null);

        public ImageSource ImagePressed
        {
            get { return (ImageSource)GetValue(ImagePressedProperty); }
            set { SetValue(ImagePressedProperty, value); }
        }


        public static readonly DependencyProperty ActionProperty = DependencyProperty.Register(
                "Action",
                typeof(GIAcaoBotao),
                typeof(GIBotaoAcao),
                new PropertyMetadata(new PropertyChangedCallback(ActionPropertyCallback)));



        private static void ActionPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GIBotaoAcao botao = (GIBotaoAcao)d;

            if (botao != null)
            {
                GIAcaoBotao acao = (GIAcaoBotao)e.NewValue;
                ImageSourceConverter conversor = new ImageSourceConverter();

                if (acao != GIAcaoBotao.Customizado)
                {                    
                    try
                    {
                        string strAcao = acao.ToString();
                        botao.Content = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString(acao.ToString());                        
                        botao.Image = (ImageSource)conversor.ConvertFromString("pack://siteoforigin:,,,/Images/Normal/GridButtons/" + strAcao + ".png");
                        botao.ImageOver = (ImageSource)conversor.ConvertFromString("pack://siteoforigin:,,,/Images/Over/GridButtons/" + strAcao + ".png");
                        botao.ImagePressed = (ImageSource)conversor.ConvertFromString("pack://siteoforigin:,,,/Images/Pressed/GridButtons/" + strAcao + ".png");
                    }
                    catch(NullReferenceException nr)
                    {
                        var x = nr.InnerException;
                        botao.Image = (ImageSource)conversor.ConvertFromString("pack://siteoforigin:,,,/Images/Normal/GridButtons/Indisponivel.png");
                        botao.ImageOver = (ImageSource)conversor.ConvertFromString("pack://siteoforigin:,,,/Images/Over/GridButtons/Indisponivel.png");
                        botao.ImagePressed = (ImageSource)conversor.ConvertFromString("pack://siteoforigin:,,,/Images/Pressed/GridButtons/Indisponivel.png");
                    }
                    catch(Exception ex)
                    {
                        var y = ex.InnerException;
                        botao.Image = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/indisponivel.png");
                        botao.ImageOver = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/white/indisponivel.png");
                        botao.ImagePressed = (ImageSource)conversor.ConvertFromString("pack://application:,,,/NL.GI.ComponentesWPF.Cliente;component/images/botaoacao/pressed/indisponivel.png");
                    }
                    
                }
            }
        }

        public GIAcaoBotao Action
        {
            get { return (GIAcaoBotao)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        #endregion
    }
}
