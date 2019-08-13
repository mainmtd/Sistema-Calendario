using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class ResultEscalacao : BaseBD, IBaseBD<ResultEscalacao>
    {
        public string Turma { get; set; }
        public string Disciplina { get; set; }
        public Divisao Divisao { get; set; }
        public string Data { get; set; }
        public TimeSpan Horario { get; set; }
        public string Aplicador { get; set; }
        public int CodProfessorAplicador { get; set; }
        public string Responsavel { get; set; }
        //public string Reservas { get; set; }
        public string Suplentes { get; set; }
        public int CodProfessorSuplente { get; set; }
        public string Sala { get; set; }
        public int CodProva { get; set; }

        public ResultEscalacao(string turma, string disciplina, Divisao div, String data,
            TimeSpan hora, string aplicador, string responsavel, string reservas, string suplente, string sala)
        {
            Turma = turma;
            Disciplina = disciplina;
            Divisao = div;
            Data = data;
            Horario = hora;
            Aplicador = aplicador;
            Responsavel = responsavel;
            //Reservas = reservas;
            Suplentes = suplente;
            Sala = sala;
        }

        public ResultEscalacao()
        {
               
        }

        public ResultEscalacao Preenche(IDataReader dr)
        {
            int div = 0, codProfApl = 0, codProfSup = 0, codProva = 0;
            TimeSpan hora = new TimeSpan();
            DateTime data = new DateTime();

            Turma = dr["turma"].ToString();
            Disciplina = dr["disciplina"].ToString();

            int.TryParse(dr["divisao"].ToString(), out div);
            Divisao = DivisaoConversor.NumeroParaDivisao(div);

            DateTime.TryParse(dr["data"].ToString(), out data);
            Data = data.ToString("dd/MM/yyyy"); ;

            TimeSpan.TryParse(dr["horario"].ToString(), out hora);
            Horario = hora;

            Aplicador = dr["aplicador"].ToString();

            Responsavel = dr["responsavel"].ToString();

            Suplentes = dr["ProfSuplente"].ToString();

            Sala = dr["sala"].ToString();

            int.TryParse(dr["codProfAplicador"].ToString(), out codProfApl);
            CodProfessorAplicador = codProfApl;

            int.TryParse(dr["codProfSuplente"].ToString(), out codProfSup);
            CodProfessorSuplente = codProfSup;

            int.TryParse(dr["Prova_Codigo"].ToString(), out codProva);
            CodProva = codProva;

            return this;
        }
        

    }
}
