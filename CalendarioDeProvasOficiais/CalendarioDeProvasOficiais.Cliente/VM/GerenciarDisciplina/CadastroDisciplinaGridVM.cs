using CalendarioDeProvasOficiais.Cliente.View.GerenciarDisciplina;
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

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarDisciplina
{
    public class CadastroDisciplinaGridVM : BindableBase
    {
        #region Variaveis
        private ObservableCollection<DisciplinaView> _listDisciplina;
        private DisciplinaView _disciplinaSelecionada;
        private DelegateCommand _cmdNovo;
        private DelegateCommand _cmdEditar;
        private DelegateCommand _cmdAtualizar;
        private DelegateCommand _cmdExcluir;
        #endregion

        #region Propriedades
        public DisciplinaView DisciplinaSelecionada
        {
            get { return _disciplinaSelecionada; }
            set { SetProperty(ref _disciplinaSelecionada, value); _cmdEditar.RaiseCanExecuteChanged(); _cmdExcluir.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<DisciplinaView> ListDisciplina
        {
            get { return _listDisciplina; }
            set { SetProperty(ref _listDisciplina, value); }
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

        #region Métodos

        public CadastroDisciplinaGridVM()
        {
            _cmdNovo = new DelegateCommand(OnNovo);
            _cmdEditar = new DelegateCommand(OnEditar, CanEditar);
            _cmdAtualizar = new DelegateCommand(OnAtualizar);
            _cmdExcluir = new DelegateCommand(OnExcluir, CanExcluir);

            PreencheListDisciplina();
        }

        private void PreencheListDisciplina()
        {
            ListDisciplina = new ObservableCollection<DisciplinaView>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaDisciplinaView());
            DisciplinaSelecionada = ListDisciplina.FirstOrDefault();
        }
        private void OnNovo()
        {
            DetalheDisciplina window = new DetalheDisciplina(new Disciplina(), false, null);
            window.ShowDialog();
            PreencheListDisciplina();
        }
        private void OnEditar()
        {
            List<Professor> _prof = new List<Professor>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProfessoresQueLecionamDisciplina(DisciplinaSelecionada.Codigo));
            DetalheDisciplina window = new DetalheDisciplina(DisciplinaSelecionada.Disciplina, true, _prof);
            window.ShowDialog();
            PreencheListDisciplina();
        }

        private bool CanEditar()
        {
            return DisciplinaSelecionada != null;
        }
        private void OnAtualizar()
        {
            PreencheListDisciplina();
        }
        private void OnExcluir()
        {
            //Verificar se curso está vinculado a alguma prova
            //se sim, avisar que curso ta vinculado a alguma prova e nao pode excluir
            //se nao
            //perguntar se deseja mesmo excluir o curso X
            if (CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaSeDisciplinaVinculadaAAlgumaProva(DisciplinaSelecionada.Codigo))
            {
                MessageBox.Show("A disciplina selecionada não pode ser excluída pois está vinculada a uma ou mais provas.");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Tem certeza que deseja excluir essa disciplina?", "Excluir Disciplina", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        CalendarioDeProvasOficiais.Cliente.Classes.BD.DeletarDisciplina(DisciplinaSelecionada.Disciplina);
                        MessageBox.Show("Disciplina deletada com sucesso!");
                        OnAtualizar();
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao deletar disciplina. Tente novamente mais tarde.");
                    }

                }
            }


        }
        private bool CanExcluir()
        {
            return DisciplinaSelecionada != null;
        }

        #endregion
    }
}
