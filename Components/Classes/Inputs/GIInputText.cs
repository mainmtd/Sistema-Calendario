using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace NL.GI.ComponentesWPF.Cliente
{
    //public enum Caixa
    //{
    //    Normal,
    //    Capitalizado,
    //    CaixaAlta,
    //    CaixaBaixa
    //}
    public class GIInputText : RadMaskedTextInput
    {

        //private string _txtNormal;
        //private Caixa _estado;


        public GIInputText()
        {
            this.DefaultStyleKey = typeof(GIInputText);
            this.SelectionOnFocus = Telerik.Windows.Controls.SelectionOnFocus.CaretToEnd;
            this.TextMode = Telerik.Windows.Controls.MaskedInput.TextMode.PlainText;
            //Initialize();
        }

        //private void Initialize()
        //{
        //    //this.CaixaTexto = new Microsoft.Practices.Prism.Commands.DelegateCommand(DoCaixa);
        //    //_estado = Caixa.Normal;
            
           
        //}

        //Se for vazio seta o value null
        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnPreviewKeyUp(e);
            if (this.Value == "")
                this.Value = null;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.CommandLock = new Microsoft.Practices.Prism.Commands.DelegateCommand(DoLock);
            if (VisibilityLock == System.Windows.Visibility.Visible)
                this.IsReadOnly = true;
        }


        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    base.OnPreviewKeyDown(e);

        //    if (e.Key == Key.Back)
        //        _txtNormal = _txtNormal.Remove(_txtNormal.Length);

        //    if (e.Key == Key.Delete)
        //        return;

        //    if (char.IsLetterOrDigit((char)e.Key))
        //        _txtNormal += e.Key;
        //}

        //private void DoCaixa()
        //{
        //    if (_estado == Caixa.Normal)
        //    {
        //        _estado = Caixa.Capitalizado;
        //        TextInfo txt = NL.GI.ComponentesWPF.Cliente.Resources.Resources.Culture.TextInfo;
        //        this.Value = txt.ToTitleCase(this.Value.ToLower());
        //    }                
        //    else if (_estado == Caixa.Capitalizado)
        //    {
        //        _estado = Caixa.CaixaAlta;
        //        this.Value = Value.ToUpper();
        //    }
        //    else if (_estado == Caixa.CaixaAlta)
        //    {
        //        _estado = Caixa.CaixaBaixa;
        //        this.Value = Value.ToLower();
        //    }

        //    if (_estado == Caixa.CaixaBaixa)
        //    {
        //        _estado = Caixa.Normal;
        //        this.Value = _txtNormal;
        //    }
                
        //}


        //private void DoCapitalizado()
        //{
        //    TextInfo txt = NL.GI.ComponentesWPF.Cliente.Resources.Resources.Culture.TextInfo;            
        //    this.Value = txt.ToTitleCase(this.Value.ToLower());
        //}

        //private void DoMaiusculo()
        //{
        //    this.Value = this.Value.ToUpper();
        //}

        //private void DoMinusculo()
        //{
        //    this.Value = this.Value.ToLower();
        //}

        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
                "MaxLength",
                typeof(int),
                typeof(GIInputText),
                new UIPropertyMetadata(0));

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }


        public static readonly DependencyProperty IsOnlyNumbersProperty = DependencyProperty.Register(
                "IsOnlyNumbers",
                typeof(bool),
                typeof(GIInputText),
                new UIPropertyMetadata(false));

        public bool IsOnlyNumbers
        {
            get { return (bool)GetValue(IsOnlyNumbersProperty); }
            set { SetValue(IsOnlyNumbersProperty, value); }
        }

        #region Comment
        //public static readonly DependencyProperty ContextMenuProperty = DependencyProperty.Register(
        //        "ContextMenu",
        //        typeof(bool),
        //        typeof(ContextMenu));

        //public ContextMenu ContextMenu 
        //{
        //    get { return (ContextMenu)GetValue(ContextMenuProperty); }
        //    set { SetValue(ContextMenuProperty, value); }
        //}


        //private ICommand _caixaTexto;

        //public ICommand CaixaTexto { get { return _caixaTexto; } set { _caixaTexto = value; } }

        //public static readonly DependencyProperty VisibilityCaixaProperty = DependencyProperty.Register(
        //    "VisibilityCaixa",
        //    typeof(Visibility),
        //    typeof(GIGridToolbarCadastro),
        //    new PropertyMetadata(Visibility.Visible));
        //private ICommand _cmdPrint;
        //private ICommand _cmdPrintPDF;

        //[Bindable(true)]
        //public Visibility VisibilityCaixa
        //{
        //    get { return (Visibility)GetValue(VisibilityCaixaProperty); }
        //    set { SetValue(VisibilityCaixaProperty, value); }
        //}


        //private ICommand _cmdCapi;
        //private ICommand _cmdMai;
        //private ICommand _cmdMin;
        //public ICommand Capitalizado { get { return _cmdCapi; } set { _cmdCapi = value; } }
        //public ICommand Maiusculo { get { return _cmdMai; } set { _cmdMai = value; } }
        //public ICommand Minusculo { get { return _cmdMin; } set { _cmdMin = value; } }

        #endregion

        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);


            if (IsOnlyNumbers)
            {
                try
                {
                    //Permite somente digitar números.
                    char c = Convert.ToChar(e.Text);
                    if (Char.IsNumber(c))
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                catch
                {
                    e.Handled = false;
                }
            }

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

        #region Lock

        public static readonly DependencyProperty CommandLockProperty = DependencyProperty.Register(
              "CommandLock",
              typeof(ICommand),
              typeof(GIInputText),
              new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandLock
        {
            get { return (DelegateCommand)GetValue(CommandLockProperty); }
            set { SetValue(CommandLockProperty, value); }
        }

        public static readonly DependencyProperty VisibilityLockProperty = DependencyProperty.Register(
               "VisibilityLock",
               typeof(Visibility),
               typeof(GIInputText),
               new PropertyMetadata(Visibility.Collapsed));

        [Bindable(true)]
        public Visibility VisibilityLock
        {
            get { return (Visibility)GetValue(VisibilityLockProperty); }
            set 
            { 
                SetValue(VisibilityLockProperty, value);
                SetIsReadOnly(value);
            }
        }

        private void DoLock()
        {
            this.IsReadOnly = false;
            VisibilityLock = System.Windows.Visibility.Collapsed;
        }

        private void SetIsReadOnly(Visibility v)
        {
            if (v == Visibility.Visible)
                this.IsReadOnly = true;
            else
                this.IsReadOnly = false;
        }
        #endregion

    }
}
