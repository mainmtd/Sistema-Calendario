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

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarProfessor
{
    public class DetalheProfessorVM : BindableBase
    {
        #region Campos
        private string _cPF, _nome, _cargaHoraria;
        private DelegateCommand _cmdConfirmar, _cmdCancelar;
        private Professor _professor;
        private Window _window;
        private bool _ehEdicao;
        #endregion

        #region Propriedades
        public string CPF 
        {
            get { return _cPF; }
            set { SetProperty(ref _cPF, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public string CargaHoraria
        {
            get { return _cargaHoraria; }
            set { SetProperty(ref _cargaHoraria, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
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
        public bool EhEdicao 
        {
            get { return _ehEdicao; }
            set { SetProperty(ref _ehEdicao, value); }
        }
        #endregion

        #region Métodos
        public DetalheProfessorVM(Professor prof, Window w, bool ehEdicao)
        {
            //Cria Commands
            _cmdConfirmar = new DelegateCommand(OnConfirmar, CanConfirmar);
            _cmdCancelar = new DelegateCommand(OnCancelar);

            EhEdicao = ehEdicao;

            _window = w;
            _professor = prof;
            CPF = _professor.CPF;
            Nome = _professor.Nome;
            CargaHoraria = ((int)_professor.Carga_Horaria).ToString();


        }

        private void OnConfirmar()
        {
            
            
            _professor.Nome = Nome;
            _professor.CPF = CPF;
            _professor.Carga_Horaria = double.Parse(CargaHoraria);

            //chama metodo de salvar ou editar
            if(_ehEdicao)
                CalendarioDeProvasOficiais.Cliente.Classes.BD.AlterarProf(_professor);
            else
            {
                
                if (CalendarioDeProvasOficiais.Cliente.Classes.BD.ValidaCPFExiste(CPF))
                {
                    MessageBox.Show("Já existe um professor cadastrado com esse CPF.");
                    return;
                }
                else
                {
                    CalendarioDeProvasOficiais.Cliente.Classes.BD.IncluirProf(_professor);
                }
            }
                

            _window.Close();
        }
        private bool CanConfirmar()
        {
            return !string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(CargaHoraria) && (int.Parse(CargaHoraria) > 0 && CPF.Length == 11);
        }
        private void OnCancelar()
        {
            _window.Close();
        }
        #endregion
    }
}
