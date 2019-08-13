using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class Disponibilidade : BaseBD, IBaseBD<Disponibilidade>
    {

        public int Dia_Semana { get; set; }
        public TimeSpan Horario { get; set; }
        public int Professor_Codigo { get; set; }
        public TipoCurso TipoCurso { get; set; }
        public Unidade Unidade { get; set; }
        public int Ano { get; set; }
        public int Curso_Codigo { get; set; }

        public Disponibilidade(int cod, int dia_semana, TimeSpan horario, int prof_cod, TipoCurso tipoCurso, Unidade unidade, int ano)
        {
            this.Codigo = cod;
            this.Dia_Semana = dia_semana;
            this.Horario = horario;
            this.Professor_Codigo = prof_cod;
            this.TipoCurso = tipoCurso;
            this.Unidade = unidade;
            this.Ano = ano;
        }

        public Disponibilidade()
        {
            
        }

        public Disponibilidade Preenche(IDataReader dr)
        {
            int cod = 0, prof_cod, dia_semana, tipo = 0, unidade = 0, ano = 0, cursocod = 0;
            TimeSpan horario = new TimeSpan();

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            int.TryParse(dr["tipoCurso"].ToString(), out tipo);
            TipoCurso = TipoCursoConversor.NumeroParaTipoCurso(tipo);

            int.TryParse(dr["dia_semana"].ToString(), out dia_semana);
            Dia_Semana = dia_semana;

            int.TryParse(dr["professor_codigo"].ToString(), out prof_cod);
            Professor_Codigo = prof_cod;

            TimeSpan.TryParse(dr["horario"].ToString(), out horario);
            Horario = horario;

            int.TryParse(dr["unidade"].ToString(), out unidade);
            Unidade = UnidadeConversor.NumeroParaTipoCurso(unidade);

            int.TryParse(dr["ano"].ToString(), out ano);
            Ano = ano;

            int.TryParse(dr["curso_codigo"].ToString(), out cursocod);
            Curso_Codigo = cursocod;


            return this;
        }
    }
}
