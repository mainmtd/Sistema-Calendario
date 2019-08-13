using CalendarioDeProvasOficiais.Cliente.View;
using CalendarioDeProvasOficiais.Cliente.View.GerenciarCurso;
using CalendarioDeProvasOficiais.Cliente.View.GerenciarDisciplina;
using CalendarioDeProvasOficiais.Cliente.View.GerenciarProfessor;
using CalendarioDeProvasOficiais.Cliente.View.GerenciarSala;
using CalendarioDeProvasOficiais.Cliente.View.GerenciarTurma;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalendarioDeProvasOficiais.Cliente.VM
{
    public class BaseVM : BindableBase
    {
        #region Variaveis
        UserControl _content;
        List<UserControl> _ucs;
        DelegateCommand _fechar;
        DelegateCommand _relatoriosCurso;
        DelegateCommand _cadDisciplinas;
        DelegateCommand _cadSalas;
        DelegateCommand _cadProfessores;
        DelegateCommand _cadTurmas;
        DelegateCommand _cadCursos;
        DelegateCommand _gerarEscalacao;
        DelegateCommand _gerencCalendario;
        DelegateCommand _CapaProvaPDF;
        DelegateCommand _gerarRelatorio;
        

        
        #endregion

        #region Properiedades

        public UserControl Content 
        {
            get { return _content; }
            set 
            {
                SetProperty(ref _content, value);
            }
        }

        public ICommand RelatoriosCurso 
        {
            get { return _relatoriosCurso; }
            set { _relatoriosCurso = (DelegateCommand)value; }
        }

        public ICommand CadDisciplinas
        {
            get { return _cadDisciplinas; }
            set { _cadDisciplinas = (DelegateCommand)value; }
        }

        public ICommand CadProfessores
        {
            get { return _cadProfessores; }
            set { _cadProfessores = (DelegateCommand)value; }

        }


        public ICommand GerarRelatorio
        {
            get { return _gerarRelatorio; }
            set { _gerarRelatorio = (DelegateCommand)value; }

        }

        public ICommand CadTurmas
        {
            get { return _cadTurmas; }
            set { _cadTurmas = (DelegateCommand)value; }

        }

        public ICommand CadSalas
        {
            get { return _cadSalas; }
            set { _cadSalas = (DelegateCommand)value; }

        }

        public ICommand CadCursos
        {
            get { return _cadCursos; }
            set { _cadCursos = (DelegateCommand)value; }

        }


        public ICommand GerencCalendario
        {
            get { return _gerencCalendario; }
            set { _gerencCalendario = (DelegateCommand)value; }

        }

        public ICommand GerarEscalacao
        {
            get { return _gerarEscalacao; }
            set { _gerarEscalacao = (DelegateCommand)value; }
        }

        public ICommand CapaProvaPDF
        {
            get { return _CapaProvaPDF; }
            set { _CapaProvaPDF = (DelegateCommand)value; }
        }

        public ICommand Fechar
        {
            get { return _fechar; }
            set { _fechar = (DelegateCommand)value; }
        }


        #endregion

        public BaseVM()
        {
            _ucs = new List<UserControl>();
            _ucs.Add(new CadastroCursoGrid() { Name = "CadCursos" });            

            Content = _ucs.FirstOrDefault();

            InicializaCommands();
        }

        private void InicializaCommands()
        {
            _fechar = new DelegateCommand(OnFechar);
            _relatoriosCurso = new DelegateCommand(OnRelatoriosCurso);
            _cadDisciplinas = new DelegateCommand(OnCadDisciplinas);
            _cadProfessores= new DelegateCommand(OnCadProfessores);
            _cadTurmas = new DelegateCommand(OnCadTurmas);
            _cadCursos = new DelegateCommand(OnCadCursos);
            _cadSalas = new DelegateCommand(OnCadSalas);
            _gerarEscalacao = new DelegateCommand(OnGerarEscalacao);
            _gerencCalendario = new DelegateCommand(OnGerencCalendario);
            _CapaProvaPDF = new DelegateCommand(OnCapaProvaPDF);
            _gerarRelatorio = new DelegateCommand(OnGerarRelatorio);

        }

        private void OnFechar()
        {
            //perguntar se quer fechar mesmo

            MessageBoxResult mR = MessageBox.Show("Tem certeza que deseja sair?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mR == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void OnRelatoriosCurso()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "RelatorioCursos").FirstOrDefault();

            if (uc == null)
            {
                //uc = new ConsultaGradeCurso();
                _ucs.Add(uc);
            }
                

            Content = uc;
        }

        private void OnCadDisciplinas()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "CadDisciplinas").FirstOrDefault();

            if (uc == null)
            {
                //uc = new CadastroDisciplinas();
                uc = new CadastroDisciplinaGrid();
                _ucs.Add(uc);
            }


            Content = uc;
        }

        private void OnCadSalas()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "CadSalas").FirstOrDefault();

            if (uc == null)
            {
                uc = new CadastroSalaGrid();
                _ucs.Add(uc);
            }


            Content = uc;
        }

        private void OnGerencCalendario()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "GerencCalendario").FirstOrDefault();

            if (uc == null)
            {
                uc = new GerencCalendario();
                _ucs.Add(uc);
            }


            Content = uc;

        }

        private void OnCadProfessores()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "CadProfessores").FirstOrDefault();

            if (uc == null)
            {
                //uc = new CadastroProfessores();
                uc = new CadastroProfessorGrid();
                _ucs.Add(uc);
            }


            Content = uc;
        }

        private void OnGerarRelatorio()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "GerarRelatorio").FirstOrDefault();

            if (uc == null)
            {
                //uc = new CadastroProfessores();
                uc = new GerarRelatorio();
                _ucs.Add(uc);
            }


            Content = uc;
        }



        private void OnCadTurmas()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "CadTurmas").FirstOrDefault();

            if (uc == null)
            {
                uc = new CadastroTurmaGrid();
                _ucs.Add(uc);
            }


            Content = uc;
        }

        private void OnCadCursos()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "CadCursos").FirstOrDefault();

            if (uc == null)
            {
                uc = new CadastroCursoGrid();
                _ucs.Add(uc);
            }


            Content = uc;
        }



        private void OnGerarEscalacao()
        {

            if (!CalendarioDeProvasOficiais.Cliente.Classes.BD.ExisteCalendario())
            {
                MessageBox.Show("Não existe nenhum calendário cadastrado. Por favor cadastre um.");
                return;
            }


            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "GerarEscalacaoTela").FirstOrDefault();

            if (uc == null)
            {
                uc = new GerarEscalacao();
                _ucs.Add(uc);
            }
            GerarEscalacao ge = uc as GerarEscalacao;
            ge.PreencheListas();
            Content = ge;
        }


        private void OnCapaProvaPDF()
        {
            if (_ucs == null)
                _ucs = new List<UserControl>();

            UserControl uc = _ucs.Where(p => p.Name == "CapaProvaPDF").FirstOrDefault();

            if (uc == null)
            {
                uc = new CapaProva();
                _ucs.Add(uc);
            }

            Content = uc;
        }




    }
}
