using CalendarioDeProvasOficiais.Cliente.View.GerenciarSala;
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

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarSala
{
    public class CadastroSalaGridVM : BindableBase
    {
        #region Variaveis
        private ObservableCollection<Sala> _listSala;
        private Sala _salaSelecionada;
        private DelegateCommand _cmdNovo;
        private DelegateCommand _cmdEditar;
        private DelegateCommand _cmdAtualizar;
        private DelegateCommand _cmdExcluir;
        #endregion

        #region Propriedades
        public Sala SalaSelecionada
        {
            get { return _salaSelecionada; }
            set { SetProperty(ref _salaSelecionada, value); _cmdEditar.RaiseCanExecuteChanged(); _cmdExcluir.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Sala> ListSala
        {
            get { return _listSala; }
            set { SetProperty(ref _listSala, value); }
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
        public CadastroSalaGridVM()
        {
            _cmdNovo = new DelegateCommand(OnNovo);
            _cmdEditar = new DelegateCommand(OnEditar, CanEditar);
            _cmdAtualizar = new DelegateCommand(OnAtualizar);
            _cmdExcluir = new DelegateCommand(OnExcluir, CanExcluir);

            PreencheListaSala();
        }

        private void PreencheListaSala()
        {
            ListSala = new ObservableCollection<Sala>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasSalas());
            SalaSelecionada = ListSala.FirstOrDefault();
        }
        private void OnNovo()
        {
            DetalheSala window = new DetalheSala(new Sala(), false);
            window.ShowDialog();
            PreencheListaSala();
        }
        private void OnEditar()
        {
            DetalheSala window = new DetalheSala(SalaSelecionada, true);
            window.ShowDialog();
            PreencheListaSala();
        }

        private bool CanEditar()
        {
            return SalaSelecionada != null;
        }
        private void OnAtualizar()
        {
            PreencheListaSala();
        }
        private void OnExcluir()
        {
            //Verificar se curso está vinculado a alguma prova
            //se sim, avisar que curso ta vinculado a alguma prova e nao pode excluir
            //se nao
            //perguntar se deseja mesmo excluir o curso X
            if (CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaSeSalaVinculadoAAlgumaProva(SalaSelecionada.Codigo))
            {
                MessageBox.Show("A sala selecionada não pode ser excluída pois está vinculada a uma ou mais provas.");
            }
            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Tem certeza que deseja excluir essa sala?", "Excluir Sala", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        CalendarioDeProvasOficiais.Cliente.Classes.BD.DeletarSala(SalaSelecionada);
                        MessageBox.Show("Sala deletada com sucesso!");
                        OnAtualizar();
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao deletar sala. Tente novamente mais tarde.");
                    }
                         
                }
            }
           

        }
        private bool CanExcluir()
        {
            return SalaSelecionada != null;
        }
        #endregion
    }
}
