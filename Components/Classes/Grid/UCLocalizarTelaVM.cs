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
    public class UCLocalizarTelaVM : BindableBase
    {
        private string _texto;
        private DelegateCommand _cmdFechar, _cmdLocalizar;
        private IEventAggregator _ea = Factory.CriaEventAggregator();

        public string Texto 
        {
            get { return _texto; }
            set 
            { 
                SetProperty(ref _texto, value);
                this._cmdLocalizar.RaiseCanExecuteChanged();
            }
        }

        public ICommand CmdFechar 
        {
            get { return _cmdFechar; }
            set { _cmdFechar = (DelegateCommand)value; }
        }

        public ICommand CmdLocalizar
        {
            get { return _cmdLocalizar; }
            set { _cmdLocalizar = (DelegateCommand)value; }
        }

        public UCLocalizarTelaVM()
        {
            this._cmdFechar = new DelegateCommand(DoFechar);
            this._cmdLocalizar = new DelegateCommand(DoLocalizar, CanLocalizar);
        }

        private bool CanLocalizar()
        {
            return !string.IsNullOrEmpty(Texto);
        }

        private void DoLocalizar()
        {
            _ea.GetEvent<FindEvent>().Publish(Texto);
        }

        private void DoFechar()
        {
            
        }
    }
}
