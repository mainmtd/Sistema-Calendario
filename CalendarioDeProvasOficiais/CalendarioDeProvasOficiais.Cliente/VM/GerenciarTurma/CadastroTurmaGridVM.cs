using CalendarioDeProvasOficiais.Cliente.View.GerenciarTurma;
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

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarTurma
{
    public class CadastroTurmaGridVM : BindableBase
    {

        #region Variaveis
        private ObservableCollection<TurmaView> _listTurma;
        private TurmaView _turmaSelecionada;
        private DelegateCommand _cmdNovo;
        private DelegateCommand _cmdEditar;
        private DelegateCommand _cmdAtualizar;
        private DelegateCommand _cmdExcluir;
        #endregion

        #region Propriedades
        public TurmaView TurmaSelecionada
        {
            get { return _turmaSelecionada; }
            set { SetProperty(ref _turmaSelecionada, value); _cmdEditar.RaiseCanExecuteChanged(); _cmdExcluir.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<TurmaView> ListTurma
        {
            get { return _listTurma; }
            set { SetProperty(ref _listTurma, value); }
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

        public CadastroTurmaGridVM()
        {
            _cmdNovo = new DelegateCommand(OnNovo);
            _cmdEditar = new DelegateCommand(OnEditar, CanEditar);
            _cmdAtualizar = new DelegateCommand(OnAtualizar);
            _cmdExcluir = new DelegateCommand(OnExcluir, CanExcluir);

            PreencheListaTurma();
        }

        private void PreencheListaTurma()
        {
            ListTurma = new ObservableCollection<TurmaView>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTurmaView());
            TurmaSelecionada = ListTurma.FirstOrDefault();
        }
        private void OnNovo()
        {
            DetalheTurma window = new DetalheTurma(new Turma(), false, null);
            window.ShowDialog();
            PreencheListaTurma();
        }
        private void OnEditar()
        {
            List<Disciplina> disciplinas = new List<Disciplina>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaDisciplinasDaTurma(TurmaSelecionada.Codigo));
            DetalheTurma window = new DetalheTurma(TurmaSelecionada.Turma, true, disciplinas);
            window.ShowDialog();
            PreencheListaTurma();
        }

        private bool CanEditar()
        {
            return TurmaSelecionada != null;
        }
        private void OnAtualizar()
        {
            PreencheListaTurma();
        }
        private void OnExcluir()
        {
            //Verificar se curso está vinculado a alguma prova
            //se sim, avisar que curso ta vinculado a alguma prova e nao pode excluir
            //se nao
            //perguntar se deseja mesmo excluir o curso X
            if (CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaSeTurmaVinculadoAAlgumaProva(TurmaSelecionada.Codigo))
            {
                MessageBox.Show("O turma selecionada não pode ser excluída pois está vinculada a uma ou mais provas.");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Tem certeza que deseja excluir essa turma?", "Excluir Turma", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        CalendarioDeProvasOficiais.Cliente.Classes.BD.DeletarTurma(TurmaSelecionada.Turma);
                        MessageBox.Show("Turma deletada com sucesso!");
                        OnAtualizar();
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao deletar turma. Tente novamente mais tarde.");
                    }

                }
            }


        }
        private bool CanExcluir()
        {
            return TurmaSelecionada != null;
        }

        #endregion
    }
}
