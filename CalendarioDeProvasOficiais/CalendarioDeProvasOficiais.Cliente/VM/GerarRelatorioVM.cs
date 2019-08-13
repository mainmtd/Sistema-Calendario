using iTextSharp.text;
using iTextSharp.text.pdf;
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
using System.IO;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;

namespace CalendarioDeProvasOficiais.Cliente.VM
{
    public class GerarRelatorioVM : BindableBase
    {
        #region Fields
        private String Curso_Par, Data_Par, Turno_Par, Hora_Par, Regime_Par, Calendario_Par;

        ObservableCollection<Relatorio> _relatoriosGrid;
        private ObservableCollection<Relatorio> _listCampus;
        private Relatorio _campusSelecionado;

        private ObservableCollection<Relatorio> _listDiaDaSemana;
        private Relatorio _diaDaSemanaSelecionado;

        private ObservableCollection<Relatorio> _listTurma;
        private Relatorio _turmaSelecionado;

        private ObservableCollection<Relatorio> _listProfessorResponsavel;
        private Relatorio _professorResponsavelSelecionado;

        private ObservableCollection<Relatorio> _listProfessorAplicador;
        private Relatorio _professorAplicadorSelecionado;

        private ObservableCollection<Relatorio> _listDivisao;
        private Relatorio _divisaoSelecionado;

        private ObservableCollection<Relatorio> _listSala;
        private Relatorio _salaSelecionado;

        private ObservableCollection<Relatorio> _listCurso;
        private Relatorio _cursoSelecionado;

        private ObservableCollection<Relatorio> _listData;
        private Relatorio _dataSelecionado;

        private ObservableCollection<Relatorio> _listHora;
        private Relatorio _horaSelecionado;

        private ObservableCollection<Relatorio> _listTurno;
        private Relatorio _turnoSelecionado;

        private ObservableCollection<Relatorio> _listDisciplina;
        private Relatorio _disciplinaSelecionado;

        private ObservableCollection<Relatorio> _listRegime;
        private Relatorio _regimeSelecionado;

        private ObservableCollection<Relatorio> _listCalendario;
        private Relatorio _calendarioSelecionado;

        Stream _imgIconOpen;
        private String _caminhoTXT;
        private bool _indicadorOcupadoCapa, _caminhoSalvar, _btnCriarDisable;
        DelegateCommand _btnCriar, _btnEscolheLocal, _btnAbrirLocal, _btnAtualizar;

        String caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private bool _indicadorRelatorioOcupado;

        #endregion

        #region Propriedades


        public ObservableCollection<Relatorio> RelatoriosGrid
        {
            get { return _relatoriosGrid; }
            set { SetProperty(ref _relatoriosGrid, value); }
        }

         public ObservableCollection<Relatorio> ListCampus
        {
            get { return _listCampus; }
            set { SetProperty(ref _listCampus, value); }
        }

         public ObservableCollection<Relatorio> ListDiaDaSemana
        {
            get { return _listDiaDaSemana; }
            set { SetProperty(ref _listDiaDaSemana, value); }
        }

         public ObservableCollection<Relatorio> ListTurma
        {
            get { return _listTurma; }
            set { SetProperty(ref _listTurma, value); }
        }

         public ObservableCollection<Relatorio> ListProfessorResponsavel
        {
            get { return _listProfessorResponsavel; }
            set { SetProperty(ref _listProfessorResponsavel, value); }
        }

         public ObservableCollection<Relatorio> ListProfessorAplicador
        {
            get { return _listProfessorAplicador; }
            set { SetProperty(ref _listProfessorAplicador, value); }
        }

         public ObservableCollection<Relatorio> ListDivisao
        {
            get { return _listDivisao; }
            set { SetProperty(ref _listDivisao, value); }
        }

         public ObservableCollection<Relatorio> ListSala
        {
            get { return _listSala; }
            set { SetProperty(ref _listSala, value); }
        }


        public ObservableCollection<Relatorio> ListCurso
        {
            get { return _listCurso; }
            set { SetProperty(ref _listCurso, value); }
        }

        public ObservableCollection<Relatorio> ListData
        {
            get { return _listData; }
            set { SetProperty(ref _listData, value); }
        }

        public ObservableCollection<Relatorio> ListHora
        {
            get { return _listHora; }
            set { SetProperty(ref _listHora, value); }
        }

        public ObservableCollection<Relatorio> ListTurno
        {
            get { return _listTurno; }
            set { SetProperty(ref _listTurno, value); }
        }

        public ObservableCollection<Relatorio> ListDisciplina
        {
            get { return _listDisciplina; }
            set { SetProperty(ref _listDisciplina, value); }
        }

        public ObservableCollection<Relatorio> ListRegime
        {
            get { return _listRegime; }
            set { SetProperty(ref _listRegime, value); }
        }

        public ObservableCollection<Relatorio> ListCalendario
        {
            get { return _listCalendario; }
            set { SetProperty(ref _listCalendario, value); }
        }

        public bool BtnCriarDisable
        {
            get { return _btnCriarDisable; }
            set { SetProperty(ref _btnCriarDisable, value); }
        }

        public bool IndicadorOcupadoCapa
        {
            get { return _indicadorOcupadoCapa; }
            set { SetProperty(ref _indicadorOcupadoCapa, value); }
        }

        public bool CaminhoSalvar
        {
            get { return _caminhoSalvar; }

            set
            {
                if (_caminhoSalvar == value)
                {
                    return;
                }

                _caminhoSalvar = value;
                RaisePropertyChanged("IsEnabled1");
            }
        }


        public Stream ImgIconOpen
        {
            get { return _imgIconOpen; }
            set { SetProperty(ref _imgIconOpen, value); }
        }

        public Relatorio CursoSelecionado
        {
            get { return _cursoSelecionado; }
            set { SetProperty(ref _cursoSelecionado, value); }
        }

        public Relatorio DataSelecionado
        {
            get { return _dataSelecionado; }
            set { SetProperty(ref _dataSelecionado, value); }
        }

        public Relatorio HoraSelecionado
        {
            get { return _horaSelecionado; }
            set { SetProperty(ref _horaSelecionado, value); }
        }

        public Relatorio TurnoSelecionado
        {
            get { return _turnoSelecionado; }
            set { SetProperty(ref _turnoSelecionado, value); }
        }

        public Relatorio DisciplinaSelecionado
        {
            get { return _disciplinaSelecionado; }
            set { SetProperty(ref _disciplinaSelecionado, value); }
        }

        public Relatorio RegimeSelecionado
        {
            get { return _regimeSelecionado; }
            set { SetProperty(ref _regimeSelecionado, value); }
        }


        public Relatorio CalendarioSelecionado
        {
            get { return _calendarioSelecionado; }
            set { SetProperty(ref _calendarioSelecionado, value); }
        }

        public ICommand BtnCriar
        {
            get { return _btnCriar; }
            set { _btnCriar = (DelegateCommand)value; }
        }

        public ICommand BtnEscolheLocal
        {
            get { return _btnEscolheLocal; }
            set { _btnEscolheLocal = (DelegateCommand)value; }
        }

        public ICommand BtnAbrirLocal
        {
            get { return _btnAbrirLocal; }
            set { _btnAbrirLocal = (DelegateCommand)value; }
        }

        public ICommand BtnAtualizar
        {
            get { return _btnAtualizar; }
            set { _btnAtualizar = (DelegateCommand)value; }
        }

        public String CaminhoTXT
        {
            get { return _caminhoTXT; }
            set { SetProperty(ref _caminhoTXT, value); _btnCriar.RaiseCanExecuteChanged(); }
        }

        public bool IndicadorRelatorioOcupado
        {
            get { return _indicadorRelatorioOcupado; }
            set { SetProperty(ref _indicadorRelatorioOcupado, value); }
        }


        #endregion

        #region Metodos

        public GerarRelatorioVM()
        {   
            //Instância Botões
            BtnCriar = new DelegateCommand(Criar, PodeCriar);
            BtnEscolheLocal = new DelegateCommand(EscolheLocal);
            BtnAbrirLocal = new DelegateCommand(AbrirLocal);
            BtnAtualizar = new DelegateCommand(Atualizar);

            //Invoca o Metodo PreparaTela
            PreparaTela();
        }
         private void PreparaTela()
         {
            IndicadorRelatorioOcupado = false;

             ImgIconOpen = CalendarioDeProvasOficiais.Cliente.Classes.Imagens.RetornaImagemOpen();

             //Traz a lista de Cursos conforme o BD
             ListCurso = new ObservableCollection<Relatorio>();
             ListCurso.Add(new Relatorio() { Curso = "Todos" });
             ListCurso.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosCurso());
             CursoSelecionado = ListCurso.FirstOrDefault();

             ListData= new ObservableCollection<Relatorio>();
             ListData.Add(new Relatorio() { Data = "Todos" });
             ListData.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosData());
             DataSelecionado = ListData.FirstOrDefault();


             ListHora = new ObservableCollection<Relatorio>();
             ListHora.Add(new Relatorio() { Hora = "Todos" });
             ListHora.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosHora());
             HoraSelecionado = ListHora.FirstOrDefault();


             ListTurno = new ObservableCollection<Relatorio>();
             ListTurno.Add(new Relatorio() { Turno = "Todos" });
             ListTurno.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosTurno());
             TurnoSelecionado = ListTurno.FirstOrDefault();


             ListDisciplina = new ObservableCollection<Relatorio>();
             ListDisciplina.Add(new Relatorio() { Disciplina = "Todos" });
             ListDisciplina.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosDisciplina());
             DisciplinaSelecionado = ListDisciplina.FirstOrDefault();


             ListRegime = new ObservableCollection<Relatorio>();
             ListRegime.Add(new Relatorio() { Regime = "Todos" });
             ListRegime.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosRegime());
             RegimeSelecionado = ListRegime.FirstOrDefault();

             ListCalendario = new ObservableCollection<Relatorio>();
             ListCalendario.Add(new Relatorio() { Calendario = "Todos" });
             ListCalendario.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosCalendario());
             CalendarioSelecionado = ListCalendario.FirstOrDefault();

             Curso_Par = CursoSelecionado.Curso.ToString();
             if (Curso_Par.Equals("Todos"))
             {
                 Curso_Par = "%";
             }
             else
             {
                 Curso_Par = CursoSelecionado.Curso.ToString();
             }

             Data_Par = DataSelecionado.Data.ToString();
             if (Data_Par.Equals("Todos"))
             {
                 Data_Par = "%";
             }
             else
             {
                 Data_Par = DataSelecionado.Data.ToString();
             }

             Turno_Par = TurnoSelecionado.Turno.ToString();
             if (Turno_Par.Equals("Todos"))
             {
                 Turno_Par = "%";
             }
             else
             {
                 Turno_Par = TurnoSelecionado.Turno.ToString();
             }

             Hora_Par = HoraSelecionado.Hora.ToString();
             if (Hora_Par.Equals("Todos"))
             {
                 Hora_Par = "%";
             }
             else
             {
                 Hora_Par = HoraSelecionado.Hora.ToString();
             }

             Regime_Par = RegimeSelecionado.Regime.ToString();
             if (Regime_Par.Equals("Todos"))
             {
                 Regime_Par = "%";
             }
             else
             {
                 Regime_Par = RegimeSelecionado.Regime.ToString();
             }

             Calendario_Par = CalendarioSelecionado.Calendario.ToString();
             if (Calendario_Par.Equals("Todos"))
             {
                 Calendario_Par = "%";
             }
             else
             {
                 Calendario_Par = CalendarioSelecionado.Calendario.ToString();
             }


             List<Relatorio> txt = CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosRelatorios(Curso_Par, Data_Par, Turno_Par, Hora_Par, Regime_Par, Calendario_Par);
             RelatoriosGrid = new ObservableCollection<Relatorio>(txt);


             CaminhoTXT = this.caminho;
             BtnCriarDisable = false;    
         }

         public void EscolheLocal()
         {
             FolderBrowserDialog fdb = new FolderBrowserDialog();
             if (fdb.ShowDialog() == DialogResult.OK)
             {
                 //Atribui a variavel de camino o caminho selecionado pelo usuário
                 this.caminho = fdb.SelectedPath.ToString();
                 BtnCriarDisable = true;
             }
             CaminhoTXT = this.caminho;
         }

        private void CriarRelatorio()
        {
            Curso_Par = CursoSelecionado.Curso.ToString();
            if (Curso_Par.Equals("Todos"))
            {
                Curso_Par = "%";
            }
            else
            {
                Curso_Par = CursoSelecionado.Curso.ToString();
            }

            Data_Par = DataSelecionado.Data.ToString();
            if (Data_Par.Equals("Todos"))
            {
                Data_Par = "%";
            }
            else
            {
                Data_Par = DataSelecionado.Data.ToString();
            }

            Turno_Par = TurnoSelecionado.Turno.ToString();
            if (Turno_Par.Equals("Todos"))
            {
                Turno_Par = "%";
            }
            else
            {
                Turno_Par = TurnoSelecionado.Turno.ToString();
            }

            Hora_Par = HoraSelecionado.Hora.ToString();
            if (Hora_Par.Equals("Todos"))
            {
                Hora_Par = "%";
            }
            else
            {
                Hora_Par = HoraSelecionado.Hora.ToString();
            }

            Regime_Par = RegimeSelecionado.Regime.ToString();
            if (Regime_Par.Equals("Todos"))
            {
                Regime_Par = "%";
            }
            else
            {
                Regime_Par = RegimeSelecionado.Regime.ToString();
            }

            Calendario_Par = CalendarioSelecionado.Calendario.ToString();
            if (Calendario_Par.Equals("Todos"))
            {
                Calendario_Par = "%";
            }
            else
            {
                Calendario_Par = CalendarioSelecionado.Calendario.ToString();
            }


            List<Relatorio> txt = CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosRelatorios(Curso_Par, Data_Par, Turno_Par, Hora_Par, Regime_Par, Calendario_Par);
            if (txt.Count() > 0)
            {
                ExportToExcel(txt);
            }
            else
            {
                System.Windows.MessageBox.Show("Não há provas cadastradas com os filtros selecionados.", "Erro !");

            }
        }

         public void Criar()
         {

            try
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, ea) =>
                {
                    CriarRelatorio();

                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    IndicadorRelatorioOcupado = false;
                    // Mensagem de sucesso
                    System.Windows.MessageBox.Show("O seu Arquivo Relatorio.xls Foi Salvo com Sucesso!", "Sucesso!");
                };
                IndicadorRelatorioOcupado = true;
                worker.RunWorkerAsync();
            }
            catch
            {
                System.Windows.MessageBox.Show("Houve um problema ao gerar relatório, tente novamente mais tarde", "Erro!");
            }
             
         }

         //Verifica se existe um caminho.
         private bool PodeCriar()
         {
             return !string.IsNullOrEmpty(CaminhoTXT);
         }

         public void Atualizar()
         {
             Curso_Par = CursoSelecionado.Curso.ToString();
             if (Curso_Par.Equals("Todos"))
             {
                 Curso_Par = "%";
             }
             else
             {
                 Curso_Par = CursoSelecionado.Curso.ToString();
             }
             
             Data_Par = DataSelecionado.Data.ToString();
             if (Data_Par.Equals("Todos"))
             {
                 Data_Par = "%";
             }
             else
             {
                 Data_Par = DataSelecionado.Data.ToString();
             }

             Turno_Par = TurnoSelecionado.Turno.ToString();
             if (Turno_Par.Equals("Todos"))
             {
                 Turno_Par = "%";
             }
             else
             {
                 Turno_Par = TurnoSelecionado.Turno.ToString();
             }

             Hora_Par = HoraSelecionado.Hora.ToString();
             if (Hora_Par.Equals("Todos"))
             {
                 Hora_Par = "%";
             }
             else
             {
                 Hora_Par = HoraSelecionado.Hora.ToString();
             }

             Regime_Par = RegimeSelecionado.Regime.ToString();
             if (Regime_Par.Equals("Todos"))
             {
                 Regime_Par = "%";
             }
             else
             {
                 Regime_Par = RegimeSelecionado.Regime.ToString();
             }

             Calendario_Par = CalendarioSelecionado.Calendario.ToString();
             if (Calendario_Par.Equals("Todos"))
             {
                 Calendario_Par = "%";
             }
             else
             {
                 Calendario_Par = CalendarioSelecionado.Calendario.ToString();
             }

             List<Relatorio> txt = CalendarioDeProvasOficiais.Cliente.Classes.BD.Relatorio_RetornaTodosRelatorios(Curso_Par, Data_Par, Turno_Par, Hora_Par, Regime_Par, Calendario_Par);
             RelatoriosGrid = new ObservableCollection<Relatorio>(txt);
         }

         public void AbrirLocal()
         {
             String local = this.caminho + "\\";
             bool isfile = System.IO.File.Exists(local);
             if (isfile)
             {
                 string argument = @"/select/, " + local;
                 System.Diagnostics.Process.Start("explorer.exe", argument);
             }
             else
             {
                 bool isfolder = System.IO.Directory.Exists(local);
                 if (isfolder)
                 {
                     string argument = @"/select/, " + local;
                     System.Diagnostics.Process.Start("explorer.exe", argument);
                 }
             }
         }

        

         public void ExportToExcel(List<Relatorio> _relatorio)
         {
             // Carregar o aplicativo Excel
             Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

             // Create empty workbook
             excel.Workbooks.Add();

             // Criar planilha 
             Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;

             try
             {

                 workSheet.Cells[1, "A"] = "Regime";
                 workSheet.Cells[1, "B"] = "Campus";
                 workSheet.Cells[1, "C"] = "Turno";
                 workSheet.Cells[1, "D"] = "Curso";
                 workSheet.Cells[1, "E"] = "Dia da Semana";
                 workSheet.Cells[1, "F"] = "Data";
                 workSheet.Cells[1, "G"] = "Hora";
                 workSheet.Cells[1, "H"] = "Turma";
                 workSheet.Cells[1, "I"] = "Disciplina";
                 workSheet.Cells[1, "J"] = "Professor Responsavel";
                 workSheet.Cells[1, "K"] = "Professor Aplicador";
                 workSheet.Cells[1, "L"] = "Divisão";
                 workSheet.Cells[1, "M"] = "Sala";
                 workSheet.Cells[1, "N"] = "Calendario";

                 int row = 2; 
                  String Data_Nova;
                 foreach (Relatorio r in _relatorio)
                 {                    
                     workSheet.Cells[row, "A"] = r.Regime;
                     workSheet.Cells[row, "B"] = r.Campus;
                     workSheet.Cells[row, "C"] = r.Turno;
                     workSheet.Cells[row, "D"] = r.Curso;
                     workSheet.Cells[row, "E"] = r.DiaSemana;
                     Data_Nova = r.Data.Substring(3, 2) + "/";
                     Data_Nova = Data_Nova + r.Data.Substring(0, 2) + "/";
                     Data_Nova = Data_Nova + r.Data.Substring(6, 4);
                     workSheet.Cells[row, "F"] = Data_Nova;
                     workSheet.Cells[row, "G"] = r.Hora;
                     workSheet.Cells[row, "H"] = r.Turma;
                     workSheet.Cells[row, "I"] = r.Disciplina;
                     workSheet.Cells[row, "J"] = r.ProfResp;
                     workSheet.Cells[row, "K"] = r.ProfApl;
                     workSheet.Cells[row, "L"] = r.Divisao;
                     workSheet.Cells[row, "M"] = r.Sala;
                     workSheet.Cells[row, "N"] = r.Calendario;

                     row++;
                 }

                 // estilos predefinidos 
                 workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic3);

                 // Nome do Arquivo
                 string fileName = string.Format(caminho + "\\Relatorio.xls", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

                 // Salvando Arquivo
                 workSheet.SaveAs(fileName);
               
             }
             catch (Exception exception)
             {
                System.Windows.MessageBox.Show("Ocorreu um problema para salvar o arquivo !\n" + exception.Message, "Erro");
             }
             finally
             {
                 // Sair do Excel
                 excel.Quit();

                 if (excel != null)
                     System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                 if (workSheet != null)
                     System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

                 // Limpando Arquivos
                 excel = null;
                 workSheet = null;

                 // Forçar a limpeza do coletor de lixo
                 GC.Collect();
             }
         }

        #endregion

    }
}