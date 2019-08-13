using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class CalendarioProva : BaseBD, IBaseBD<CalendarioProva>
    {
        public int Calendario_Codigo { get; set; }
        public int Prova_Codigo { get; set; }

        public CalendarioProva(int codigo, int codCalendario, int codProva)
        {
            Codigo = codigo;
            Calendario_Codigo = codCalendario;
            Prova_Codigo = codProva;
        }
        public CalendarioProva()
        {

        }
        public CalendarioProva Preenche(System.Data.IDataReader dr)
        {
            int cod = 0, codC = 0, codP = 0;

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            int.TryParse(dr["calendario_codigo"].ToString(), out codC);
            Calendario_Codigo = codC;

            int.TryParse(dr["prova_codigo"].ToString(), out codP);
            Prova_Codigo = codP;
            return this;
        }
    }
}
