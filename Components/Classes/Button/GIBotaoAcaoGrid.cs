using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
//using Infra.Traducao;

namespace GI_ComponentesWPF
{
    public class GIBotaoAcaoGrid : RadButton
    {
        public GIBotaoAcaoGrid()
        {
            DefaultStyleKey = typeof(GIBotaoAcaoGrid);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #region Propriedades de Dependencia

        #region Descricao
        public static readonly DependencyProperty DescricaoProperty = DependencyProperty.Register(
                "Descricao",
                typeof(string),
                typeof(GIBotaoAcaoGrid),
                null);

        public string Descricao
        {
            get { return (string)GetValue(DescricaoProperty); }
            set { SetValue(DescricaoProperty, value); }
        }
        #endregion

        #region Imagem
        public static readonly DependencyProperty ImagemProperty = DependencyProperty.Register(
                "Imagem",
                typeof(ImageSource),
                typeof(GIBotaoAcaoGrid),
                null);

        public ImageSource Imagem
        {
            get { return (ImageSource)GetValue(ImagemProperty); }
            set { SetValue(ImagemProperty, value); }
        }
        #endregion

        #region ImagemOver
        public static readonly DependencyProperty ImagemOverProperty = DependencyProperty.Register(
                "ImagemOver",
                typeof(ImageSource),
                typeof(GIBotaoAcaoGrid),
                null);

        public ImageSource ImagemOver
        {
            get { return (ImageSource)GetValue(ImagemOverProperty); }
            set { SetValue(ImagemOverProperty, value); }
        }
        #endregion

        #region Acao
        public static readonly DependencyProperty AcaoProperty = DependencyProperty.Register(
                "Acao",
                typeof(GIAcaoBotao),
                typeof(GIBotaoAcaoGrid),
                new PropertyMetadata(new PropertyChangedCallback(AcaoPropertyCallback)));

        private static void AcaoPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GIBotaoAcaoGrid botao = (GIBotaoAcaoGrid)d;

            if (botao != null)
            {
                GIAcaoBotao acao = (GIAcaoBotao)e.NewValue;

                ImageSourceConverter conversor = new ImageSourceConverter();

                if (acao != GIAcaoBotao.Customizado)
                {
                    botao.Imagem = (ImageSource)conversor.ConvertFromString("/Imagens/White/" + acao.ToString() + ".png");
                    botao.ImagemOver = (ImageSource)conversor.ConvertFromString("/Imagens/White/" + acao.ToString() + ".png");
                    //botao.Descricao = botao.TraduzTexto(acao.ToString());
                }
            }
        }

        public GIAcaoBotao Acao
        {
            get { return (GIAcaoBotao)GetValue(AcaoProperty); }
            set { SetValue(AcaoProperty, value); }
        }
        #endregion

        #endregion

        //private string TraduzTexto(string texto)
        //{
        //    try
        //    {
        //        return Tradutor.Traduz(texto);
        //    }
        //    catch
        //    {
        //        return texto;
        //    }
        //}
    }
}

