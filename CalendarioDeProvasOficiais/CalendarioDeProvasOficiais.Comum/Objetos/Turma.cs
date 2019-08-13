using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class Turma : BaseBD, IBaseBD<Turma>
    {
        public TipoCurso TipoCurso { get; set; }
        public string Nome { get; set; }
        public int CodigoCurso { get; set; }

        public Turma(int codigo, TipoCurso tipoCurso, string nome, int codCurso)
        {
            Codigo = codigo;
            TipoCurso = tipoCurso;
            Nome = nome;
            CodigoCurso = codCurso;
        }

        public Turma()
        {   
        }

        public Turma Preenche(System.Data.IDataReader dr)
        {
            int cod = 0, codC = 0, codCF = 0, tipo = 0;
            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            int.TryParse(dr["tipoCurso"].ToString(), out tipo);
            TipoCurso = TipoCursoConversor.NumeroParaTipoCurso(tipo);

            Nome = dr["nome"].ToString();

            int.TryParse(dr["curso_codigo"].ToString(), out codC);
            CodigoCurso = codC;

            return this;
        }
    }
}
