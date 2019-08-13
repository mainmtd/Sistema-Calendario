using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NL.GI.ComponentesWPF.Cliente
{
    /// <summary>
    /// Enumerador que representa os tipos que o botão do wizard pode ser desenhado: Avançar ou Voltar
    /// </summary>
    public enum TipoBotaoPasso
    {
        Avancar, Voltar
    }

    /// <summary>
    /// Controle que representa um botão de passos do sistema GI.
    /// Através da propriedade TipoPasso o controle é representado como Avançar ou Voltar
    /// </summary>
    [TemplatePart(Name = "PART_TextBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_Path_Avancar", Type = typeof(Path))]
    [TemplatePart(Name = "PART_Path_Voltar", Type = typeof(Path))]
    [TemplatePart(Name = "PART_Background", Type = typeof(Border))]
    public class GIBotaoWizard : Button
    {
        private TextBlock _textBlock = null;
        private Path _pathAvancar = null;
        private Path _pathVoltar = null;
        private Border _pathBackground = null;

        public GIBotaoWizard()
        {
            this.DefaultStyleKey = typeof(GIBotaoWizard);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AplicaComponentes();
        }

        private void CapturaControles()
        {
            _textBlock = GetTemplateChild("PART_TextBlock") as TextBlock;
            _pathVoltar = GetTemplateChild("PART_Path_Voltar") as Path;
            _pathAvancar = GetTemplateChild("PART_Path_Avancar") as Path;
            _pathBackground = GetTemplateChild("PART_Background") as Border;
        }

        private void SetaVisibilidadePath(Visibility avancar, Visibility voltar)
        {
            _pathAvancar.Visibility = avancar;
            _pathVoltar.Visibility = voltar;
        }

        //Responsável por customizar o botão
        private void AplicaComponentes()
        {
            SetaBotao(TipoPasso);
        }

        private void SetaBotao(TipoBotaoPasso tipo)
        {
            CapturaControles();

            if (_textBlock == null || _pathBackground == null)
                return;

            //Pega as ações customizadas e já seta o caption e a imagem padrão. 
            //Caso não possua nenhuma ação padrão, pego o endereço e o caption passado para a imagem
            switch (tipo)
            {
                //Pegar os textos do arquivo de tradução
                case TipoBotaoPasso.Avancar:
                    _textBlock.Text = "Avançar";
                    SetaVisibilidadePath(Visibility.Visible, Visibility.Collapsed);
                    _pathBackground.CornerRadius = new CornerRadius(10, 0, 0, 10);
                    break;
                case TipoBotaoPasso.Voltar:
                    _textBlock.Text = "Voltar";
                    SetaVisibilidadePath(Visibility.Collapsed, Visibility.Visible);
                    _pathBackground.CornerRadius = new CornerRadius(0, 10, 10, 0);
                    break;
            }
        }




        /// <summary>
        /// Propriedade que recebe o tipo do botão: Avançar ou Voltar
        /// </summary>
        public TipoBotaoPasso TipoPasso
        {
            get { return (TipoBotaoPasso)GetValue(TipoPassoProperty); }
            set { SetValue(TipoPassoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TipoPasso.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipoPassoProperty =
            DependencyProperty.Register("TipoPasso", typeof(TipoBotaoPasso), typeof(GIBotaoWizard),
                new PropertyMetadata(new PropertyChangedCallback(MudaTipoBotao)));

        private static void MudaTipoBotao(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as GIBotaoWizard).SetaBotao((TipoBotaoPasso)e.NewValue);
        }

    }
}
