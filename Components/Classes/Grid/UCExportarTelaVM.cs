using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NL.GI.ComponentesWPF.Cliente.Classes.Grid
{
    public class UCExportarTelaVM : BindableBase
    {
        private bool _isExcelChecked = true;
        private bool _isPDFChecked = false;
        private DelegateCommand _cmdOk;
        private IEventAggregator _ea = Factory.CriaEventAggregator();

        public bool IsExcelChecked 
        {
            get { return _isExcelChecked; }
            set { SetProperty(ref _isExcelChecked, value); }
        }
        public bool IsPDFChecked 
        {
            get { return _isPDFChecked; }
            set { SetProperty(ref _isPDFChecked, value); }
        }

        public ICommand CmdOk 
        {
            get { return _cmdOk; }
            set { _cmdOk = (DelegateCommand)value; }
        }

        public UCExportarTelaVM()
        {
            this.CmdOk = new DelegateCommand(OnOk, CanOk);
        }

        private void OnOk()
        {
            if (IsPDFChecked)
                _ea.GetEvent<ExportEvent>().Publish(ExportType.PDF);
            else if (IsExcelChecked)
                _ea.GetEvent<ExportEvent>().Publish(ExportType.Excel);
        }

        private bool CanOk()
        {
            return (IsExcelChecked || IsPDFChecked);
        }
    }
}
