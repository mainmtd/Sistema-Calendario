using CalendarioDeProvasOficiais.Cliente.View.GerenciarProfessor;
using CalendarioDeProvasOficiais.Comum.Objetos;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarProfessor
{
    public class CadastroProfessorGridVM : BindableBase
    {
        #region Variaveis
        private Professor _professorSelecionado;
        private ObservableCollection<Professor> _listProfessor;
        private DelegateCommand _cmdNovo;
        private DelegateCommand _cmdEditar;
        private DelegateCommand _cmdAtualizar;
        private DelegateCommand _cmdExcluir;
        #endregion

        #region Propriedades
        public Professor ProfessorSelecionado 
        {
            get { return _professorSelecionado; }
            set { SetProperty(ref _professorSelecionado, value); _cmdEditar.RaiseCanExecuteChanged(); _cmdExcluir.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Professor> ListProfessor 
        {
            get { return _listProfessor; }
            set { SetProperty(ref _listProfessor, value); }
        }
        public ICommand CmdNovo
        {
            get { return _cmdNovo; }
            set { _cmdNovo = (DelegateCommand)value; }
        }
        public ICommand CmdEditar
        {
            get { return _cmdEditar; }
            set { _cmdEditar = (DelegateCommand)value; }
        }
        public ICommand CmdAtualizar
        {
            get { return _cmdAtualizar; }
            set { _cmdAtualizar = (DelegateCommand)value; }
        }
        public ICommand CmdExcluir
        {
            get { return _cmdExcluir; }
            set { _cmdExcluir = (DelegateCommand)value; }
        }

        #endregion

        #region Metodos

        public CadastroProfessorGridVM()
        {
            _cmdNovo = new DelegateCommand(OnNovo);
            _cmdEditar = new DelegateCommand(OnEditar, CanEditar);
            _cmdAtualizar = new DelegateCommand(OnAtualizar);
            _cmdExcluir = new DelegateCommand(OnExcluir, CanExcluir);

            PreencheListaProfessor();
        }

        private void PreencheListaProfessor()
        {
            ListProfessor = new ObservableCollection<Professor>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosProfessores());
            ProfessorSelecionado = ListProfessor.FirstOrDefault();
        }
        private void OnNovo()
        {
            DetalheProfessor window = new DetalheProfessor(new Professor(), false);
            window.ShowDialog();
            PreencheListaProfessor();
        }
        private void OnEditar()
        {
            DetalheProfessor window = new DetalheProfessor(ProfessorSelecionado, true);
            window.ShowDialog();
            PreencheListaProfessor();
        }

        private bool CanEditar()
        {
            return ProfessorSelecionado != null;
        }
        private void OnAtualizar()
        {
            PreencheListaProfessor();
        }
        private void OnExcluir()
        {
            //Verificar se professor está vinculado a alguma prova
            //se sim, avisar que professor ta vinculado a alguma prova e nao pode excluir
            //se nao
            //perguntar se deseja mesmo excluir o professor X
            if (CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaSeProfessorVinculadoAAlgumaProva(ProfessorSelecionado.Codigo))
            {
                MessageBox.Show("O professor selecionado não pode ser excluído pois está vinculado a uma ou mais provas.");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Tem certeza que deseja excluir esse professor?", "Excluir Professor", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        CalendarioDeProvasOficiais.Cliente.Classes.BD.DeletarProf(ProfessorSelecionado);
                        MessageBox.Show("Professor deletado com sucesso!");
                        OnAtualizar();
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao deletar professor. Tente novamente mais tarde.");
                    }
                         
                }
            }
           

        }
        private bool CanExcluir()
        {
            return ProfessorSelecionado != null;
        }

        #endregion
    }
}
