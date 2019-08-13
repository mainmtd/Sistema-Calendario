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

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarCurso
{
    public class DetalheCursoVM : BindableBase
    {
        #region Campos
        private Dictionary<TipoCurso, string> _listTipoCurso;
        private KeyValuePair<TipoCurso, string>  _tipoCursoSelecionado;        
        private ObservableCollection<Faculdade> _listFaculdade;
        private Faculdade _faculdadeSelecionada;
        private ObservableCollection<Professor> _listCoordenador;
        private Professor _coordenadorSelecionado;
        private string _nome;
        private DelegateCommand _cmdConfirmar, _cmdCancelar;
        private Window _window;
        private bool _ehEdicao;
        private Curso _curso;
        #endregion

        #region Propriedades

        public Dictionary<TipoCurso, string> ListTipoCurso 
        {
            get { return _listTipoCurso; }
            set { SetProperty(ref _listTipoCurso, value); }
        }
        public KeyValuePair <TipoCurso, string> TipoCursoSelecionado
        {
            get { return _tipoCursoSelecionado; }
            set { SetProperty(ref _tipoCursoSelecionado, value); }
        }
        public ObservableCollection<Faculdade> ListFaculdade
        {
            get { return _listFaculdade; }
            set { SetProperty(ref _listFaculdade, value); }
        }
        public Faculdade FaculdadeSelecionada
        {
            get { return _faculdadeSelecionada; }
            set { SetProperty(ref _faculdadeSelecionada, value); }
        }
        public ObservableCollection<Professor> ListCoordenador
        {
            get { return _listCoordenador; }
            set { SetProperty(ref _listCoordenador, value); }
        }
        public Professor CoordenadorSelecionado
        {
            get { return _coordenadorSelecionado; }
            set { SetProperty(ref _coordenadorSelecionado, value); }
        }
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
        public bool EhEdicao
        {
            get { return _ehEdicao; }
            set { SetProperty(ref _ehEdicao, value); }
        }

        #endregion

        #region Metodos

        public DetalheCursoVM(Curso curso, Window w, bool ehEdicao)
        { 
            //Cria Commands
            _cmdConfirmar = new DelegateCommand(OnConfirmar, CanConfirmar);
            _cmdCancelar = new DelegateCommand(OnCancelar);

            EhEdicao = ehEdicao;
            _window = w;
            _curso = curso;
            Nome = curso.Nome;

            //Preenche Lists
            PreencheLists();           

        }

        private void PreencheLists()
        {
            //TipoCurso
            PreencheTipoCurso();

            //Faculdade
            PreencheFaculdade();

            //Coordenador-Professor
            PreencheProfessor();

        }

        private void PreencheTipoCurso()
        {
            ListTipoCurso = new Dictionary<TipoCurso, string>();
            ListTipoCurso.Add(TipoCurso.Anual, "Anual");
            ListTipoCurso.Add(TipoCurso.Semestral, "Semestral");

            if (EhEdicao)
            {
                TipoCursoSelecionado = ListTipoCurso.Where(p => p.Key == _curso.TipoCurso).FirstOrDefault();
            }
            else
            {
                TipoCursoSelecionado = ListTipoCurso.FirstOrDefault();
            }
            
        }
        private void PreencheFaculdade()
        {
            ListFaculdade = new ObservableCollection<Faculdade>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasFaculdades());
            FaculdadeSelecionada = ListFaculdade.FirstOrDefault();
        }
        private void PreencheProfessor()
        {
            ListCoordenador = new ObservableCollection<Professor>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosProfessores());
            CoordenadorSelecionado = ListCoordenador.FirstOrDefault();
        }

        private void OnConfirmar()
        {
            _curso.Nome = Nome;
            _curso.CodigoFaculdade = FaculdadeSelecionada.Codigo;
            _curso.CodigoProfessor = CoordenadorSelecionado.Codigo;
            _curso.TipoCurso = TipoCursoSelecionado.Key;

            //chama metodo de salvar ou editar
            if (_ehEdicao)
                CalendarioDeProvasOficiais.Cliente.Classes.BD.AlterarCurso(_curso);
            else
                CalendarioDeProvasOficiais.Cliente.Classes.BD.IncluirCurso(_curso);


            _window.Close();
        }
        private bool CanConfirmar()
        {
            return (!string.IsNullOrEmpty(Nome) && FaculdadeSelecionada != null && CoordenadorSelecionado != null);
        }
        private void OnCancelar()
        {
            _window.Close();
        }

        #endregion
    }
}
