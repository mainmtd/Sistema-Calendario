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
    public class Disciplina : BaseBD, IBaseBD<Disciplina>
    {
        public string Nome { get; set; }
        public TipoCurso TipoCurso { get; set; }
        public int CodCurso { get; set; }

        public Disciplina(int codigo, string nome, TipoCurso tipoCurso)
        {
            Codigo = codigo;
            Nome = nome;
            TipoCurso = tipoCurso;
        }

        public Disciplina(int codigo, string nome, TipoCurso tipoCurso, int codCurso)
        {
            Codigo = codigo;
            Nome = nome;
            TipoCurso = tipoCurso;
            CodCurso = codCurso;
        }

        public Disciplina()
        {
           
        }

        public Disciplina Preenche(IDataReader dr)
        {
            int cod = 0, tipo = 0, codCur = 0;

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            Nome = dr["nome"].ToString();

            int.TryParse(dr["tipoDisciplina"].ToString(), out tipo);
            TipoCurso = TipoCursoConversor.NumeroParaTipoCurso(tipo);

            int.TryParse(dr["curso_codigo"].ToString(), out codCur);
            CodCurso = codCur;

            return this;
        }
    }
}
