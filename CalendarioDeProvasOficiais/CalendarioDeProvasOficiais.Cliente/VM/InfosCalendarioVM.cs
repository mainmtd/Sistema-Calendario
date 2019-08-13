using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Objetos;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CalendarioDeProvasOficiais.Cliente.VM
{
    class InfosCalendarioVM : BindableBase
    {
        #region valores
        private Dictionary<Semestre, string> _listSemestre;
        private KeyValuePair<Semestre, string> _semestreSelecionado;
        private ObservableCollection<Faculdade> _listFaculdade;
        private Faculdade _faculdadeSelecionada;
        private string _nome, _ano;
        private Window _window;
        private DelegateCommand _cmdConfirmar, _cmdCancelar;
        private bool _indicadorInsertOcupado;
        ObservableCollection<Prova> _provasCalendario;
        #endregion

        #region propiedades

        public bool IndicadorInsertOcupado
        {
            get { return _indicadorInsertOcupado; }
            set { SetProperty(ref _indicadorInsertOcupado, value); }
        }

        public Dictionary<Semestre, string> ListSemestre
        {
            get { return _listSemestre; }
            set { SetProperty(ref _listSemestre, value); }
        }
        public KeyValuePair<Semestre, string> SemestreSelecionado
        {
            get { return _semestreSelecionado; }
            set { SetProperty(ref _semestreSelecionado, value); }
        }
        public ObservableCollection<Faculdade> ListFaculdade
        {
            get { return _listFaculdade; }
            set { SetProperty(ref _listFaculdade, value); }
        }
        public ObservableCollection<Prova> ProvasCalendario
        {
            get { return _provasCalendario; }
            set { SetProperty(ref _provasCalendario, value); }
        }

        public Faculdade FaculdadeSelecionada
        {
            get { return _faculdadeSelecionada; }
            set { SetProperty(ref _faculdadeSelecionada, value); }
        }
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public string Ano
        {
            get { return _ano; }
            set { SetProperty(ref _ano, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
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
        #endregion

        public InfosCalendarioVM(Window w,ObservableCollection<Prova> prov)
        {
            IndicadorInsertOcupado = false;
            //Cria Commands
            _cmdConfirmar = new DelegateCommand(OnConfirmar, CanConfirmar);
            _cmdCancelar = new DelegateCommand(OnCancelar);

            ProvasCalendario = prov;
            PreencheLists();
            _window = w;
        }

        public void PreencheLists()
        {
            PreencheFaculdade();
            PreencheSemestre();
        }

        private void PreencheFaculdade()
        {
            ListFaculdade = new ObservableCollection<Faculdade>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasFaculdades());
            FaculdadeSelecionada = ListFaculdade.FirstOrDefault();
        }

        private void PreencheSemestre()
        {
            ListSemestre = new Dictionary<Semestre, string>();
            ListSemestre.Add(Semestre.Primeiro, "1º Semestre");
            ListSemestre.Add(Semestre.Segundo, "2º Semestre");
            SemestreSelecionado = ListSemestre.FirstOrDefault();
        }

        private void OnConfirmar()
        {
            BackgroundWorker worker = new BackgroundWorker();
            bool erro = false;

            worker.DoWork += (o, ea) =>
            {
                int semestre = SemestreConversor.SemestreParaNumero(SemestreSelecionado.Key);
                int ano = int.Parse(Ano);
                try
                {
                    CriaCalendarioDeProvas(semestre, Nome, ano, ProvasCalendario);
                }
                catch
                {
                    MessageBox.Show("Erro na importação, por favor, tente novamente", "Erro");
                    IndicadorInsertOcupado = false;
                    erro = true;
                }
            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                if (!erro)
                {
                    MessageBox.Show("Importação concluída com sucesso!", "Concluído");
                    IndicadorInsertOcupado = false;
                }
                else
                {
                    IndicadorInsertOcupado = false;
                }

                _window.Close();               

            };
            IndicadorInsertOcupado = true;
            worker.RunWorkerAsync();

            
        }
        private bool CanConfirmar()
        {
            return !string.IsNullOrEmpty(Nome) && !string.IsNullOrEmpty(Ano) && (int.Parse(Ano) > 0);
        }
        private void OnCancelar()
        {
            _window.Close();
        }


        // Cria o calendário de provas e insere as provas nele
        // Executado ao clicar no botão de confirmar a importação
        public void CriaCalendarioDeProvas(int semestre, string nome, int ano, ObservableCollection<Prova> provas)
        {
            InsereCalendario(semestre, nome, ano);
            int codCalendario = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaUltimoCalendario();
            InsereProvas(codCalendario, provas);
            InsereCalendarioHasProf(codCalendario);
        }


        // Insere as provas e une com o calendário
        public void InsereProvas(int codCalendario, ObservableCollection<Prova> provas)
        {
            foreach (Prova p in provas)
            {
                CalendarioDeProvasOficiais.Cliente.Classes.BD.InsereProva(p);
                int codProva = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaUltimaProva();
                CalendarioDeProvasOficiais.Cliente.Classes.BD.InsereCalendarioProvaIndividual(codCalendario, codProva);
            }
        }

        // Insere um novo calendário de provas com as informações digitadas pelo usuário
        // Alteração necessária caso precisarmos incluir outras Faculdades
        public void InsereCalendario(int semestre, string nome, int ano)
        {
            int codFacul = 0;
            codFacul = FaculdadeSelecionada.Codigo;
            Calendario cal = new Calendario(semestre, codFacul, nome, ano);
            CalendarioDeProvasOficiais.Cliente.Classes.BD.InsereCalendario(cal);
        }

        public void InsereCalendarioHasProf(int codCalendario)
        {
             CalendarioDeProvasOficiais.Cliente.Classes.BD.InsereCalHProf(codCalendario);
        }
    }
}
