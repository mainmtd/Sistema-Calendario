using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class CalendarioProfessor : IBaseBD<CalendarioProfessor>
    {
        public int Calendario_Codigo { get; set; }
        public int Professor_Codigo { get; set; }
        public double Carga_Horaria_Disponivel { get; set; }


        public CalendarioProfessor Preenche(System.Data.IDataReader dr)
        {
            int codC = 0, codP = 0;
            double carga = 0.0;

            int.TryParse(dr["calendario_codigo"].ToString(), out codC);
            Calendario_Codigo = codC;

            int.TryParse(dr["professor_codigo"].ToString(), out codP);
            Professor_Codigo = codP;

            double.TryParse(dr["carga_horaria_disponivel"].ToString(), out carga);
            Carga_Horaria_Disponivel = carga;   


            return this;
        }
    }
}
