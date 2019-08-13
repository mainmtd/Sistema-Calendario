using br.com.nucleosplog.GI_Uteis.Classes;
using NL.GI.ComponentesWPF.Cliente.Classes.ComboTree;
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
    [TemplatePart(Name = "radMateriais", Type = typeof(GIRadioButton))]
    [TemplatePart(Name = "radProdutos", Type = typeof(GIRadioButton))]
    [TemplatePart(Name = "radServicos", Type = typeof(GIRadioButton))]
    public class GIComboTree : GIComboTreeGeneric
    { 
        GIRadioButton _radMateriais;
        GIRadioButton _radProdutos;
        GIRadioButton _radServicos;

        static GIComboTree()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(GIComboTree),
                new FrameworkPropertyMetadata(typeof(GIComboTree)));            
        }        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            TabG = TipoTreeConversor.TipoTreeToNum(SelectedRadio);
            GetComponents();            
            IsDropDownOpen = false;
        }
        private void GetComponents()
        {
            _radMateriais = GetTemplateChild("radMateriais") as GIRadioButton;
            _radProdutos = GetTemplateChild("radProdutos") as GIRadioButton;
            _radServicos = GetTemplateChild("radServicos") as GIRadioButton;

            if (_radMateriais != null)
            {
                _radMateriais.Checked += _radMateriais_Checked;
            }
            if (_radProdutos != null)
            {
                _radProdutos.Checked += _radMateriais_Checked;
            }
            if (_radServicos != null)
            {
                _radServicos.Checked += _radMateriais_Checked;
            }
        }
        private void _radMateriais_Checked(object sender, RoutedEventArgs e)
        {
            if (_radMateriais.IsChecked == true)
                SelectedRadio = TipoTree.Materiais; //Tag = 10;
            else if (_radProdutos.IsChecked == true)
                SelectedRadio = TipoTree.Produtos; //Tag = 13;
            else if (_radServicos.IsChecked == true)
                SelectedRadio = TipoTree.Servicos; //Tag = 15;
        }

        private void ChangeSelectedRadio()
        {
            switch (SelectedRadio)
            {
                case TipoTree.Materiais:
                    _radMateriais.IsChecked = true;
                    break;
                case TipoTree.Produtos:
                    _radProdutos.IsChecked = true;
                    break;
                case TipoTree.Servicos:
                    _radServicos.IsChecked = true;
                    break;
            }
        }
        
        public static DependencyProperty SelectedRadioProperty =
           DependencyProperty.Register(
               "SelectedRadio",
               typeof(TipoTree),
               typeof(GIComboTree),
              new UIPropertyMetadata(TipoTree.Materiais, SelectedRadioChanged));

        [Bindable(true)]
        public TipoTree SelectedRadio
        {
            get { return (TipoTree)GetValue(SelectedRadioProperty); }
            set 
            { 
                SetValue(SelectedRadioProperty, value);
                ChangeSelectedRadio();
                TabG = TipoTreeConversor.TipoTreeToNum(SelectedRadio);
                
            }
        }

        private static void SelectedRadioChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            GIComboTree c = ((GIComboTree)sender);

            int cod = c.CodInt;
            c.SelectedRadio = (TipoTree)e.NewValue;
            c.CodInt = cod;
        }

        #region Prop Enabled

        
        public static DependencyProperty MateriaisEnabledProperty =
           DependencyProperty.Register(
               "MateriaisEnabled",
               typeof(bool),
               typeof(GIComboTree),
                new PropertyMetadata(true));

        [Bindable(true)]
        public bool MateriaisEnabled
        {
            get { return (bool)GetValue(MateriaisEnabledProperty); }
            set
            {
                SetValue(MateriaisEnabledProperty, value);
            }
        }

        
        public static DependencyProperty ProdutosEnabledProperty =
           DependencyProperty.Register(
               "ProdutosEnabled",
               typeof(bool),
               typeof(GIComboTree),
                new PropertyMetadata(true));

        [Bindable(true)]
        public bool ProdutosEnabled
        {
            get { return (bool)GetValue(ProdutosEnabledProperty); }
            set
            {
                SetValue(ProdutosEnabledProperty, value);
            }
        }

       
        public static DependencyProperty ServicosEnabledProperty =
           DependencyProperty.Register(
               "ServicosEnabled",
               typeof(bool),
               typeof(GIComboTree),
                new PropertyMetadata(true));

        [Bindable(true)]
        public bool ServicosEnabled
        {
            get { return (bool)GetValue(ServicosEnabledProperty); }
            set
            {
                SetValue(ServicosEnabledProperty, value);
            }
        }



        #endregion

    }

}
