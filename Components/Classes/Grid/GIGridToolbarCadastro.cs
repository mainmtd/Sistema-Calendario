using Microsoft.Practices.Prism.PubSubEvents;
using NL.GI.ComponentesWPF.Cliente.Classes.Grid;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using System.Linq;
using NL.GI.ComponentesWPF.Cliente.Interfaces;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using System.Collections.Generic;
using Telerik.Windows.Controls.GridView;
using System.Windows.Threading;
using Telerik.Windows.Data;

namespace NL.GI.ComponentesWPF.Cliente
{
    [TemplatePart(Name = "BtnLocalizar", Type = typeof(GIBotaoAcao))]
    [TemplatePart(Name = "ExportarBtn", Type = typeof(GIBotaoDropDownExportar))]
    [TemplatePart(Name = "OutrosBtn", Type = typeof(GIBotaoDropDownOutros))]
    public class GIGridToolbarCadastro : GIGrid
    {

        #region Attributes
        GIBotaoDropDownLocalizar _btnLocalizar;
        GIBotaoDropDownExportar _btnExportar;
        GIBotaoDropDownOutros _btnOutros;
        private IEventAggregator _ea = Factory.CriaEventAggregator();

        #endregion

        #region Initialize Methods

        public GIGridToolbarCadastro()
        {
            this.DefaultStyleKey = typeof(GIGridToolbarCadastro);
            Initialize();
        }
        protected void Initialize()
        {
            this.CmdPrint = new Microsoft.Practices.Prism.Commands.DelegateCommand(DoPrint, CanPrint);
            this.CmdPrintPDF = new Microsoft.Practices.Prism.Commands.DelegateCommand(DoPrintPDF, CanPrint);
            this.CommandImprimir = new Microsoft.Practices.Prism.Commands.DelegateCommand(DoImprimir, CanImprimir);
            this.CommandLocalizar = new Microsoft.Practices.Prism.Commands.DelegateCommand(DoLocalizar, CanLocalizar);
            this.SelectionUnit = Telerik.Windows.Controls.GridView.GridViewSelectionUnit.FullRow;
            _ea.GetEvent<ExportEvent>().Subscribe(ExportAs);
            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GetComponents();
        }

        private void GetComponents()
        {
            _btnLocalizar = GetTemplateChild("BtnLocalizar") as GIBotaoDropDownLocalizar;
            _btnExportar = GetTemplateChild("ExportarBtn") as GIBotaoDropDownExportar;
            _btnOutros = GetTemplateChild("OutrosBtn") as GIBotaoDropDownOutros;

            if (_btnExportar != null)
                _btnExportar.Content = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("Exportar");
            if (_btnOutros != null)
                _btnOutros.Content = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("Outros");
            if (_btnLocalizar != null)
            {
                _btnLocalizar.Content = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("Buscar");
                _btnLocalizar.DropDownClosed += _btnLocalizar_DropDownClosed;
                _btnLocalizar.DropDownOpened += _btnLocalizar_DropDownOpened;
                //_btnLocalizar.KeepOpen = true;
            }
        }

        protected override void OnSelectedItemChanged(object oldItem, object newItem)
        {
            base.OnSelectedItemChanged(oldItem, newItem);

            if(_btnLocalizar != null)
                if (_btnLocalizar.IsOpen)
                    SetFocusOnTxt();
        }
        protected override void OnSelectedCellsChanging(GridViewSelectedCellsChangingEventArgs args)
        {
            base.OnSelectedCellsChanging(args);

            if (_btnLocalizar != null)
                if (_btnLocalizar.IsOpen)
                    SetFocusOnTxt();
        }       
        void _btnLocalizar_DropDownOpened(object sender, RoutedEventArgs e)
        {
            SetFocusOnTxt();
        }
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            CheckVisibilityLocalizar();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if(e.Key != Key.Return)
                base.OnPreviewKeyDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e.Key != Key.Return)
                base.OnKeyDown(e);
        }

        #endregion

        #region Find

        bool isTheFirstClick = true;
        string findOriginal;
        List<object> _itemsFind;
        object _lastFind;

        private void CloseDropDownLocalizar()
        {
            _btnLocalizar.IsOpen = false;
            OnDropLocalizarClosed();
        }
        void _btnLocalizar_DropDownClosed(object sender, RoutedEventArgs e)
        {
            if(FindText != "" || FindText != null)
                OnDropLocalizarClosed();
        }
        private void OnDropLocalizarClosed()
        {
            FindText = "";
            isTheFirstClick = true;
            _btnLocalizar.FindText = "";

            if (_lastFind != null)
            {
                this.SelectedItems.Add(_lastFind);
                _lastFind = null;
            }
        }

        private void CheckVisibilityLocalizar()
        {
            //Seta o botão de Localizar invisivel caso os items não implementem a interface IGIGridToolbarFindable
            if (Items != null)
            {
                if (Items[0] is IGIGridToolbarFindable || Items is IGIGridToolbarFindable)
                {
                    if(this.CommandLocalizar == null)
                        this.CommandLocalizar = new Microsoft.Practices.Prism.Commands.DelegateCommand(DoLocalizar, CanLocalizar);  

                    this.VisibilityLocalizar = System.Windows.Visibility.Visible;                                      
                }

                else
                {
                    this.CommandLocalizar = null;
                    this.VisibilityLocalizar = System.Windows.Visibility.Collapsed;
                }
            }
        }

        #region Comment

        // http://stackoverflow.com/questions/33895403/wpf-radgridview-loop-through-rows-cant-get-all-rows/33916970#33916970
        //The reason why didn't work. But this method can be reused in another thing.


        //private List<object> ReturnListFounds(string text)
        //{
        //    List<object> result = new List<object>();
        //    int index = 0;

        //    foreach (var item in this.Items)
        //    {

        //        if (item == null)
        //            continue;

        //        GridViewRow row = this.ItemContainerGenerator.ContainerFromItem(item) as GridViewRow;

        //        if (row == null)
        //        {
        //            row = this.ItemContainerGenerator.ContainerFromIndex(index) as GridViewRow;
        //            if (row == null)
        //            {
        //                index++;
        //                continue;
        //            }

        //        }

        //        foreach (GridViewCell cell in row.Cells)
        //        {
        //            if (cell != null && cell.Value != null)
        //            {
        //                string str = cell.Value.ToString();

        //                if (str.Equals(text, StringComparison.InvariantCultureIgnoreCase) || str.ToLower().Contains(text.ToLower()))
        //                {
        //                    result.Add(row.Item);
        //                    break;
        //                }
        //            }
        //        }
        //        index++;
        //    }

        //    result = new List<object>(result.Distinct());

        //    return result;
        //}
        #endregion


        private List<object> ReturnListFounds(string text)
        {
            List<object> result = new List<object>();

            for (int l = 0; l <= Items.Count; l++)
            {
                var cell = new GridViewCellInfo(this.Items[l], this.Columns[0], this);

                if (cell.Item != null)
                {
                    var props = cell.Item.GetType().GetProperties();

                    foreach (var p in props)
                    {
                        if (p == null || cell.Item == null)
                            continue;

                        var t = p.GetValue(cell.Item);

                        
                        
                        if (t == null)
                            continue;

                        var str = t.ToString();
                        if (str.Equals(text, StringComparison.InvariantCultureIgnoreCase) || str.ToLower().Contains(text))
                        {
                            result.Add(cell.Item);
                        }
                    }
                }
            }

            result = new List<object>(result.Distinct());

            return result;
        }

        private void SetFocusOnTxt()
        {
            _btnLocalizar.IsTextFocused = true;
            Keyboard.Focus(_btnLocalizar.TextBox);
            _btnLocalizar.TextBox.Focus();
        }

        private void FindNext(string text)
        {
            if (isTheFirstClick)
            {
                findOriginal = text;
                _itemsFind = ReturnListFounds(text);

                //Se não achou nenhum resultado: mostra a mensagem, fecha dropdown e sai do método.
                if (_itemsFind.Count <= 0)
                {
                    ShowRadAlert(NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("SemResultados"));                    
                    CloseDropDownLocalizar();
                    return;
                }

                //Adiciona o primeiro item encontrado na lista de SelectedItems
                this.SelectedItems.Add(_itemsFind.FirstOrDefault());
                _lastFind = _itemsFind.FirstOrDefault();

                //Posiciona o grid no item que foi selecionado.
                ScrollIntoView();

                isTheFirstClick = false;

                //Remove o primeiro item que foi encontrado, já que o mesmo esta como item selecionado.
                _itemsFind.RemoveAt(0);

                //SetFocusOnTxt();
            }
            else
            {
                //Valida se o texto digitado é o mesmo da primeira vez.
                if (text.Equals(findOriginal, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (_itemsFind.Count <= 0)
                    {
                        //Como não tem resultados, matém o item selecionado posicionado no ultimo item encontrado e fecha o dropdown.
                        //ScrollIntoView();
                        ShowRadAlert(NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("SemMaisResultados"));
                        CloseDropDownLocalizar();

                        return;
                    }
                    else
                    {
                        this.SelectedItems.Clear();
                        this.SelectedItems.Add(_itemsFind.FirstOrDefault());
                        _lastFind = _itemsFind.FirstOrDefault();
                        ScrollIntoView();
                        _itemsFind.RemoveAt(0);
                        //SetFocusOnTxt();
                    }
                }
                else
                {
                    //Chama o método de novo, como se fosse a primeira vez, pois o texto digitado para busca é diferente do original.
                    isTheFirstClick = true;
                    findOriginal = "";
                    FindNext(text);
                    return;
                }
            }

            #region Comment Pintar linha

            //Pintar linha selecionada.
            //Não está funcionando 100%

            //GridViewRow row = null;
            //SortDescriptors.Reset();

            ////System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            ////{
            //    row = this.GetRowForItem(first);

            //    if (row != null)
            //    {
            //        row.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#CC082B"));
            //        row.Foreground = Brushes.White;
            //    }

            // }));
            #endregion


        }
        private void ScrollIntoView()
        {
            object first = SelectedItems.FirstOrDefault();
            if (first != null)
                this.ScrollIndexIntoView(this.Items.IndexOf(first));
            else
                this.ScrollIndexIntoView(0);

            SetFocusOnTxt();
        }
        private void ShowRadAlert(string msg)
        {
            string alerta = NL.GI.ComponentesWPF.Cliente.Resources.Resources.ResourceManager.GetString("Alerta");
            RadWindow.Alert(new DialogParameters { Header = alerta, Content = msg });
        }
        protected void DoLocalizar()
        {
            this.SelectedItems.Clear();

            FindNext(FindText);
        }
        protected bool CanLocalizar()
        {
            return true;
        }

        #endregion

        #region Print - PDF - Excel
        private void ExportAs(ExportType type)
        {
            if (type == ExportType.Excel)
                ExportaExcel();
            else if (type == ExportType.PDF)
                ExportaPDF();
        }
        protected void DoImprimir()
        {
            PrintExtensions.Print(this);
        }
        protected bool CanImprimir()
        {
            return true;
        }
        protected void DoPrintPDF()
        {
            //ExportaPDF();
            PrintExtensions.ExportPDF(this, null);
            //MessageBox.Show("Print PDF!");
        }
        protected void DoPrint()
        {
            ExportaExcel();

            //MessageBox.Show("Print Excel!");
        }
        protected void ExportaExcel()
        {                        
            string extension = "xls";
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog()
            {
                DefaultExt = extension,
                Filter = String.Format("{1} files (*.{0})|*.{0}|Excell Files (*.xls)|*.xls", extension, "Excel"),
                FilterIndex = 2,
                Title = "Selecione o local onde deseja exportar."
            };


            var dialogRes = saveFileDialog1.ShowDialog();

            if (dialogRes == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = saveFileDialog1.OpenFile())
                {
                    Telerik.Windows.Controls.GridViewExportOptions exp = new Telerik.Windows.Controls.GridViewExportOptions();
                    exp.ShowColumnFooters = true;
                    exp.ShowColumnHeaders = true;
                    exp.ShowGroupFooters = true;

                    this.Export(stream, exp);
                }
            }
        }
        protected void ExportaPDF()
        {

            var document = CreateDocument(this, null);

            if (document != null)
            {
                document.LayoutMode = DocumentLayoutMode.Paged;
                document.Measure(RadDocument.MAX_DOCUMENT_SIZE);
                document.Arrange(new RectangleF(PointF.Empty, document.DesiredSize));

                IDocumentFormatProvider provider = new PdfFormatProvider();

                var dialog = new System.Windows.Forms.SaveFileDialog();
                dialog.DefaultExt = "*.pdf";
                dialog.Filter = "Adobe PDF Document (*.pdf)|*.pdf";

                var dialogRes = dialog.ShowDialog();

                if (dialogRes == System.Windows.Forms.DialogResult.OK)
                {
                    using (var output = dialog.OpenFile())
                    {
                        provider.Export(document, output);
                    }
                }
            }


            //string extension = "pdf";

            //System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog()
            //{
            //    DefaultExt = extension,
            //    Filter = String.Format("{1} files (*.{0})|*.{0}|Pdf files (*.pdf)|*.pdf", extension, "Pdf"),
            //    FilterIndex = 2,
            //    Title = "Selecione o local onde deseja exportar."
            //};



            //if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    using (Stream stream = saveFileDialog1.OpenFile())
            //    {
            //        Telerik.Windows.Controls.GridViewExportOptions exp = new Telerik.Windows.Controls.GridViewExportOptions();
            //        exp.ShowColumnFooters = true;
            //        exp.ShowColumnHeaders = true;
            //        exp.ShowGroupFooters = true;

            //        this.Export(stream, exp);
            //    }
            //}
        }

        private static RadDocument CreateDocument(RadGridView grid, PrintSettings settings)
        {
            RadDocument document = null;

            using (var stream = new MemoryStream())
            {
                grid.Export(stream, new GridViewExportOptions()
                {
                    Format = ExportFormat.Html,
                    ShowColumnFooters = grid.ShowColumnFooters,
                    ShowColumnHeaders = grid.ShowColumnHeaders,
                    ShowGroupFooters = grid.ShowGroupFooters
                });


                stream.Position = 0;

                document = new HtmlFormatProvider().Import(stream);
            }

            return document;
        }

        protected bool CanPrint()
        {
            return true;
        }
        #endregion

        #region Dependency Properties

        #region Print
        public ICommand CmdPrint {  get {return _cmdPrint; } set { _cmdPrint = value; } }
        public ICommand CmdPrintPDF { get { return _cmdPrintPDF; } set { _cmdPrintPDF = value; } }
        #endregion

        #region Novo
        public static readonly DependencyProperty CommandNovoProperty = DependencyProperty.Register(
              "CommandNovo",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

         [Bindable(true)]
        public ICommand CommandNovo
        {
            get { return (ICommand)GetValue(CommandNovoProperty); }
            set { SetValue(CommandNovoProperty, value); }
        }

         public static readonly DependencyProperty VisibilityNovoProperty = DependencyProperty.Register(
              "VisibilityNovo",
              typeof(Visibility),
              typeof(GIGridToolbarCadastro),
              new PropertyMetadata(Visibility.Visible));

         [Bindable(true)]
         public Visibility VisibilityNovo
         {
             get { return (Visibility)GetValue(VisibilityNovoProperty); }
             set { SetValue(VisibilityNovoProperty, value); }
         }

        #endregion

        #region Editar

        public static readonly DependencyProperty CommandEditarProperty = DependencyProperty.Register(
              "CommandEditar",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

         [Bindable(true)]
        public ICommand CommandEditar
        {
            get { return (DelegateCommand)GetValue(CommandEditarProperty); }
            set { SetValue(CommandEditarProperty, value); }
        }

         public static readonly DependencyProperty VisibilityEditarProperty = DependencyProperty.Register(
              "VisibilityEditar",
              typeof(Visibility),
              typeof(GIGridToolbarCadastro),
              new PropertyMetadata(Visibility.Visible));

         [Bindable(true)]
         public Visibility VisibilityEditar
         {
             get { return (Visibility)GetValue(VisibilityEditarProperty); }
             set { SetValue(VisibilityEditarProperty, value); }
         }


        #endregion

        #region Excluir

        public static readonly DependencyProperty CommandExcluirProperty = DependencyProperty.Register(
              "CommandExcluir",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

         [Bindable(true)]
        public ICommand CommandExcluir
        {
            get { return (DelegateCommand)GetValue(CommandExcluirProperty); }
            set { SetValue(CommandExcluirProperty, value); }
        }

         public static readonly DependencyProperty VisibilityExcluirProperty = DependencyProperty.Register(
               "VisibilityExcluir",
               typeof(Visibility),
               typeof(GIGridToolbarCadastro),
               new PropertyMetadata(Visibility.Visible));

         [Bindable(true)]
         public Visibility VisibilityExcluir
         {
             get { return (Visibility)GetValue(VisibilityExcluirProperty); }
             set { SetValue(VisibilityExcluirProperty, value); }
         }

        #endregion

        #region Salvar

        public static readonly DependencyProperty CommandSalvarProperty = DependencyProperty.Register(
              "CommandSalvar",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

         [Bindable(true)]
        public ICommand CommandSalvar
        {
            get { return (DelegateCommand)GetValue(CommandSalvarProperty); }
            set { SetValue(CommandSalvarProperty, value); }
        }

        public static readonly DependencyProperty VisibilitySalvarProperty = DependencyProperty.Register(
               "VisibilitySalvar",
               typeof(Visibility),
               typeof(GIGridToolbarCadastro),
               new PropertyMetadata(Visibility.Visible));

         [Bindable(true)]
         public Visibility VisibilitySalvar
         {
             get { return (Visibility)GetValue(VisibilitySalvarProperty); }
             set { SetValue(VisibilitySalvarProperty, value); }
         }


        #endregion

        #region Atualizar

        public static readonly DependencyProperty CommandAtualizarProperty = DependencyProperty.Register(
              "CommandAtualizar",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

         [Bindable(true)]
        public ICommand CommandAtualizar
        {
            get { return (DelegateCommand)GetValue(CommandAtualizarProperty); }
            set { SetValue(CommandAtualizarProperty, value); }
        }

         public static readonly DependencyProperty VisibilityAtualizarProperty = DependencyProperty.Register(
               "VisibilityAtualizar",
               typeof(Visibility),
               typeof(GIGridToolbarCadastro),
               new PropertyMetadata(Visibility.Visible));

         [Bindable(true)]
         public Visibility VisibilityAtualizar
         {
             get { return (Visibility)GetValue(VisibilityAtualizarProperty); }
             set { SetValue(VisibilityAtualizarProperty, value); }
         }

        #endregion

        // O item customizado tem como padrão visibility = collapsed. Caso o usuário precise utilizar esse botão,
        //deve colocar a propriedade VisibilityCustomizado como True e atribuir valores às outras propriedades,
        // como DescricaoCustomizado (que é o content do botao), Image e ImageOver (que são as imagens do botão
        // e Command (que define a ação do botão através de um ICommand).

        #region Customizado

        #region Command

        public static readonly DependencyProperty CommandCustomizadoProperty = DependencyProperty.Register(
              "CommandCustomizado",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro));

         [Bindable(true)]
        public ICommand CommandCustomizado
        {
            get { return (DelegateCommand)GetValue(CommandCustomizadoProperty); }
            set { SetValue(CommandCustomizadoProperty, value); }
        }

        #endregion

        #region Descricao

        public static readonly DependencyProperty DescricaoCustomizadoProperty = DependencyProperty.Register(
              "DescricaoCustomizado",
              typeof(string),
              typeof(GIGridToolbarCadastro));

         [Bindable(true)]
        public String DescricaoCustomizado
        {
            get { return (String)GetValue(DescricaoCustomizadoProperty); }
            set { SetValue(DescricaoCustomizadoProperty, value); }
        }

        #endregion

        #region Images

        #region Image

        public static readonly DependencyProperty ImageCustomizadoProperty = DependencyProperty.Register(
              "ImageCustomizado",
              typeof(ImageSource),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

         [Bindable(true)]
        public ImageSource ImageCustomizado
        {
            get { return (ImageSource)GetValue(ImageCustomizadoProperty); }
            set { SetValue(ImageCustomizadoProperty, value); }
        }

        #endregion

        #region ImageOver

        public static readonly DependencyProperty ImageOverCustomizadoProperty = DependencyProperty.Register(
              "ImageOverCustomizado",
              typeof(ImageSource),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

         [Bindable(true)]
        public ImageSource ImageOverCustomizado
        {
            get { return (ImageSource)GetValue(ImageOverCustomizadoProperty); }
            set { SetValue(ImageOverCustomizadoProperty, value); }
        }

        #endregion

        #region ImagePressed

         public static readonly DependencyProperty ImagePressedCustomizadoProperty = DependencyProperty.Register(
               "ImagePressedCustomizado",
               typeof(ImageSource),
               typeof(GIGridToolbarCadastro),
               new UIPropertyMetadata(null));

         [Bindable(true)]
         public ImageSource ImagePressedCustomizado
         {
             get { return (ImageSource)GetValue(ImagePressedCustomizadoProperty); }
             set { SetValue(ImagePressedCustomizadoProperty, value); }
         }

        #endregion

        #endregion

        #region Visibility

         public static readonly DependencyProperty VisibilityCustomizadoProperty = DependencyProperty.Register(
             "VisibilityCustomizado",
             typeof(Visibility),
             typeof(GIGridToolbarCadastro),
             new PropertyMetadata(Visibility.Collapsed));
         private ICommand _cmdPrint;
         private ICommand _cmdPrintPDF;

        [Bindable(true)]
        public Visibility VisibilityCustomizado
        {
            get { return (Visibility)GetValue(VisibilityCustomizadoProperty); }
            set { SetValue(VisibilityCustomizadoProperty, value); }
        }


        #endregion


        #endregion

        #region Exportar
        public static readonly DependencyProperty VisibilityExportarProperty = DependencyProperty.Register(
               "VisibilityExportar",
               typeof(Visibility),
               typeof(GIGridToolbarCadastro),
               new PropertyMetadata(Visibility.Visible));

        [Bindable(true)]
        public Visibility VisibilityExportar
        {
            get { return (Visibility)GetValue(VisibilityExportarProperty); }
            set { SetValue(VisibilityExportarProperty, value); }
        }
        #endregion

        #region Localizar

        private ICommand _commandLocalizar;

        [Bindable(true)]
        public ICommand CommandLocalizar
        {
            get { return _commandLocalizar; }
            set { _commandLocalizar = value; }
        }

        private string _findText;

        [Bindable(true)]
        public String FindText
        {
            get { return _findText; }
            set 
            { 
                _findText = value;

                //if(!string.IsNullOrEmpty(_findText))
                //    BtnLocalizarEnabled = true;
                //else
                //    BtnLocalizarEnabled = false;
            }
        }

        public static readonly DependencyProperty VisibilityLocalizarProperty = DependencyProperty.Register(
               "VisibilityLocalizar",
               typeof(Visibility),
               typeof(GIGridToolbarCadastro),
               new PropertyMetadata(Visibility.Visible));

        [Bindable(true)]
        public Visibility VisibilityLocalizar
        {
            get { return (Visibility)GetValue(VisibilityLocalizarProperty); }
            set { SetValue(VisibilityLocalizarProperty, value); }
        }

        bool _btnLocalizarEnabled;
        [Bindable(true)]
        public bool BtnLocalizarEnabled
        {
            get { return _btnLocalizarEnabled; }
            set { _btnLocalizarEnabled = value; }
        }

        #endregion

        #region Imprimir

        private ICommand _commandImprimir;

        [Bindable(true)]
        public ICommand CommandImprimir
        {
            get { return _commandImprimir; }
            set { _commandImprimir = value; }
        }

        public static readonly DependencyProperty VisibilityImprimirProperty = DependencyProperty.Register(
               "VisibilityImprimir",
               typeof(Visibility),
               typeof(GIGridToolbarCadastro),
               new PropertyMetadata(Visibility.Visible));

        [Bindable(true)]
        public Visibility VisibilityImprimir
        {
            get { return (Visibility)GetValue(VisibilityImprimirProperty); }
            set { SetValue(VisibilityImprimirProperty, value); }
        }

        #endregion

        #region Outros

        #region Mudar senha (Usuario)

        public static readonly DependencyProperty CommandMudarSenhaUsuarioProperty = DependencyProperty.Register(
              "CommandMudarSenhaUsuario",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandMudarSenhaUsuario
        {
            get { return (DelegateCommand)GetValue(CommandMudarSenhaUsuarioProperty); }
            set { SetValue(CommandMudarSenhaUsuarioProperty, value); }
        }

        #endregion

        #region Mudar senha (Email)

        public static readonly DependencyProperty CommandMudarSenhaEmailProperty = DependencyProperty.Register(
              "CommandMudarSenhaEmail",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandMudarSenhaEmail
        {
            get { return (DelegateCommand)GetValue(CommandMudarSenhaEmailProperty); }
            set { SetValue(CommandMudarSenhaEmailProperty, value); }
        }

        #endregion

        #region Acesso

        public static readonly DependencyProperty CommandAcessoProperty = DependencyProperty.Register(
              "CommandAcesso",
              typeof(ICommand),
              typeof(GIGridToolbarCadastro),
              new UIPropertyMetadata(null));

        [Bindable(true)]
        public ICommand CommandAcesso
        {
            get { return (DelegateCommand)GetValue(CommandAcessoProperty); }
            set { SetValue(CommandAcessoProperty, value); }
        }

        #endregion

        public static readonly DependencyProperty VisibilityOutrosProperty = DependencyProperty.Register(
              "VisibilityOutros",
              typeof(Visibility),
              typeof(GIGridToolbarCadastro),
              new PropertyMetadata(Visibility.Collapsed));

        [Bindable(true)]
        public Visibility VisibilityOutros
        {
            get { return (Visibility)GetValue(VisibilityOutrosProperty); }
            set { SetValue(VisibilityOutrosProperty, value); }
        }

        #endregion

        #endregion
    }
    
}
