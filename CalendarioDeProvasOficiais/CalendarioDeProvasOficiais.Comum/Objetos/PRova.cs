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
    public class Prova : BaseBD, IBaseBD<Prova>
    {
        public Divisao Divisao { get; set; }
        public Unidade Unidade { get; set; }
        public TipoProva Tipo { get; set; }
        public TimeSpan Duracao { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public int Sala_Codigo { get; set; }
        public int Professor_Codigo { get; set; }
        public int Professor_Suplente_Codigo { get; set; }
        public int Disciplina_Codigo { get; set; }
        public int Turma_Codigo { get; set; }
        public bool Especial { get; set; }
        public string Periodo { get; set; }
        public TipoCurso Regime { get; set; }

        public Prova(int codigo, Divisao div, Unidade uni, TipoProva tipo, TimeSpan duracao,
            DateTime data, TimeSpan hora, int sala, int codigoProf, int codDisciplina, int codTuma)
        {
            Codigo = codigo;
            Divisao = div;
            Unidade = uni;
            Tipo = tipo;
            Duracao = duracao;
            Data = data;
            Hora = hora;
            Sala_Codigo = sala;
            Professor_Codigo = codigoProf;
            Disciplina_Codigo = codDisciplina;
            Turma_Codigo = codTuma;
        }

        public Prova()
        {   
        }
        public Prova Preenche(Prova prova)
        {
            Codigo = prova.Codigo;
            Divisao = prova.Divisao;
            Unidade = prova.Unidade;
            Tipo = prova.Tipo;
            Duracao = prova.Duracao;
            Data = prova.Data;
            Hora = prova.Hora;
            Sala_Codigo = prova.Sala_Codigo;
            Professor_Codigo = prova.Professor_Codigo;
            Professor_Suplente_Codigo = prova.Professor_Suplente_Codigo;
            Disciplina_Codigo = prova.Disciplina_Codigo;
            Turma_Codigo = prova.Turma_Codigo;
            Especial = prova.Especial;
            Periodo = prova.Periodo;
            Regime = prova.Regime;

            return this;
        }

        public Prova Preenche(IDataReader dr)
        {
            int cod = 0,  div = 0, uni = 0,
                tipoP = 0, prof = 0, profSup = 0, disciplina = 0, turma = 0, sala = 0, esp = 0, reg;
            TimeSpan hora = new TimeSpan();
            DateTime data = new DateTime();

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            int.TryParse(dr["divisao"].ToString(), out div);
            Divisao = DivisaoConversor.NumeroParaDivisao(div);

            int.TryParse(dr["unidade"].ToString(), out uni);
            Unidade = UnidadeConversor.NumeroParaTipoCurso(uni);

            int.TryParse(dr["tipo"].ToString(), out tipoP);
            Tipo = TipoProvaConversor.NumeroParaTipoCurso(tipoP);

            int.TryParse(dr["regime"].ToString(), out reg);
            Regime = TipoCursoConversor.NumeroParaTipoCurso(reg);

            int.TryParse(dr["professor_codigo"].ToString(), out prof);
            Professor_Codigo = prof;

            int.TryParse(dr["professor_suplente_codigo"].ToString(), out profSup);
            Professor_Suplente_Codigo = profSup;

            int.TryParse(dr["sala_codigo"].ToString(), out sala);
            Sala_Codigo = sala;

            int.TryParse(dr["disciplina_codigo"].ToString(), out disciplina);
            Disciplina_Codigo = disciplina;

            int.TryParse(dr["turma_codigo"].ToString(), out turma);
            Turma_Codigo = turma;
            
            int.TryParse(dr["especial"].ToString(), out esp);
            Especial = (esp == 1);

            DateTime.TryParse(dr["data"].ToString(), out data);
            Data = data;

            TimeSpan.TryParse(dr["hora"].ToString(), out hora);
            Hora = hora;

            return this;
        }
    }
}
