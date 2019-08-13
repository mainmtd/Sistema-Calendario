using CalendarioDeProvasOficiais.Comum.Objetos;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarSala
{
    public class DetalheSalaVM : BindableBase
    {
        #region Campos
        private string _nome;
        private Sala _sala;
        private Window _window;
        private bool _ehEdicao;
        private DelegateCommand _cmdConfirmar, _cmdCancelar;
        #endregion

        #region Propriedades
        public string Nome 
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public ICommand CmdConfirmar
        {
            get { return _cmdConfirmar; }
            set { _cmdConfirmar = (DelegateCommand)value; }
        }
        public ICommand CmdCancelar
        {
            get { return _cmdCancelar; }
            set { _cmdCancelar = (DelegateCommand)value; }
        }
        #endregion

        #region Metodos
        public DetalheSalaVM(Sala sala, Window w, bool ehEdicao)
        {
            //Cria Commands
            _cmdConfirmar = new DelegateCommand(OnConfirmar, CanConfirmar);
            _cmdCancelar = new DelegateCommand(OnCancelar);

            _ehEdicao = ehEdicao;
            _window = w;
            _sala = sala;
            Nome = sala.Nome;

        }

        private void OnConfirmar()
        {
            _sala.Nome = Nome;

            if (!CalendarioDeProvasOficiais.Cliente.Classes.BD.ValidarSeJaExisteSala(Nome))
            {
                //chama metodo de salvar ou editar
                if (_ehEdicao)
                    CalendarioDeProvasOficiais.Cliente.Classes.BD.AlterarSala(_sala);
                else
                    CalendarioDeProvasOficiais.Cliente.Classes.BD.IncluirSala(_sala);
            }
            else
            {
                MessageBox.Show("Já existe uma sala com esse nome.");
            }
           


            _window.Close();
        }
        private bool CanConfirmar()
        {
            return (!string.IsNullOrEmpty(Nome));
        }
        private void OnCancelar()
        {
            _window.Close();
        }
        #endregion
    }
}
