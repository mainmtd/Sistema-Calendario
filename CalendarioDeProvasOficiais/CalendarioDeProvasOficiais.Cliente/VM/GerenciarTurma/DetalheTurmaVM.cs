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

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarTurma
{
    public class DetalheTurmaVM : BindableBase
    {
        #region Campos
        private string _nome;
        private Curso _cursoSelecionado;
        private ObservableCollection<Curso> _listCurso;
        private Dictionary<TipoCurso, string> _listTipoCurso;
        private KeyValuePair<TipoCurso, string> _tipoCursoSelecionado;
        private ObservableCollection<Disciplina> _listDisciplinaGeral;
        private Disciplina _disciplinaGeralSelecionada;
        private ObservableCollection<Disciplina> _listDisciplinaSelecionada;
        private Disciplina _disciplinaSelecionada;
        private Turma _turma;
        private bool _ehEdicao;
        private DelegateCommand _cmdConfirmar, _cmdCancelar, _cmdAdd, _cmdDel;
        private Window _window;
        #endregion

        #region Propriedades
        public string Nome 
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public Curso CursoSelecionado 
        {
            get { return _cursoSelecionado; }
            set { SetProperty(ref _cursoSelecionado, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Curso> ListCurso 
        {
            get { return _listCurso; }
            set { SetProperty(ref _listCurso, value); }
        }
        public Dictionary<TipoCurso, string> ListTipoCurso
        {
            get { return _listTipoCurso; }
            set { SetProperty(ref _listTipoCurso, value); }
        }
        public KeyValuePair<TipoCurso, string> TipoCursoSelecionado
        {
            get { return _tipoCursoSelecionado; }
            set { SetProperty(ref _tipoCursoSelecionado, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Disciplina> ListDisciplinaGeral
        {
            get { return _listDisciplinaGeral; }
            set { SetProperty(ref _listDisciplinaGeral, value); }
        }
        public Disciplina DisciplinaGeralSelecionada
        {
            get { return _disciplinaGeralSelecionada; }
            set { SetProperty(ref _disciplinaGeralSelecionada, value); _cmdAdd.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Disciplina> ListDisciplinaSelecionada
        {
            get { return _listDisciplinaSelecionada; }
            set { SetProperty(ref _listDisciplinaSelecionada, value); }
        }
        public Disciplina DisciplinaSelecionada
        {
            get { return _disciplinaSelecionada; }
            set { SetProperty(ref _disciplinaSelecionada, value); _cmdDel.RaiseCanExecuteChanged(); }
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
        #endregion

        #region Métodos
        public DetalheTurmaVM(Turma t, Window w, bool ehEdicao, List<Disciplina> diciplinas)
        {
            //Cria Commands
            _cmdConfirmar = new DelegateCommand(OnConfirmar, CanConfirmar);
            _cmdCancelar = new DelegateCommand(OnCancelar);
            _cmdAdd = new DelegateCommand(OnAdd, CanAdd);
            _cmdDel = new DelegateCommand(OnDel, CanDel);

            _ehEdicao = ehEdicao;
            _window = w;
            _turma = t;
            Nome = t.Nome;

            //Preenche Lists
            PreencheLists(diciplinas);

           

        }

        private void PreencheLists(List<Disciplina> disciplinas)
        {

            //Disciplina geral
            PreencheDisciplinasGeral();

            //Disciplina selecionados
            PreencheDisciplinasSelecionadas(disciplinas);

            //Preenche curso
            PreencheCurso();

            //PreencheTipoCurso
            PreencheTipoCurso();

        }

        private void PreencheTipoCurso()
        {

            ListTipoCurso = new Dictionary<TipoCurso, string>();
            ListTipoCurso.Add(TipoCurso.Anual, "Anual");
            ListTipoCurso.Add(TipoCurso.Semestral, "Semestral");

            if (_ehEdicao)
            {
                TipoCursoSelecionado = ListTipoCurso.Where(p => p.Key == _turma.TipoCurso).FirstOrDefault();
            }
            else
            {
                TipoCursoSelecionado = ListTipoCurso.FirstOrDefault();
            }
        }

        private void PreencheCurso()
        {
            ListCurso = new ObservableCollection<Curso>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosCursosCrudCurso());
            CursoSelecionado = ListCurso.FirstOrDefault();
        }

        private void PreencheDisciplinasGeral()
        {
            ListDisciplinaGeral = new ObservableCollection<Disciplina>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasDisciplinas());
            DisciplinaGeralSelecionada = ListDisciplinaGeral.FirstOrDefault();
        }

        private void PreencheDisciplinasSelecionadas(List<Disciplina> disciplinas)
        {
            if (disciplinas == null)
                disciplinas = new List<Disciplina>();

            ListDisciplinaSelecionada = new ObservableCollection<Disciplina>(disciplinas);
            DisciplinaSelecionada = ListDisciplinaSelecionada.FirstOrDefault();

            //remove disciplina que estão aqui do geral
            foreach (Disciplina di in ListDisciplinaSelecionada)
            {
                Disciplina disciplina = ListDisciplinaGeral.Where(p => di.Codigo == p.Codigo).FirstOrDefault();
                if (disciplina != null)
                {
                    ListDisciplinaGeral.Remove(disciplina);
                }
            }
        }

        private void OnConfirmar()
        {
            _turma.Nome = Nome;
            _turma.CodigoCurso = CursoSelecionado.Codigo;
            _turma.TipoCurso = TipoCursoSelecionado.Key;

            //chama metodo de salvar ou editar
            if (_ehEdicao)
                CalendarioDeProvasOficiais.Cliente.Classes.BD.AlterarTurma(_turma, ListDisciplinaSelecionada.ToList());
            else
                CalendarioDeProvasOficiais.Cliente.Classes.BD.IncluirTurma(_turma, ListDisciplinaSelecionada.ToList());


            _window.Close();
        }
        private bool CanConfirmar()
        {
            return (!string.IsNullOrEmpty(Nome) && CursoSelecionado != null);
        }
        private void OnCancelar()
        {
            _window.Close();
        }
        private void OnAdd()
        {
            Disciplina disciplina = DisciplinaGeralSelecionada;
            ListDisciplinaGeral.Remove(DisciplinaGeralSelecionada);
            ListDisciplinaSelecionada.Add(disciplina);
            ListDisciplinaSelecionada = new ObservableCollection<Disciplina>(ListDisciplinaSelecionada.OrderBy(p => p.Nome));
            DisciplinaGeralSelecionada = ListDisciplinaGeral.FirstOrDefault();
            DisciplinaSelecionada = ListDisciplinaGeral.FirstOrDefault();
        }
        private bool CanAdd()
        {
            return DisciplinaGeralSelecionada != null;
        }
        private void OnDel()
        {
            Disciplina disciplina = DisciplinaSelecionada;
            ListDisciplinaSelecionada.Remove(DisciplinaSelecionada);
            ListDisciplinaGeral.Add(disciplina);
            ListDisciplinaGeral = new ObservableCollection<Disciplina>(ListDisciplinaGeral.OrderBy(p => p.Nome));
            DisciplinaGeralSelecionada = ListDisciplinaGeral.FirstOrDefault();
            DisciplinaSelecionada = ListDisciplinaGeral.FirstOrDefault();
        }
        private bool CanDel()
        {
            return DisciplinaSelecionada != null;
        }


        #endregion
    }
}
