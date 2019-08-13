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
    public class Relatorio : BaseBD, IBaseBD<Relatorio>
    {
        public String Calendario { get; set; }
        public String Regime { get; set; }
        public String Campus { get; set; }
        public String Turno { get; set; }
        public String Curso { get; set; }
        public String DiaSemana { get; set; }
        public String Data { get; set; }
        public String Hora { get; set; }
        public String Turma { get; set; }
        public String Disciplina { get; set; }
        public String ProfResp { get; set; }
        public String ProfApl { get; set; }
        public String Divisao { get; set; }
        public String Sala { get; set; }

        public Relatorio(String regime, String campus, String turno, String curso, String diaSemana, String data, String hora, String turma, String disciplina, String profResp, String profApl, String divisao, String sala, String calendario)
        {
            Calendario = calendario;
            Regime = regime;
            Campus = campus;
            Turno = turno;
            Curso = curso;
            DiaSemana = diaSemana;
            Data = data;
            Hora = hora;
            Turma = turma;
            Disciplina = disciplina;
            ProfResp = profResp;
            ProfApl = profApl;
            Divisao = divisao;
            Sala = sala;
        }

        public Relatorio()
        {
        }


        public Relatorio Preenche(IDataReader dr)
        {

            
            // ****** Atribuição do BD ******
            this.Regime = dr["Regime"].ToString();
            this.Campus = dr["Campus"].ToString();
            this.Turno = dr["Turno"].ToString();
            this.Curso = dr["Curso"].ToString();
            this.DiaSemana = dr["DiaDaSemana"].ToString();
            this.Data = dr["Data"].ToString();
            this.Hora = dr["Hora"].ToString();
            this.Turma = dr["Turma"].ToString();
            this.Disciplina = dr["Disciplina"].ToString();
            this.ProfResp = dr["ProfessorResponsavel"].ToString();
            this.ProfApl = dr["ProfessorAplicador"].ToString();
            this.Divisao = dr["Divisao"].ToString();
            this.Sala = dr["Sala"].ToString();
            this.Calendario = dr["Calendario"].ToString();

            return this;
        }
    }
}
