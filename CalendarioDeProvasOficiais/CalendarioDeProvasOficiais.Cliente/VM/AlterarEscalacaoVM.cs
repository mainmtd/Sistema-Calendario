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

namespace CalendarioDeProvasOficiais.Cliente.VM
{
    public class AlterarEscalacaoVM : BindableBase
    {
        #region Variaveis
        private ObservableCollection<Professor> _listProfessoresAplicador;
        private Professor _professorAplicadorSelecionado;
        private ObservableCollection<Professor> _listProfessoresSup;
        private Professor _professorSuplenteSelecionado;
        private DelegateCommand _cmdConfirmar;
        private DelegateCommand _cmdCancelar;
        private Window _window;
        private Prova _prova;
        private bool _fechaJanela;
        private int _codProfSup;
        private int _profAplicador;
        #endregion

        #region Propriedades
        public ObservableCollection<Professor> ListProfessoresAplicador 
        {
            get { return _listProfessoresAplicador; }
            set { SetProperty(ref _listProfessoresAplicador, value); }
        }
        public Professor ProfessorAplicadorSelecionado
        {
            get { return _professorAplicadorSelecionado; }
            set { SetProperty(ref _professorAplicadorSelecionado, value); _cmdConfirmar.RaiseCanExecuteChanged(); }
        }
        public ObservableCollection<Professor> ListProfessoresSup
        {
            get { return _listProfessoresSup; }
            set { SetProperty(ref _listProfessoresSup, value); }
        }
        public Professor ProfessorSuplenteSelecionado
        {
            get { return _professorSuplenteSelecionado; }
            set { SetProperty(ref _professorSuplenteSelecionado, value); }
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
        public bool Cancelou { get; set; }

        #endregion

        #region Metodos
        public AlterarEscalacaoVM(Window w, Prova prova, int ano)
        {
            _cmdConfirmar = new DelegateCommand(OnConfirmar, CanConfirmar);
            _cmdCancelar = new DelegateCommand(OnCancelar);
            _window = w;
            _prova = prova;

            PreencheLists(prova, ano);
            Cancelou = true;
            _codProfSup = ProfessorSuplenteSelecionado.Codigo;
           
        }

        private void PreencheLists(Prova prova, int ano)
        {
            ListProfessoresAplicador = new ObservableCollection<Professor>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProfessoresDisponiveisDiaHorario(prova.Data, prova.Hora.Hours, prova.Unidade, prova.Regime, ano));
            ProfessorAplicadorSelecionado = ListProfessoresAplicador.Where(p => p.Codigo == prova.Professor_Codigo).FirstOrDefault();

            if(ProfessorAplicadorSelecionado != null)
                _profAplicador = ProfessorAplicadorSelecionado.Codigo;



            ListProfessoresSup = new ObservableCollection<Professor>();
            ListProfessoresSup.Add(new Professor(){Nome = "-- NENHUM --"});
            ListProfessoresSup.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProfessoresDisponiveisDiaHorario(prova.Data, prova.Hora.Hours, prova.Unidade, prova.Regime, ano));

            ProfessorSuplenteSelecionado = ListProfessoresSup.Where(p => p.Codigo == prova.Professor_Suplente_Codigo).FirstOrDefault();
            if (ProfessorSuplenteSelecionado == null)
                ProfessorSuplenteSelecionado = ListProfessoresSup.FirstOrDefault();
        }

        private void OnConfirmar()
        {

            if (ProfessorAplicadorSelecionado.Codigo == ProfessorSuplenteSelecionado.Codigo)
            {
                MessageBox.Show("Professor aplicador e suplente não podem ser os mesmos");
            }
            else
            {
                ProfessorAplicador();

                if(_fechaJanela)
                    ProfessorSuplente();

                Cancelou = false;
                if(_fechaJanela)
                    _window.Close();
            }

           
           
        }

        private void ProfessorAplicador()
        {
            List<Prova> provasApl = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProvaSeProfAplicaDiaHorario(_prova.Data, _prova.Hora.Hours, ProfessorAplicadorSelecionado.Codigo);

            if (provasApl.Count > 0 && _profAplicador != ProfessorAplicadorSelecionado.Codigo)
            {
                string mensagem = "O professor aplicador selecionado já está escalado para aplicar outra prova nesse mesmo horário. " +
                    "Tem certeza que deseja continuar? Essa ação irá remover a escalação da outra prova.";

                MessageBoxResult dialogResult = MessageBox.Show(mensagem, "CUIDADO!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        SetaAplicadorNaProva(ProfessorAplicadorSelecionado, _prova, provasApl);
                        _fechaJanela = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao trocar aplicador. Tente novamente mais tarde.");
                        _fechaJanela = false;
                    }

                }
                else
                {
                    _fechaJanela = false;
                }
            }
            else
            {
                SetaAplicadorNaProva(ProfessorAplicadorSelecionado, _prova, provasApl);
                _fechaJanela = true;
            }

        }
        private void ProfessorSuplente()
        {
            if (ProfessorSuplenteSelecionado.Codigo == 0 && _codProfSup != ProfessorSuplenteSelecionado.Codigo)
            {
                CalendarioDeProvasOficiais.Cliente.Classes.BD.TiraProfSuplente(_prova.Codigo);
                _fechaJanela = true;
            }
            else if (ProfessorSuplenteSelecionado.Codigo > 0 && _codProfSup != ProfessorSuplenteSelecionado.Codigo)
            {
                List<Prova> provasSup = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProvaSeProfAplicaDiaHorario(_prova.Data, _prova.Hora.Hours, ProfessorSuplenteSelecionado.Codigo);

                if (provasSup.Count > 0)
                {
                    string mensagem = "O professor suplente selecionado já está escalado para aplicar outra prova nesse mesmo horário. " +
                        "Tem certeza que deseja continuar? Essa ação irá remover a escalação da outra prova.";

                    MessageBoxResult dialogResult = MessageBox.Show(mensagem, "CUIDADO!", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        try
                        {
                            SetaSuplenteNaProva(ProfessorSuplenteSelecionado, _prova, provasSup);
                            _fechaJanela = true;
                        }
                        catch
                        {
                            MessageBox.Show("Erro ao trocar suplente. Tente novamente mais tarde.");
                            _fechaJanela = false;
                        }

                    }
                    else
                    {
                        _fechaJanela = false;
                    }
                }
                else
                {
                    SetaSuplenteNaProva(ProfessorSuplenteSelecionado, _prova, provasSup);
                    _fechaJanela = true;
                }
            }
        }


        private void SetaAplicadorNaProva(Professor professorAplicadorSelecionado, Prova provaTrocar, List<Prova> provasParaDesescalar)
        {
            foreach(Prova prova in provasParaDesescalar)
            {
                CalendarioDeProvasOficiais.Cliente.Classes.BD.TiraAplicador(prova);
            }

            //seta aplicador Novo na prova
            CalendarioDeProvasOficiais.Cliente.Classes.BD.AlteraAplicador(_prova.Codigo, professorAplicadorSelecionado.Codigo);

            //Verifica se tem mais alguma prova na mesma sala da prova
            List<Prova> provaMesmaSala = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProvasMesmaSala(_prova.Data, _prova.Hora.Hours, _prova.Codigo, _prova.Sala_Codigo);
            foreach (Prova prova in provaMesmaSala)
            {
                CalendarioDeProvasOficiais.Cliente.Classes.BD.AlteraAplicador(prova.Codigo, professorAplicadorSelecionado.Codigo);
            }
           
        }

        private void SetaSuplenteNaProva(Professor professorSuplenteSelecionado, Prova provaTrocar, List<Prova> provasParaDesescalar)
        {
            foreach (Prova prova in provasParaDesescalar)
            {
                CalendarioDeProvasOficiais.Cliente.Classes.BD.TiraAplicador(prova);
                CalendarioDeProvasOficiais.Cliente.Classes.BD.TiraSuplente(prova);
            }

            //seta aplicador Novo na prova
            CalendarioDeProvasOficiais.Cliente.Classes.BD.AlteraSuplente(_prova.Codigo, professorSuplenteSelecionado.Codigo);

            //Verifica se tem mais alguma prova na mesma sala da prova
            List<Prova> provaMesmaSala = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProvasMesmaSala(_prova.Data, _prova.Hora.Hours, _prova.Codigo, _prova.Sala_Codigo);
            foreach (Prova prova in provaMesmaSala)
            {
                CalendarioDeProvasOficiais.Cliente.Classes.BD.AlteraSuplente(prova.Codigo, professorSuplenteSelecionado.Codigo);
            }

        }
        private void OnCancelar()
        {
            Cancelou = true;
            _window.Close();
        }
        private bool CanConfirmar()
        {
            return ProfessorAplicadorSelecionado != null;
        }
        #endregion
    }
}
