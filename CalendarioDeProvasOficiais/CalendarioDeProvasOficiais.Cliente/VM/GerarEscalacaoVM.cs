using CalendarioDeProvasOficiais.Comum.Objetos;
using CalendarioDeProvasOficiais.Cliente.Classes;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;
using System.Threading;
using System.ComponentModel;
using System.Windows;
using CalendarioDeProvasOficiais.Cliente.View;

namespace CalendarioDeProvasOficiais.Cliente.VM
{
    public class GerarEscalacaoVM : BaseVM
    {
        #region Fields
        
        
        private ObservableCollection<Calendario> _listCalendarioProva;
        //private ObservableCollection<Calendario> _listCalendario;
        private ObservableCollection<ResultEscalacao> _provasResult;
        private ResultEscalacao _provaSelecionada;
        private Calendario _calendarioProvaSelecionado;
        private DelegateCommand _gerarEscalacao;
        private DelegateCommand _cmdAlterarEscalacao;
        private DelegateCommand _zerarEscalacao;
        private bool _indicadorEscalacaoOcupado;
        private bool _indicadorProvasOcupado;
        private bool _indicadorZerarOcupado;
        
        //private BusyIndicator _ocupado;

        #endregion

        #region Properties
        public bool IndicadorEscalacaoOcupado
        {
            get { return _indicadorEscalacaoOcupado; }
            set { SetProperty(ref _indicadorEscalacaoOcupado, value); }
        }
        public bool IndicadorProvasOcupado
        {
            get { return _indicadorProvasOcupado; }
            set { SetProperty(ref _indicadorProvasOcupado, value); }
        }
        public bool IndicadorZerarOcupado
        {
            get { return _indicadorZerarOcupado; }
            set { SetProperty(ref _indicadorZerarOcupado, value); }
        }

        //public BusyIndicator Ocupado 
        //{
        //    get { return _ocupado; }
        //    set { SetProperty(ref _ocupado, value); }
        //}

        public ObservableCollection<Calendario> ListCalendarioProva 
        {
            get { return _listCalendarioProva; }
            set { SetProperty(ref _listCalendarioProva, value); }
        }

        public ObservableCollection<ResultEscalacao> ListProvas
        {
            get { return _provasResult; }
            set { SetProperty(ref _provasResult, value); }
        }

        //public ObservableCollection<Calendario> ListCalendario
        //{
        //    get { return _listCalendario; }
        //    set { SetProperty(ref _listCalendario, value); }
        //}
        public ResultEscalacao ProvaSelecionada 
        {
            get { return _provaSelecionada; }
            set { SetProperty(ref _provaSelecionada, value); _cmdAlterarEscalacao.RaiseCanExecuteChanged(); }
        }

        public Calendario CalendarioProvaSelecionado 
        {
            get 
            { 
                return _calendarioProvaSelecionado;
            }
            set 
            { 
                SetProperty(ref _calendarioProvaSelecionado, value);
                _gerarEscalacao.RaiseCanExecuteChanged();
                _zerarEscalacao.RaiseCanExecuteChanged();
                _cmdAlterarEscalacao.RaiseCanExecuteChanged();
                PreencheLists();
                
            }
        }
        public ICommand GerarEscalacaoCmd 
        {
            get { return _gerarEscalacao; }
            set { _gerarEscalacao = (DelegateCommand)value;  }
        }
        public ICommand CmdAlterarEscalacao 
        {
            get { return _cmdAlterarEscalacao; }
            set { _cmdAlterarEscalacao = (DelegateCommand)value; }
        }
        public ICommand ZerarEscalacao
        {
            get { return _zerarEscalacao; }
            set { _zerarEscalacao = (DelegateCommand)value; }
        }
        #endregion

        #region Metodos
        public GerarEscalacaoVM()
        {
            IndicadorEscalacaoOcupado = false;
            IndicadorProvasOcupado = false;
            IndicadorZerarOcupado = false;
            _gerarEscalacao = new DelegateCommand(OnGerar, CanGerar);
            _cmdAlterarEscalacao = new DelegateCommand(OnAlterar, CanAlterar);
            _zerarEscalacao = new DelegateCommand(OnZerar, CanZerar);
            PreencheCalendarioProva();
            
        }

        private bool CanGerar()
        {
            return CalendarioProvaSelecionado != null;
        }

        private void OnGerar()
        {
            bool sucesso = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                try
                {
                    GeradorEscalacao g = new GeradorEscalacao();
                    g.GerarEscalacao(CalendarioProvaSelecionado);
                    PreencheCalendarioProva();
                    PreencheListaResult();
                }
                catch
                {
                    System.Windows.MessageBox.Show("Erro ao gerar escalação! Selecione o botão \"Gerar\" para começar de onde parou.", "Erro!");
                    sucesso = false;
                }

            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                IndicadorEscalacaoOcupado = false;
                if(sucesso)
                    System.Windows.MessageBox.Show("Escalação gerada com sucesso!", "Sucesso!");
            };
            IndicadorEscalacaoOcupado = true;
            worker.RunWorkerAsync();
        }

        private void PreencheListaResult()
        {
            try
            {
                if (CalendarioProvaSelecionado == null)
                    CalendarioProvaSelecionado = ListCalendarioProva.FirstOrDefault();

                int cod = CalendarioProvaSelecionado.Codigo;
                List<ResultEscalacao> list = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProvasPorCalendario(cod);

                //foreach (ResultEscalacao r in list)
                //{
                //    r.Reservas = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaNomesProfsReservas(r.CodProva, cod);
                //}


                ListProvas = new ObservableCollection<ResultEscalacao>(list);
            }
            catch
            {
                
            }
            
        }        

        private void PreencheLists()
        {
            IndicadorProvasOcupado = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                PreencheListaResult();
            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                IndicadorProvasOcupado = false;
            };

            worker.RunWorkerAsync();
        }        

        public void PreencheCalendarioProva()
        {

            ListCalendarioProva = new ObservableCollection<Calendario>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosCalendarios());            
            CalendarioProvaSelecionado = ListCalendarioProva.FirstOrDefault();
        }

        private void OnAlterar()
        {
            if (CalendarioProvaSelecionado == null)
                CalendarioProvaSelecionado = ListCalendarioProva.FirstOrDefault();

            int codCalendario = CalendarioProvaSelecionado.Codigo;
            AlterarEscalacao window = new AlterarEscalacao(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProva(ProvaSelecionada.CodProva), CalendarioProvaSelecionado.Ano);
            window.ShowDialog();

            if (!window.Cancelou())
            {
                System.Windows.MessageBox.Show("Escalação alterada com sucesso!");
                CalendarioProvaSelecionado = ListCalendarioProva.Where(p => p.Codigo == codCalendario).FirstOrDefault();
            }
            
        }

        private bool CanAlterar()
        {
            return ProvaSelecionada != null && CalendarioProvaSelecionado != null;
        }

        private void OnZerar()
        {
            MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Tem certeza que deseja ZERAR a escalação do calendário selecionado? Essa ação não poderá ser desfeita.", "Zerar Escalação", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (dialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += (o, ea) =>
                    {
                        if (CalendarioProvaSelecionado == null)
                            CalendarioProvaSelecionado = ListCalendarioProva.FirstOrDefault();

                        int codCalendario = CalendarioProvaSelecionado.Codigo;
                        CalendarioDeProvasOficiais.Cliente.Classes.BD.ZerarEscalacao(codCalendario);
                        CalendarioProvaSelecionado = ListCalendarioProva.Where(p => p.Codigo == codCalendario).FirstOrDefault();

                    };
                    worker.RunWorkerCompleted += (o, ea) =>
                    {
                        IndicadorZerarOcupado = false;
                        System.Windows.MessageBox.Show("Escalação zerada com sucesso!", "Sucesso!");
                    };
                    IndicadorZerarOcupado = true;
                    worker.RunWorkerAsync();                   
                }
                catch
                {
                    System.Windows.MessageBox.Show("Erro ao zerar escalação. Tente novamente mais tarde.");
                }

            }

            
        }
        private bool CanZerar()
        {
            return CalendarioProvaSelecionado != null;
        }

        #endregion


    }
}
