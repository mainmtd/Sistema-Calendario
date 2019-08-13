using CalendarioDeProvasOficiais.Cliente.View.GerenciarCurso;
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

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarCurso
{
    public class CadastroCursoGridVM : BindableBase
    {
        #region Variaveis
        private ObservableCollection<CursoView> _listCurso;
        private CursoView _cursoSelecionado;
        private DelegateCommand _cmdNovo;
        private DelegateCommand _cmdEditar;
        private DelegateCommand _cmdAtualizar;
        private DelegateCommand _cmdExcluir;
        #endregion

        #region Propriedades
        public CursoView CursoSelecionado
        {
            get { return _cursoSelecionado; }
            set { SetProperty(ref _cursoSelecionado, value); _cmdEditar.RaiseCanExecuteChanged(); _cmdExcluir.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<CursoView> ListCurso
        {
            get { return _listCurso; }
            set { SetProperty(ref _listCurso, value); }
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
        public CadastroCursoGridVM()
        {
            _cmdNovo = new DelegateCommand(OnNovo);
            _cmdEditar = new DelegateCommand(OnEditar, CanEditar);
            _cmdAtualizar = new DelegateCommand(OnAtualizar);
            _cmdExcluir = new DelegateCommand(OnExcluir, CanExcluir);

            PreencheListaCurso();
        }

        private void PreencheListaCurso()
        {
            ListCurso = new ObservableCollection<CursoView>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaCursoView());
            CursoSelecionado = ListCurso.FirstOrDefault();
        }
        private void OnNovo()
        {
            DetalheCurso window = new DetalheCurso(new Curso(), false);
            window.ShowDialog();
            PreencheListaCurso();
        }
        private void OnEditar()
        {
            DetalheCurso window = new DetalheCurso(CursoSelecionado.Curso, true);
            window.ShowDialog();
            PreencheListaCurso();
        }

        private bool CanEditar()
        {
            return CursoSelecionado != null;
        }
        private void OnAtualizar()
        {
            PreencheListaCurso();
        }
        private void OnExcluir()
        {
            //Verificar se curso está vinculado a alguma prova
            //se sim, avisar que curso ta vinculado a alguma prova e nao pode excluir
            //se nao
            //perguntar se deseja mesmo excluir o curso X
            if (CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaSeCursoVinculadoAAlgumaProva(CursoSelecionado.Codigo))
            {
                MessageBox.Show("O curso selecionado não pode ser excluído pois está vinculado a uma ou mais provas.");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Tem certeza que deseja excluir esse curso?", "Excluir Curso", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        CalendarioDeProvasOficiais.Cliente.Classes.BD.DeletarCurso(CursoSelecionado.Curso);
                        MessageBox.Show("Curso deletado com sucesso!");
                        OnAtualizar();
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao deletar curso. Tente novamente mais tarde.");
                    }
                         
                }
            }
           

        }
        private bool CanExcluir()
        {
            return CursoSelecionado != null;
        }
        #endregion
    }
}
