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
    public class ProfessorDisciplina : BaseBD, IBaseBD<ProfessorDisciplina>
    {
        public int Professor_Codigo { get; set; }
        public int Disciplina_Codigo { get; set; }
        public bool Responsavel { get; set; }
        public Unidade Unidade { get; set; }

        public ProfessorDisciplina(int codProf, int codDisciplina, bool responsavel)
        {
            Professor_Codigo = codProf;
            Disciplina_Codigo = codDisciplina;
            Responsavel = responsavel;
        }

        public ProfessorDisciplina()
        {           
        }

        public ProfessorDisciplina Preenche(IDataReader dr)
        {
            int  codP = 0, codD = 0, resp = 0, cod = 0, uni = 0;

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            int.TryParse(dr["professor_codigo"].ToString(), out codP);
            Professor_Codigo = codP;

            int.TryParse(dr["disciplina_codigo"].ToString(), out codD);
            Disciplina_Codigo = codD;

            int.TryParse(dr["responsavel"].ToString(), out resp);

            if (resp == 1)
                Responsavel = true;
            else
                Responsavel = false;

            int.TryParse(dr["unidade"].ToString(), out uni);
            Unidade = UnidadeConversor.NumeroParaTipoCurso(uni);

            return this;
        }
    }
}
