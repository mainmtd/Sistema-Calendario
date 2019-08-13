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
    public class Curso : BaseBD, IBaseBD<Curso>
    {
        public TipoCurso TipoCurso { get; set; }
        public String Nome { get; set; }
        public int CodigoProfessor { get; set; }
        public int CodigoFaculdade { get; set; }

        public Curso(int codigo, TipoCurso tipoCurso, string nome, int codProfessor, int codFaculdade)
        {
            Codigo = codigo;
            TipoCurso = tipoCurso;
            Nome = nome;
            CodigoProfessor = codProfessor;
            CodigoFaculdade = codFaculdade;
        }
        public Curso()
        {
        }
        public Curso Preenche(IDataReader dr)
        {
            int cod = 0, codP = 0, codF = 0, tipo = 0;

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            Nome = dr["nome"].ToString();

            int.TryParse(dr["tipoCurso"].ToString(), out tipo);
            TipoCurso = TipoCursoConversor.NumeroParaTipoCurso(tipo);

            int.TryParse(dr["Coordenador"].ToString(), out codP);
            CodigoProfessor = codP;

            int.TryParse(dr["faculdade_codigo"].ToString(), out codF);
            CodigoFaculdade = codF;

            return this;
        }


    }


}
