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
using System.ComponentModel;

namespace CalendarioDeProvasOficiais.Cliente.VM
{
    class CapaProvaVM : BaseVM
    {
        private ObservableCollection<Calendario> _listCalendarioProva;
        private Calendario _calendarioProvaSelecionado;

        private ObservableCollection<CapaDeProva> _listCurso;
        private CapaDeProva _cursoSelecionado;

        private ObservableCollection<CapaDeProva> _listData;
        private CapaDeProva _dataSelecionado;

        private ObservableCollection<CapaDeProva> _listHora;
        private CapaDeProva _horaSelecionado;

        private ObservableCollection<CapaDeProva> _listUnidade;
        private CapaDeProva _unidadeSelecionado;

        private ObservableCollection<CapaDeProva> _listPeriodo;
        private CapaDeProva _periodoSelecionado;

        private ObservableCollection<CapaDeProva> _listFaculdade;
        private CapaDeProva _faculdadeSelecionado;

        Stream _imgIconOpen;
        private String _caminhoTXT;
        private bool _indicadorOcupadoCapa, _caminhoSalvar, _btnCriarDisable;
        DelegateCommand _btnCriar, _btnEscolheLocal, _btnAbrirLocal;
        

        public ObservableCollection<Calendario> ListCalendarioProva
        {
            get { return _listCalendarioProva; }
            set { SetProperty(ref _listCalendarioProva, value); }
        }

        public ObservableCollection<CapaDeProva> ListCurso
        {
            get { return _listCurso; }
            set { SetProperty(ref _listCurso, value); }
        }

        public ObservableCollection<CapaDeProva> ListData
        {
            get { return _listData; }
            set { SetProperty(ref _listData, value); }
        }

        public ObservableCollection<CapaDeProva> ListHora
        {
            get { return _listHora; }
            set { SetProperty(ref _listHora, value); }
        }

        public ObservableCollection<CapaDeProva> ListPeriodo
        {
            get { return _listPeriodo; }
            set { SetProperty(ref _listPeriodo, value); }
        }

        public ObservableCollection<CapaDeProva> ListFaculdade
        {
            get { return _listFaculdade; }
            set { SetProperty(ref _listFaculdade, value); }
        }

        public ObservableCollection<CapaDeProva> ListUnidade
        {
            get { return _listUnidade; }
            set { SetProperty(ref _listUnidade, value); }
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
        
        public String CaminhoTXT
        {
            get { return _caminhoTXT; }
            set { SetProperty(ref _caminhoTXT, value); _btnCriar.RaiseCanExecuteChanged(); }
        }

        public Stream ImgIconOpen
        {
            get { return _imgIconOpen; }
            set { SetProperty(ref _imgIconOpen, value); }
        }

        public Calendario CalendarioProvaSelecionado        
        {
            get { return _calendarioProvaSelecionado; }
            set { SetProperty(ref _calendarioProvaSelecionado, value); }
        }

        public CapaDeProva CursoSelecionado
        {
            get { return _cursoSelecionado; }
            set { SetProperty(ref _cursoSelecionado, value); }
        }

        public CapaDeProva DataSelecionado
        {
            get { return _dataSelecionado; }
            set { SetProperty(ref _dataSelecionado, value); }
        }

        public CapaDeProva HoraSelecionado
        {
            get { return _horaSelecionado; }
            set { SetProperty(ref _horaSelecionado, value); }
        }

        public CapaDeProva UnidadeSelecionado
        {
            get { return _unidadeSelecionado; }
            set { SetProperty(ref _unidadeSelecionado, value); }
        }

        public CapaDeProva PeriodoSelecionado
        {
            get { return _periodoSelecionado; }
            set { SetProperty(ref _periodoSelecionado, value); }
        }


        public CapaDeProva FaculdadeSelecionado
        {
            get { return _faculdadeSelecionado; }
            set { SetProperty(ref _faculdadeSelecionado, value); }
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
        
        public int COUNT = 1;

        public CapaProvaVM()
        {
            IndicadorOcupadoCapa = false;

            

            //Instância Botões
            BtnCriar = new DelegateCommand(Criar, PodeCriar);
            BtnEscolheLocal = new DelegateCommand(EscolheLocal);
            BtnAbrirLocal = new DelegateCommand(AbrirLocal);

            //Invoca o Metodo PreparaTela
            PreparaTela();
        }


        private void PreparaTela()
        {
            //Trás a lista de Calendários conforme o BD
            ListCalendarioProva = new ObservableCollection<Calendario>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosCalendarios());
            CalendarioProvaSelecionado = ListCalendarioProva.FirstOrDefault();

            //Trás a lista de Cursos conforme o BD
            ListCurso = new ObservableCollection<CapaDeProva>();
            ListCurso.Add(new CapaDeProva() { Curso = "Todos" });
            ListCurso.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosCursos());
            CursoSelecionado = ListCurso.FirstOrDefault();

            //Trás a lista de Data de prova conforme o BD
            ListData = new ObservableCollection<CapaDeProva>();
            ListData.Add(new CapaDeProva() { DataDeProva = "Todas" });
            ListData.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasDatasProvas());
            DataSelecionado = ListData.FirstOrDefault();

            //Trás a lista de Horarios de prova conforme o BD
            ListHora = new ObservableCollection<CapaDeProva>();
            ListHora.Add(new CapaDeProva() { Horario = "Todas" });
            ListHora.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasHorasProvas());
            HoraSelecionado = ListHora.FirstOrDefault();

            //Trás a lista de Unidade de prova conforme o BD
            ListUnidade = new ObservableCollection<CapaDeProva>();
            ListUnidade.Add(new CapaDeProva() { Unidade = "Todas" });
            ListUnidade.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasUnidades());
            UnidadeSelecionado = ListUnidade.FirstOrDefault();

            //Trás a lista de Faculdade conforme o BD
            ListFaculdade = new ObservableCollection<CapaDeProva>();
            ListFaculdade.Add(new CapaDeProva() { Faculdade = "Todas" });
            ListFaculdade.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodasFaculdade());
            FaculdadeSelecionado = ListFaculdade.FirstOrDefault();

            ListPeriodo = new ObservableCollection<CapaDeProva>();
            ListPeriodo.Add(new CapaDeProva() { Periodo = "Todos" });
            ListPeriodo.AddRange(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosPeriodos());
            PeriodoSelecionado = ListPeriodo.FirstOrDefault();

            //Atribui o caminho de Meus Documentos a variável caminho
            CaminhoTXT = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Concatena o caminho dos Meus Documentos com o caminho da pasta TCC
            ImgIconOpen = CalendarioDeProvasOficiais.Cliente.Classes.Imagens.RetornaImagemOpen();
            BtnCriarDisable = false;                  
        }

        //Metodo que seleciona o destino aonde será salvo as capas de provas
        public void EscolheLocal()
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                //Atribui a variavel de camino o caminho selecionado pelo usuário
                this.CaminhoTXT = fdb.SelectedPath.ToString();
                BtnCriarDisable = true;
            }

            string pasta = RetornaNomePasta();
            string fullPath = Path.Combine(CaminhoTXT, pasta);
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            CaminhoTXT = fullPath;
        }

        private string RetornaNomePasta()
        {
            string pasta = "Capas de Prova_";

            pasta += DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;

            return pasta;
        }

        //Metodo que faz abrir o caminho selecionado.
        public void AbrirLocal()
        {
            String local = this.CaminhoTXT + "\\";
            bool isfile = System.IO.File.Exists(local);
            if (isfile)
            {
                string argument = @"/select/, " + local;
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            else
            {
                bool isfolder = System.IO.Directory.Exists(local);
                if(isfolder)
                {
                    string argument = @"/select/, " + local;
                    System.Diagnostics.Process.Start("explorer.exe", argument);
                }
            }
        }

        public void CriarCapa()
        {
            int Codigo = CalendarioProvaSelecionado.Codigo;

            //Caso o nome do curso serja igual á Todos ele atribui % que representa trazer todos os cursos do DB
            String NomeCurso_Par = CursoSelecionado.Curso.ToString();
            if (NomeCurso_Par.Equals("Todos"))
            {
                NomeCurso_Par = "%";
            }
            else
            {
                NomeCurso_Par = CursoSelecionado.Curso.ToString();
            }

            //Caso o nome do data seja igual á Todas ele atribui % que representa trazer todas as datas do DB
            String DataDeProva_Par = DataSelecionado.DataDeProva.ToString();
            if (DataDeProva_Par.Equals("Todas"))
            {
                DataDeProva_Par = "%";
            }
            else
            {
                DataDeProva_Par = DataSelecionado.DataDeProva.ToString();
            }

            //Caso o nome da hora seja igual á Todas ele atribui % que representa trazer todas as horas do DB
            String Hora_Par = HoraSelecionado.Horario.ToString();
            if (Hora_Par.Equals("Todas"))
            {
                Hora_Par = "%";
            }
            else
            {
                Hora_Par = HoraSelecionado.Horario.ToString();
            }

            //Caso o nome da unidade seja igual á Todas ele atribui % que representa trazer todas as unidades do DB
            String Unidade_Par = UnidadeSelecionado.Unidade.ToString();
            if (Unidade_Par.Equals("Todas"))
            {
                Unidade_Par = "%";
            }
            else
            {
                Unidade_Par = UnidadeSelecionado.Unidade.ToString();
            }

            //Caso o nome da faculdade seja igual á Todas ele atribui % que representa trazer todas as faculdades do DB
            String Faculdade_Par = FaculdadeSelecionado.Faculdade.ToString();
            if (Faculdade_Par.Equals("Todas"))
            {
                Faculdade_Par = "%";
            }
            else
            {
                Faculdade_Par = FaculdadeSelecionado.Faculdade.ToString();
            }

            String Periodo_Par = PeriodoSelecionado.Periodo.ToString();

            //Atribui a variavel txt o metodo RetornaCapaDeProva com os parametros selecionados pelo usuário
            List<CapaDeProva> txt = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaCapaDeProva(Codigo, NomeCurso_Par, DataDeProva_Par, Hora_Par, Unidade_Par, Faculdade_Par, Periodo_Par);

            //Caso quantidade de prova seja diferente de 0
            if (txt.Count() != 0)
            {
                //Faz do inicio ao fim de todas as provas selecionadas conforme os parametros
                foreach (CapaDeProva c in txt)
                {
                    //Cria uma capa de prova de cada vez
                    CapaProva_PDF(c.Unidade, c.Data, c.LocalDeEntrega, c.Solicitante, c.Ramal, c.Curso, c.Faculdade, c.Periodo, c.Turma, c.Disciplina, c.Divisao, c.DataDeProva, c.Horario, c.Duracao, c.Sala, c.Aplicador, c.Responsavel, "", "", "", "", "");
                }
            }
            else
            {
                //Caso não tenha provas retornará mensagem de erro
                System.Windows.MessageBox.Show("Não há provas para Curso e Calendários Selecionados !", "ERRO");
            }

            COUNT = 1;
            BtnCriarDisable = true;
        }

        public void Criar()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                try
                {
                    string pasta = RetornaNomePasta();
                    string fullPath = Path.Combine(CaminhoTXT, pasta);
                    if (!Directory.Exists(fullPath))
                        Directory.CreateDirectory(fullPath);
                    CaminhoTXT = fullPath;

                    CriarCapa();
                }
                catch
                {
                    System.Windows.MessageBox.Show("Erro ao gerar capas de prova! Tente novamente mais tarde.", "Erro!");
                }

            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                IndicadorOcupadoCapa = false;
                //Mensagem de Finalização de geração de capa de prova.
                System.Windows.MessageBox.Show("Capas geradas com sucesso.", "Sucesso!");
            };
            IndicadorOcupadoCapa = true;
            worker.RunWorkerAsync();

        }

        //Verifica se existe um caminho.
        private bool PodeCriar()
        {
            return !string.IsNullOrEmpty(CaminhoTXT);
        }

        public void CapaProva_PDF(string Par_UNIDADE, string Par_DATA, string Par_LOCAL_ENTREGA, string Par_SOLICITANTE, string Par_RAMAL, string Par_CURSO, string Par_FACULDADE, string Par_PERIODO, string Par_SERIE_TURMA, string Par_DISCIPLINA, string Par_DIVISAO, string Par_DATAPROVA, string Par_HORARIO, string Par_DURACAO, string Par_SALA, string Par_PROFESSOR_APLICADOR, string Par_PROFESSOR_RESPONSAVEL, string Par_PROFESSOR_SUBSTITUTO, string Par_QNT_FOLHAS, string Par_QNT_ARQUIVO, string Par_PROVA_IMPRESSA, string Par_NUMERO_PROVAS)
        {
            //Trás as fontes do windonws para utilização no framework
            iTextSharp.text.FontFactory.RegisterDirectory("C:\\WINDOWS\\Fonts");
            //Atribui o tamanho que será gerado o PDF
            var Principal = new iTextSharp.text.Rectangle(1240, 1754);
            var Documento = new Document(Principal);
            //Atribui o caminho conforme as variavez e o contador.
            var NomeArq = CaminhoTXT + "\\" + COUNT++ + " - Unidade " + Par_UNIDADE + ", Curso " + Par_CURSO + ", Disciplina " + Par_DISCIPLINA + ",  Periodo " + Par_PERIODO + ".pdf";

            var writer = PdfWriter.GetInstance(Documento, new FileStream(@NomeArq, FileMode.Create));

            Documento.Open();

            // Img de Background para se inserida as informações
            Stream RetornaImagemCapaRedimensionada = CalendarioDeProvasOficiais.Cliente.Classes.Imagens.RetornaImagemCapaRedimensionada();

            var Teste = iTextSharp.text.Image.GetInstance(RetornaImagemCapaRedimensionada);
            Teste.SetAbsolutePosition(70, 0);
            Documento.Add(Teste);           


            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();

            //Atribui os tipos de fontes com suas caracteristicas
            BaseFont Font01 = BaseFont.CreateFont(BaseFont.HELVETICA_BOLDOBLIQUE, BaseFont.CP1252, false, false);
            BaseFont Font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false, false);
            BaseFont FontTitulo = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, true, true);


            //Posiciona e escreve Unidade no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_UNIDADE, 190, 1332, 0);

            //Posiciona e escreve Data da Geração no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_DATA, 535, 1332, 0);

            //Posiciona e escreve Local de Entrega no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_LOCAL_ENTREGA, 1020, 1332, 0);

            //Posiciona e escreve Solicitante no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT,Par_SOLICITANTE, 205, 1255, 0);

            //Posiciona e escreve Faculdade no PDF
            cb.SetFontAndSize(Font, 20);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_FACULDADE, 205, 1184, 0);

            //Posiciona e escreve Curso no PDF
            cb.SetFontAndSize(Font, 20);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_CURSO, 690, 1184, 0);

            //Posiciona e escreve Periodo no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_PERIODO, 180, 1108, 0);

            //Posiciona e escreve Serie/Turma no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_SERIE_TURMA, 955, 1108, 0);

            //Posiciona e escreve Disciplina no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_DISCIPLINA, 200, 1032, 0);

            //Posiciona e escreve Divisão no PDF
            cb.SetFontAndSize(Font, 23);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_DIVISAO, 900, 1032, 0);

            //Posiciona e escreve Data da Prova no PDF
            cb.SetFontAndSize(Font, 22);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_DATAPROVA, 240, 958, 0);

            //Posiciona e escreve Horário no PDF
            cb.SetFontAndSize(Font, 22);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_HORARIO, 895, 958, 0);

            //Posiciona e escreve Duração no PDF
            cb.SetFontAndSize(Font, 22);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_DURACAO, 190, 880, 0);

            //Posiciona e escreve Sala no PDF
            cb.SetFontAndSize(Font, 22);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Par_SALA, 870, 880, 0);

            //Posiciona e escreve Professor Aplicador no PDF
            cb.SetFontAndSize(Font, 18);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Par_PROFESSOR_APLICADOR, 260, 795, 0);

            //Posiciona e escreve Professor Responsavel no PDF
            cb.SetFontAndSize(Font, 18);
            cb.SetColorFill(new BaseColor(51, 51, 51));
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Par_PROFESSOR_RESPONSAVEL, 970, 795, 0);

            //Fecha Edição
            cb.EndText();

            //Fecha Documento.
            Documento.Close();

        }
    }
}