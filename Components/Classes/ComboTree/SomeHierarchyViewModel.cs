using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class SomeHierarchyViewModel : ITreeViewItemModel
    {
        public SomeHierarchyViewModel(string title, List<SomeHierarchyViewModel> children, SomeHierarchyViewModel parent = null)
        {
            this.Title = title;
            this.Parent = parent;
            this.Children = children;

            if (this.Children != null)
            {
                this.Children.ForEach(ch => ch.Parent = this);
            }
        }

        public string Title { get; set; }

        public SomeHierarchyViewModel Parent { get; set; }

        public List<SomeHierarchyViewModel> Children { get; set; }


        public CM_CTABM ItemAtual { get; set; }
        public int CodInt { get; set; }
        //public TipoTree TipoTreeP { get; set; }
        public bool IsChecked { get; set; }

        #region ITreeViewItemModel
        public string SelectedValuePath
        {
            get { return Title; }
        }

        public string DisplayValuePath
        {
            get { return Title; }
        }

        private bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public IEnumerable<ITreeViewItemModel> GetHierarchy()
        {
            return GetAscendingHierarchy().Reverse();
        }

        public IEnumerable<ITreeViewItemModel> GetChildren()
        {
            return this.Children;
        }

        #endregion

        private IEnumerable<SomeHierarchyViewModel> GetAscendingHierarchy()
        {
            var vm = this;

            yield return vm;
            while (vm.Parent != null)
            {
                yield return vm.Parent;
                vm = vm.Parent;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
