using CalendarioDeProvasOficiais.Comum.Enum;
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
    public class DetalheDisciplinaVM : BindableBase
    {
        #region Variaveis
        private Dictionary<TipoCurso, string> _listTipoCurso;
        private KeyValuePair<TipoCurso, string> _tipoCursoSelecionado;
        private string _nome;
        private ObservableCollection<Professor> _listProfessoresGeral;
        private Professor _professoresGeralSelecionado;
        private ObservableCollection<Professor> _listProfessoresSelecionados;
        private Professor _professorSelecionado;
        private DelegateCommand _cmdConfirmar, _cmdCancelar, _cmdAdd, _cmdDel;
        private Window _window;
        private bool _ehEdicao;
        private Disciplina _disciplina;
        #endregion

        #region Propriedades

        public Dictionary<TipoCurso, string> ListRegime
        {
            get { return _listTipoCurso; }
            set { SetProperty(ref _listTipoCurso, value); }
        }
        public KeyValuePair<TipoCurso, string> RegimeSelecionado
        {
            get { return _tipoCursoSelecionado; }
            set { SetProperty(ref _tipoCursoSelecionado, value); }
        }
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Professor> ListProfessoresGeral
        {
            get { return _listProfessoresGeral; }
            set { SetProperty(ref _listProfessoresGeral, value); }
        }
        public Professor ProfessoresGeralSelecionado
        {
            get { return _professoresGeralSelecionado; }
            set { SetProperty(ref _professoresGeralSelecionado, value); _cmdAdd.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Professor> ListProfessoresSelecionados
        {
            get { return _listProfessoresSelecionados; }
            set { SetProperty(ref _listProfessoresSelecionados, value); }
        }
        public Professor ProfessorSelecionado
        {
            get { return _professorSelecionado; }
            set { SetProperty(ref _professorSelecionado, value); _cmdDel.RaiseCanExecuteChanged(); }
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
        public ICommand CmdAdd
        {
            get { return _cmdAdd; }
            set { _cmdAdd = (DelegateCommand)value; }
        }
        public ICommand CmdDel
        {
            get { return _cmdDel; }
            set { _cmdDel = (DelegateCommand)value; }
        }

        public bool EhEdicao
        {
            get { return _ehEdicao; }
            set { SetProperty(ref _ehEdicao, value); }
        }
        #endregion

        #region Métodos

        public DetalheDisciplinaVM(Disciplina d, Window w, bool ehEdicao, List<Professor> professores)
        {
            //Cria Commands
            _cmdConfirmar = new DelegateCommand(OnConfirmar, CanConfirmar);
            _cmdCancelar = new DelegateCommand(OnCancelar);
            _cmdAdd = new DelegateCommand(OnAdd, CanAdd);
            _cmdDel = new DelegateCommand(OnDel, CanDel);

            EhEdicao = ehEdicao;
            _window = w;
            _disciplina = d;
            Nome = d.Nome;

            //Preenche Lists
            PreencheLists(professores);

        }

        private void PreencheLists(List<Professor> professores)
        {
            //TipoCurso
            PreencheTipoCurso();

            //Professor geral
            PreencheProfessorGeral();

            //Professores selecionados
            PreencheProfessorSelecionado(professores);

        }

        private void PreencheTipoCurso()
        {
            ListRegime = new Dictionary<TipoCurso, string>();
            ListRegime.Add(TipoCurso.Anual, "Anual");
            ListRegime.Add(TipoCurso.Semestral, "Semestral");

            if (EhEdicao)
            {
                RegimeSelecionado = ListRegime.Where(p => p.Key == _disciplina.TipoCurso).FirstOrDefault();
            }
            else
            {
                RegimeSelecionado = ListRegime.FirstOrDefault();
            }

        }

        private void PreencheProfessorGeral()
        {
            ListProfessoresGeral = new ObservableCollection<Professor>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosProfessores());
            ProfessoresGeralSelecionado = ListProfessoresGeral.FirstOrDefault();
        }

        private void PreencheProfessorSelecionado(List<Professor> professores)
        {
            if (professores == null)
                professores = new List<Professor>();

            ListProfessoresSelecionados = new ObservableCollection<Professor>(professores);
            ProfessorSelecionado = ListProfessoresSelecionados.FirstOrDefault();

            //remove professores que estão aqui do geral
            foreach (Professor pr in ListProfessoresSelecionados)
            {
                Professor prof = ListProfessoresGeral.Where(p => pr.Codigo == p.Codigo).FirstOrDefault();
                if (prof != null)
                {
                    ListProfessoresGeral.Remove(prof);
                }
            }
        }

        private void OnConfirmar()
        {
            _disciplina.Nome = Nome;
            _disciplina.TipoCurso = RegimeSelecionado.Key;

            //chama metodo de salvar ou editar
            if (_ehEdicao)
                CalendarioDeProvasOficiais.Cliente.Classes.BD.AlterarDisciplina(_disciplina, ListProfessoresSelecionados.ToList());
            else
                CalendarioDeProvasOficiais.Cliente.Classes.BD.IncluirDisciplina(_disciplina, ListProfessoresSelecionados.ToList());


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
        private void OnAdd()
        {
            Professor prof = ProfessoresGeralSelecionado;
            ListProfessoresGeral.Remove(ProfessoresGeralSelecionado);
            ListProfessoresSelecionados.Add(prof);
            ListProfessoresSelecionados = new ObservableCollection<Professor>(ListProfessoresSelecionados.OrderBy(p => p.Nome));
            ProfessoresGeralSelecionado = ListProfessoresGeral.FirstOrDefault();
            ProfessorSelecionado = ListProfessoresSelecionados.FirstOrDefault();
        }
        private bool CanAdd()
        {
            return ProfessoresGeralSelecionado != null;
        }
        private void OnDel()
        {
            Professor prof = ProfessorSelecionado;
            ListProfessoresSelecionados.Remove(ProfessorSelecionado);
            ListProfessoresGeral.Add(prof);
            ListProfessoresGeral = new ObservableCollection<Professor>(ListProfessoresGeral.OrderBy(p => p.Nome));
            ProfessoresGeralSelecionado = ListProfessoresGeral.FirstOrDefault();
            ProfessorSelecionado = ListProfessoresSelecionados.FirstOrDefault();
        }
        private bool CanDel()
        {
            return ProfessorSelecionado != null;
        }

        #endregion

    }
}
