using System.Collections.Generic;
using System.ComponentModel;

namespace NL.GI.ComponentesWPF.Cliente
{
    public interface ITreeViewItemModel : INotifyPropertyChanged
    {
        string SelectedValuePath { get; }

        string DisplayValuePath { get; }

        bool IsExpanded { get; set; }

        bool IsSelected { get; set; }
        int CodInt { get; set; }
        //TipoTree TipoTreeP { get; set; }
        bool IsChecked { get; set; }

        IEnumerable<ITreeViewItemModel> GetHierarchy();

        IEnumerable<ITreeViewItemModel> GetChildren();
    }
}