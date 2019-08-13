using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class DisciplinaTurma : IBaseBD<DisciplinaTurma>
    {
        public int CodigoDisciplina { get; set; }
        public int CodigoTurma { get; set; }

        public DisciplinaTurma(int codDisciplina, int codTurma)
        {
            CodigoDisciplina = codDisciplina;
            CodigoTurma = codTurma;
        }
        public DisciplinaTurma()
        {

        }

        public DisciplinaTurma Preenche(System.Data.IDataReader dr)
        {
            int codD = 0, codT = 0;

            int.TryParse(dr["turma_codigo"].ToString(), out codT);
            CodigoTurma = codT;

            int.TryParse(dr["disciplina_codigo"].ToString(), out codD);
            CodigoDisciplina = codD;

            return this;
        }
    }
}
