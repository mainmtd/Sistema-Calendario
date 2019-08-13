using br.com.nucleosplog.GI_Uteis.Classes;
using NL.GI.ComponentesWPF.Cliente.Classes.ComboTree;
using NL.GI.ComponentesWPF.Cliente.Classes.ComboTree.PayRec;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NL.GI.ComponentesWPF.Cliente
{
    [TemplatePart(Name = "radRecebimento", Type = typeof(GIRadioButton))]
    [TemplatePart(Name = "radPagamento", Type = typeof(GIRadioButton))]

    public class GITreePayRec : GIComboTreeGeneric
    {
        GIRadioButton _radRecebimento;
        GIRadioButton _radPagamento;

        static GITreePayRec()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(GITreePayRec),
                new FrameworkPropertyMetadata(typeof(GITreePayRec)));            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TabG = PayRecTreeConversor.TipoTreeToNum(SelectedRadio);
            GetComponents();
            IsDropDownOpen = false;
        }
        private void GetComponents()
        {
            _radRecebimento = GetTemplateChild("radRecebimento") as GIRadioButton;
            _radPagamento = GetTemplateChild("radPagamento") as GIRadioButton;

            if (_radRecebimento != null)
            {
                _radRecebimento.Checked += _radRecebimento_Checked;
            }
            if (_radPagamento != null)
            {
                _radPagamento.Checked += _radRecebimento_Checked;
            }

            UpdateSelectedHierarchy();
        }
        void _radRecebimento_Checked(object sender, RoutedEventArgs e)
        {
            if (_radRecebimento.IsChecked == true)
                SelectedRadio = PayRecTree.Recebimento;//Tag = 3
            else if (_radPagamento.IsChecked == true)
                SelectedRadio = PayRecTree.Pagamento; //Tag 4
                
        }       
        public static DependencyProperty SelectedRadioProperty =
           DependencyProperty.Register(
               "SelectedRadio",
               typeof(PayRecTree),
               typeof(GITreePayRec),
               new UIPropertyMetadata(PayRecTree.Recebimento, SelectedRadioChanged));
        [Bindable(true)]
        public PayRecTree SelectedRadio
        {
            get { return (PayRecTree)GetValue(SelectedRadioProperty); }
            set
            {
                SetValue(SelectedRadioProperty, value);
                ChangeSelectedRadio();
                TabG = PayRecTreeConversor.TipoTreeToNum(SelectedRadio);
            }
        }

        private static void SelectedRadioChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            GITreePayRec c = ((GITreePayRec)sender);

            int cod = c.CodInt;
            c.SelectedRadio = (PayRecTree)e.NewValue;
            c.CodInt = cod;
        }

        private void ChangeSelectedRadio()
        {
            switch (SelectedRadio)
            {
                case PayRecTree.Pagamento:
                    _radPagamento.IsChecked = true;
                    break;
                case PayRecTree.Recebimento:
                default:
                    _radRecebimento.IsChecked = true;
                    break;
            }
        }



        #region Enabled

        public static DependencyProperty PagamentoEnabledProperty =
          DependencyProperty.Register(
              "PagamentoEnabled",
              typeof(bool),
              typeof(GIComboTree),
               new PropertyMetadata(true));

        [Bindable(true)]
        public bool PagamentoEnabled
        {
            get { return (bool)GetValue(PagamentoEnabledProperty); }
            set
            {
                SetValue(PagamentoEnabledProperty, value);
            }
        }


        public static DependencyProperty RecebimentoEnabledProperty =
           DependencyProperty.Register(
               "RecebimentoEnabled",
               typeof(bool),
               typeof(GIComboTree),
                new PropertyMetadata(true));

        [Bindable(true)]
        public bool RecebimentoEnabled
        {
            get { return (bool)GetValue(RecebimentoEnabledProperty); }
            set
            {
                SetValue(RecebimentoEnabledProperty, value);
            }
        }

        #endregion







    }
}
