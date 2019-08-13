using CalendarioDeProvasOficiais.Cliente.View;
using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Objetos;
using ExcelDataReader;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CalendarioDeProvasOficiais.Cliente.VM
{
    public class GerencCalendarioVM : BindableBase
    {
        #region Variáveis
            DelegateCommand _btnImport;
            DelegateCommand _btnOK;
            string testeArquivo;
            ObservableCollection<Prova> _listaProvas;
            ObservableCollection<ResultEscalacao> _provasResult;
            ObservableCollection<Calendario> _listCalendario;
            private Calendario _calendarioSelecionado;
            private bool _indicadorImportacaoOcupado;
            

        #endregion

        #region Propiedades

            public bool IndicadorImportacaoOcupado
            {
                get { return _indicadorImportacaoOcupado; }
                set { SetProperty(ref _indicadorImportacaoOcupado, value); }
            }
            
            public ICommand BtnImport
            {
                get { return _btnImport; }
                set { _btnImport = (DelegateCommand)value; }
            }

            public ICommand BtnOK
            {
                get { return _btnOK; }
                set { _btnOK = (DelegateCommand)value; }
            }

            public string TesteArquivo
            {
                get { return testeArquivo; }
                set
                {
                    SetProperty(ref testeArquivo, value);
                }
            }

            public ObservableCollection<Prova> ListaProvas
            {
                get { return _listaProvas; }
                set { SetProperty(ref _listaProvas, value); }
            }

            public ObservableCollection<ResultEscalacao> ProvasResult
            {
                get { return _provasResult; }
                set { SetProperty(ref _provasResult, value); }
            }

            public ObservableCollection<Calendario> ListCalendario
            {
                get { return _listCalendario; }
                set { SetProperty(ref _listCalendario, value); }
            }

            public Calendario CalendarioSelecionado 
            {
                get { return _calendarioSelecionado; }
                set { SetProperty(ref _calendarioSelecionado, value); }
            }
        #endregion

        public GerencCalendarioVM()
        {
            IndicadorImportacaoOcupado = false;
            PreparaTela();
            
        }

        public void PreparaTela()
        {
            BtnImport = new DelegateCommand(Importar);
            BtnOK = new DelegateCommand(CarregaProvas);
            PreencheListas();
            CarregaProvas();
        }

        private void PreencheListas(bool ultimoSelecionado = false)
        {
            ListCalendario = new ObservableCollection<Calendario>(CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaTodosCalendarios());
            ListaProvas = new ObservableCollection<Prova>();


            if(ultimoSelecionado)
               CalendarioSelecionado = ListCalendario.LastOrDefault();
            else
            CalendarioSelecionado = ListCalendario.FirstOrDefault();
        }

        public void CarregaProvas()
        {
            
            if(CalendarioSelecionado == null){
                System.Windows.Forms.MessageBox.Show("Nenhum calendário selecionado, por favor, selecione um calendário", "Erro");
            }
            else
            {
                List<ResultEscalacao> list = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaProvasPorCalendario(CalendarioSelecionado.Codigo);
                ProvasResult = new ObservableCollection<ResultEscalacao>(list);
            }

            
        }

        public void Importar()
        {
            // Cria o Explorador de Arquivos 
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();


            // Filtra para apenas mostrar xls
            dlg.DefaultExt = ".xls";
            dlg.Filter = "*.xls | *.xlsx";




            // Exibe o Explorador de arquivos
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                string caminho = dlg.FileName.ToString();
                BackgroundWorker worker = new BackgroundWorker();
                bool prontoParaImportar = false;
                worker.DoWork += (o, ea) =>
                {
                    prontoParaImportar = LerExcel(caminho);
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    IndicadorImportacaoOcupado = false;
                    if (prontoParaImportar)
                    {                        
                        InfosCalendario info = new InfosCalendario(ListaProvas);
                        info.ShowDialog();
                        PreencheListas(true);
                        CarregaProvas();
                    }   
                    
                };
                IndicadorImportacaoOcupado = true;
                worker.RunWorkerAsync();
                
            }
                        
        }


        public bool LerExcel(string caminho)
        {
            bool sucesso = false;

            try
            {
                
                FileStream stream = File.Open(caminho, FileMode.Open, FileAccess.Read);



                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {


                    do
                    {
                        bool resultTitulo = false;
                        string tituloCorreto = "";
                        try
                        {
                            reader.Read();
                            string titulo = reader.GetString(0);
                            titulo = titulo + ',' + reader.GetString(1);
                            titulo = titulo + ',' + reader.GetString(2);
                            titulo = titulo + ',' + reader.GetString(3);
                            titulo = titulo + ',' + reader.GetString(4);
                            titulo = titulo + ',' + reader.GetString(5);
                            titulo = titulo + ',' + reader.GetString(6);
                            titulo = titulo + ',' + reader.GetString(7);
                            titulo = titulo + ',' + reader.GetString(8);
                            titulo = titulo + ',' + reader.GetString(9);
                            titulo = titulo + ',' + reader.GetString(10);
                            titulo = titulo + ',' + reader.GetString(11);
                            string tituloCor = "Divisao,Unidade,Tipo,Regime,Duracao,Data,Hora,Sala,Disciplina,Turma,Especial,Curso";
                            tituloCorreto = tituloCor;
                            resultTitulo = string.Compare(titulo, tituloCorreto, true) == 0;
                        }
                        catch
                        {
                            resultTitulo = false;
                        }


                        if (resultTitulo)
                        {
                            while (reader.Read())
                            {
                                Prova p = new Prova();

                                try
                                {
                                    string nomeTurma = "";
                                    string nomeDisciplina = "";
                                    string nomeCurso = "";



                                    p.Divisao = PreencheDivisao(reader.GetString(0));
                                    p.Unidade = PreencheUnidade(reader.GetString(1));
                                    p.Tipo = PreencheTipoProva(reader.GetString(2));
                                    p.Regime = PreencheTipoCurso(reader.GetString(3));
                                    p.Duracao = reader.GetDateTime(4).TimeOfDay;
                                    p.Data = reader.GetDateTime(5);
                                    p.Hora = reader.GetDateTime(6).TimeOfDay;
                                    try
                                    {
                                        p.Sala_Codigo = RetornaSala(reader.GetString(7));
                                    }
                                    catch
                                    {
                                        p.Sala_Codigo = RetornaSala(reader.GetDouble(7).ToString());
                                    }
                                    nomeDisciplina = reader.GetString(8);
                                    nomeCurso = reader.GetString(11);
                                    p.Disciplina_Codigo = RetornaDisciplina(nomeDisciplina, TipoCursoConversor.TipoCursoParaNumero(p.Regime), nomeCurso);

                                    nomeTurma = reader.GetString(9);
                                    p.Turma_Codigo = RetornaTurma(nomeTurma, TipoCursoConversor.TipoCursoParaNumero(p.Regime));
                                    p.Especial = RetornaEspecial(reader.GetString(10));
                                }
                                catch
                                {
                                    System.Windows.Forms.MessageBox.Show("Erro ao validar dados da planilha, por favor, verifique o arquivo e tente novamente.", "Erro");
                                }




                                if (p.Sala_Codigo <= 0 | p.Disciplina_Codigo <= 0 | p.Turma_Codigo <= 0)
                                {
                                    sucesso = false;
                                    break;
                                }
                                else
                                {
                                    ListaProvas.Add(p);
                                    sucesso = true;
                                }


                                sucesso = true;
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Formato de Planilha Incorreto, por favor utilizar uma planilha no formato:\n Divisao,Unidade,Tipo,Regime,Duracao,Data,Hora,Sala,Disciplina,Turma,Especial,Curso", "Erro");
                        }



                    } while (reader.NextResult());




                }
            }
            catch
            {
                System.Windows.MessageBox.Show("O arquivo está aberto. Por favor, feche-o antes de continuar");
            }

            
            return sucesso;
        }

        private TipoProva PreencheTipoProva(string tipo)
        {
            if (tipo.CompareTo("Oficial") == 0)
            {
                return TipoProva.Oficial;
            }
            else
            {
                return TipoProva.Substitutiva;
            }
        }

        private int RetornaTurma(string nomeTurma, int tipoCurso)
        {
            int codTurma = 0;

            codTurma = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaCodTurma(nomeTurma, tipoCurso);
            if (codTurma <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Não foi possível criar o calendário, pois a turma:" + "\n" + nomeTurma + "\n" + "não foi encontrada", "Erro");
            }

            return codTurma;
        }


        
        private int RetornaDisciplina(string nomeDisciplina, int tipoDisciplina, string nomeCurso)
        {
            int codDisciplina = 0;
            int codCurso = 0;

            codCurso = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaCodCurso(nomeCurso, tipoDisciplina);
            
            codDisciplina = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaCodDisciplina(nomeDisciplina, tipoDisciplina, codCurso);
            if (codDisciplina <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Não foi possível criar o calendário, pois a disciplina:" + "\n" + nomeDisciplina + "\n" + "não foi encontrada", "Erro");
                codDisciplina = -1;
            }

            return codDisciplina;
        }

        private int RetornaSala(string nomeSala)
        {
            int codSala = 0;

            codSala = CalendarioDeProvasOficiais.Cliente.Classes.BD.RetornaCodSala(nomeSala);
            if (codSala <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Não foi possível criar o calendário, pois a sala:" + "\n" + nomeSala + "\n" + "não foi encontrada", "Erro");
                codSala = -1;
            }

            return codSala;
        }

        public Divisao PreencheDivisao(string div)
        {
            if (div.CompareTo("Par") == 0)
            {
               return Divisao.Par;
            }
            else if (div.CompareTo("Impar") == 0 | div.CompareTo("Ímpar") == 0)
            {
               return Divisao.Impar;
            }
            else
            {
                return Divisao.Unica;
            }
        }

        public Unidade PreencheUnidade(string uni)
        {
            if (uni.CompareTo("Butanta") == 0 | uni.CompareTo("Butantã") == 0)
            {
                return Unidade.Butata;
            }
            else
            {
                return Unidade.Mooca;
            }
        }

        public TipoCurso PreencheTipoCurso(string tipoC)
        {
            if (tipoC.CompareTo("Anual") == 0)
            {
                return TipoCurso.Anual;
            }
            else
            {
                return TipoCurso.Semestral;
            }
        }
        public bool RetornaEspecial(string esp)
        {
            if (esp.CompareTo("SIM") == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
