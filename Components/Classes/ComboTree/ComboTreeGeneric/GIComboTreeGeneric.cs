using NL.GI.ComponentesWPF.Cliente.Classes;
using NL.GI.ComponentesWPF.Cliente.Classes.ComboTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class GIComboTreeGeneric : ComboBox
    {
        ExtendedTreeView _treeView;
        ContentPresenter _contentPresenter;
        //bool _isSelectedItemSetFromOutside;
        bool _hasToBeLoadedAgain = true;

        public GIComboTreeGeneric()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(
            //    typeof(GIComboTreeGeneric),
            //    new FrameworkPropertyMetadata(typeof(GIComboTreeGeneric)));

            this.DefaultStyleKey = typeof(GIComboTreeGeneric);
        }

        #region Methods Default

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //don't call the method of the base class
        }
        protected override void OnDropDownClosed(EventArgs e)
        {
            if (_treeView != null)
            {
                base.OnDropDownClosed(e);
                SetSelectedItem(_treeView.SelectedItem);
                this.SetSelectedItemToHeader();
            }
        }
        protected override void OnDropDownOpened(EventArgs e)
        {
            base.OnDropDownOpened(e);
            DropOpened();
        }
        //protected override On
        /// <summary>
        /// Handles clicks on any item in the tree view
        /// </summary>
        private void OnTreeViewHierarchyMouseUp(object sender, MouseEventArgs e)
        {
            //This line isn't obligatory because it is executed in the OnDropDownClosed method, but be it so
            SetSelectedItem(_treeView.SelectedItem);

            this.IsDropDownOpen = false;
        }
        /// <summary>
        /// Selected item of the TreeView
        /// </summary>
        [Bindable(true)]
        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set
            {
                //SetValue(SelectedItemProperty, value);

                //if (_isSelectedItemSetFromOutside)
                //{
                //    try
                //    {
                //        SomeHierarchyViewModel aux = (SomeHierarchyViewModel)value;
                //        GIComboTreeUtils.RetornaItemDaTree(this.Items.OfType<SomeHierarchyViewModel>().ToList(), aux.ItemAtual.CodInt);
                //    }
                //    catch
                //    {
                //        //do nothing
                //    }
                //}


            }
        }
        //public new static readonly DependencyProperty SelectedItemProperty =
            //DependencyProperty.Register("SelectedItem", typeof(object), typeof(GIComboTreeGeneric), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemChanged)));
        //private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    ((GIComboTreeGeneric)sender).UpdateSelectedItem();
        //}

        public new static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem",
        typeof(object),
        typeof(GIComboTreeGeneric), new UIPropertyMetadata(SelectedItemChanged));
        public static void SelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            GIComboTreeGeneric t = ((GIComboTreeGeneric)sender);
            t.SelectedItem = e.NewValue;            
            t.UpdateSelectedItem();
            t.SetSelectedItemToHeader();
        }

        /// <summary>
        /// Selected hierarchy of the treeview
        /// </summary>
        public IEnumerable<string> SelectedHierarchy
        {
            get { return (IEnumerable<string>)GetValue(SelectedHierarchyProperty); }
            set { SetValue(SelectedHierarchyProperty, value); }
        }
        public static readonly DependencyProperty SelectedHierarchyProperty =
            DependencyProperty.Register("SelectedHierarchy", typeof(IEnumerable<string>), typeof(GIComboTreeGeneric), new PropertyMetadata(null, OnSelectedHierarchyChanged));
        private static void OnSelectedHierarchyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((GIComboTreeGeneric)sender).UpdateSelectedHierarchy();
        }
        private void UpdateSelectedItem()
        {
            if (this.SelectedItem is TreeViewItem)
            {
                //I would rather use a correct object instead of TreeViewItem
                SetSelectedItem(((TreeViewItem)this.SelectedItem).DataContext);
            }
            else
            {
                //Update the selected hierarchy and displays
                var model = this.SelectedItem as ITreeViewItemModel;
                if (model != null)
                {
                    this.SelectedHierarchy = model.GetHierarchy().Select(h => h.SelectedValuePath).ToList();
                }

                this.SetSelectedItemToHeader();
            }
        }
        protected void UpdateSelectedHierarchy()
        {
            if (ItemsSource != null && this.SelectedHierarchy != null)
            {
                //Find corresponding items and expand or select them
                var source = this.ItemsSource.OfType<ITreeViewItemModel>();
                var item = SelectItem(source, this.SelectedHierarchy);
                SetSelectedItem(item);
                this.CodInt = item.CodInt;
            }
        }
        /// <summary>
        /// Searches the items of the hierarchy inside the items source and selects the last found item
        /// </summary>
        private static ITreeViewItemModel SelectItem(IEnumerable<ITreeViewItemModel> items, IEnumerable<string> selectedHierarchy)
        {
            if (items == null || selectedHierarchy == null || !items.Any() || !selectedHierarchy.Any())
            {
                return null;
            }

            var hierarchy = selectedHierarchy.ToList();
            var currentItems = items;
            ITreeViewItemModel selectedItem = null;

            for (int i = 0; i < hierarchy.Count; i++)
            {
                // get next item in the hierarchy from the collection of child items
                var currentItem = currentItems.FirstOrDefault(ci => ci.SelectedValuePath == hierarchy[i]);
                if (currentItem == null)
                {
                    break;
                }

                selectedItem = currentItem;

                // rewrite the current collection of child items
                currentItems = selectedItem.GetChildren();
                if (currentItems == null)
                {
                    break;
                }

                // the intermediate items will be expanded
                if (i != hierarchy.Count - 1)
                {
                    selectedItem.IsExpanded = true;
                }
            }

            if (selectedItem != null)
            {
                selectedItem.IsSelected = true;
            }

            return selectedItem;
        }
        /// <summary>
        /// Gets the hierarchy of the selected tree item and displays it at the combobox header
        /// </summary>
        private void SetSelectedItemToHeader()
        {
            string content = null;

            var item = this.SelectedItem as ITreeViewItemModel;
            if (item != null)
            {
                //Get hierarchy and display it as the selected item
                //var hierarchy = item.GetHierarchy().Select(i => i.DisplayValuePath).ToArray();
                //if (hierarchy.Length > 0)
                //{
                //    content = string.Join(" - ", hierarchy);
                //}

                content = item.DisplayValuePath;
            }

            this.SetContentAsTextBlock(content);
        }
        /// <summary>
        /// Gets the combobox header and displays the specified content there
        /// </summary>
        private void SetContentAsTextBlock(string content)
        {
            if (_contentPresenter == null)
            {
                return;
            }

            var tb = _contentPresenter.Content as TextBlock;
            if (tb == null)
            {
                _contentPresenter.Content = tb = new TextBlock();
            }
            tb.Text = content ?? ' '.ToString();

            _contentPresenter.ContentTemplate = null;
        }

        #endregion        

        #region Methods

        public void PreencheCombo(bool setSelectedItem = true)
        {
            if (!_hasToBeLoadedAgain)
                return;

            List<SomeHierarchyViewModel> l = GIComboTreeUtils.ReturnTree(TabG);

            if (l.Count > 0)
            {
                this.ItemsSource = l;
                //AllItems = new ObservableCollection<SomeHierarchyViewModel>(l);
                //SetCurrentValue(AllItemsProperty, new ObservableCollection<SomeHierarchyViewModel>(l));
                if(setSelectedItem)
                CodInt = 0;
                //SomeHierarchyViewModel sh = (SomeHierarchyViewModel)SelectedItem;

                //if (sh == null)
                //    SetSelectedItem(Items[0]);
                //else
                //    SetSelectedItem(sh);

                _hasToBeLoadedAgain = false;
            }

        }
        private void SetSelectedItem(object item)
        {
            //_isSelectedItemSetFromOutside = false;
            //SelectedItem = item;
            //_isSelectedItemSetFromOutside = true;
            SetCurrentValue(SelectedItemProperty, item);

        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _treeView = (ExtendedTreeView)this.GetTemplateChild("treeView");
            _treeView.OnHierarchyMouseUp += new MouseEventHandler(OnTreeViewHierarchyMouseUp);
            _contentPresenter = (ContentPresenter)this.GetTemplateChild("ContentPresenter");

            this.SetSelectedItemToHeader();

            if (IsAlreadyLoaded)
                DropOpened();


            IsDropDownOpen = false;
        }
        protected void DropOpened()
        {
            PreencheCombo();
            this.SetSelectedItemToHeader();
            UpdateSelectedHierarchy();
        }

        #endregion

        #region Properties

        public static DependencyProperty CodIntProperty =
          DependencyProperty.Register(
              "CodInt",
              typeof(int),
              typeof(GIComboTreeGeneric),
              new UIPropertyMetadata(CodIntChanged));

        [Bindable(true)]
        public int CodInt
        {
            get { return (int)GetValue(CodIntProperty); }
            set 
            {
                SetValue(CodIntProperty, value);

                if (IsAlreadyLoaded && Items.Count == 0 && value > 0)
                    PreencheCombo(false);

                SomeHierarchyViewModel y = GIComboTreeUtils.RetornaItemDaTree(this.Items.OfType<SomeHierarchyViewModel>().ToList(), value);
                SetSelectedItem(y);
                
                
            }
        }

        public static void CodIntChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((GIComboTreeGeneric)sender).CodInt = (int)e.NewValue;
        }

        public static DependencyProperty TabGProperty =
           DependencyProperty.Register(
               "TabG",
               typeof(int),
               typeof(GIComboTreeGeneric), new PropertyMetadata(0));
        public int TabG
        {
            get { return (int)GetValue(TabGProperty); }
            set
            {
                SetValue(TabGProperty, value);
                _hasToBeLoadedAgain = true;
                DropOpened();
            }
        }
        //[Bindable(true)]
        //public static DependencyProperty AllItemsProperty =
        //   DependencyProperty.Register(
        //       "AllItems",
        //       typeof(ObservableCollection<SomeHierarchyViewModel>),
        //       typeof(GIComboTreeGeneric), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        //[Bindable(true)]
        //public ObservableCollection<SomeHierarchyViewModel> AllItems
        //{
        //    get { return (ObservableCollection<SomeHierarchyViewModel>)GetValue(AllItemsProperty); }
        //    set { SetValue(AllItemsProperty, value); }
        //}

        public static DependencyProperty IsAlreadyLoadedProperty =
           DependencyProperty.Register(
               "IsAlreadyLoaded",
               typeof(bool),
               typeof(GIComboTreeGeneric), new PropertyMetadata(false));
        [Bindable(true)]
        public bool IsAlreadyLoaded
        {
            get { return (bool)GetValue(IsAlreadyLoadedProperty); }
            set { SetValue(IsAlreadyLoadedProperty, value); }
        }


        #endregion
       
    }
}
