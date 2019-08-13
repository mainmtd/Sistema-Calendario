using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class CapaDeProva : BaseBD, IBaseBD<CapaDeProva>
    {
        public String Unidade { get; set; }
        public String Data { get; set; }
        public String LocalDeEntrega { get; set; }
        public String Solicitante { get; set; }
        public String Ramal  { get; set; }
        public String Curso { get; set; }
        public String Faculdade { get; set; }
        public String Periodo { get; set; }
        public String Turma  { get; set; }
        public String Disciplina { get; set; }
        public String Divisao { get; set; }
        public String DataDeProva { get; set; }
        public String Horario { get; set; }
        public String Duracao { get; set; }
        public String Sala{ get; set; }
        public String Aplicador { get; set; }
        public String Responsavel { get; set; }
        public String CaminhoTXT { get; set; }


        public CapaDeProva(String Unidade, String Data, String LocalDeEntrega, String Solicitante, String Ramal, String Curso, String Faculdade, String Periodo, String Turma, String Disciplina, String Divisao, String DataDeProva, String Horario, String Duracao, String Sala, String Aplicador, String Responsavel, String CaminhoTXT)
        {
            this.Unidade = Unidade;
            this.Data = Data;
            this.LocalDeEntrega = LocalDeEntrega;
            this.Solicitante = Solicitante;
            this.Ramal  = Ramal;
            this.Curso = Curso;
            this.Faculdade = Faculdade;
            this.Periodo = Periodo;
            this.Turma  = Turma;
            this.Disciplina = Disciplina;
            this.Divisao = Divisao;
            this.DataDeProva = DataDeProva;
            this.Horario = Horario;
            this.Duracao = Duracao;
            this.Sala = Sala;
            this.Aplicador = Aplicador;
            this.Responsavel = Responsavel;
            this.CaminhoTXT = CaminhoTXT;
        }

        public CapaDeProva()
        {
        }


        public CapaDeProva Preenche(IDataReader dr)
        {

            
            DateTime data, dataDeProva ;
            // ****** Atribuição do BD ******
            this.Unidade = dr["Unidade"].ToString();

            DateTime.TryParse(dr["data"].ToString(), out data);
            this.Data = data.ToString("dd/MM/yyyy");

            this.LocalDeEntrega = dr["LocalDeEntrega"].ToString();

            this.Solicitante = dr["Solicitante"].ToString();

            this.Ramal = dr["Ramal"].ToString();

            this.Curso = dr["Curso"].ToString();

            this.Faculdade = dr["Faculdade"].ToString();

            this.Periodo = dr["Periodo"].ToString();

            this.Turma = dr["Turma"].ToString();

            this.Disciplina = dr["Disciplina"].ToString();


            this.Divisao = dr["Divisao"].ToString();


            DateTime.TryParse(dr["DatadaProva"].ToString(), out dataDeProva);
            this.DataDeProva = dataDeProva.ToString("dd-MM-yyyy"); ;

            //TimeSpan.TryParse(dr["Horario"].ToString(), out horario);
                //this.Horario = horario;
            this.Horario = dr["Horario"].ToString();

            this.Duracao = dr["Duracao"].ToString();

            this.Sala = dr["Sala"].ToString();

            this.Aplicador = dr["Aplicador"].ToString();

            this.Responsavel = dr["Responsavel"].ToString();

            //this.CaminhoTXT = dr["CaminhoTXT"].ToString();

            return this;
        }
    }
}
