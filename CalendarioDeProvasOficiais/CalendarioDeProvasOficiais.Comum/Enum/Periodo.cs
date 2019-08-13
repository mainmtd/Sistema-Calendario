using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Enum
{
    public enum Periodo
    {
        Matutino = 1,
        Vespertino = 2,
        Noturno = 3,
        Todos = 4
    }


    public class PeriodoConversor
    {
        public static Periodo NumeroParaPeriodo(int num)
        {
            if (num == 1)
                return Periodo.Matutino;
            else if (num == 2)
                return Periodo.Vespertino;
            else if (num == 3)
                return Periodo.Noturno;
            else
                return Periodo.Todos;
        }

        public static int PeriodoParaNumero(Periodo div)
        {
            if (div == Periodo.Matutino)
                return 1;
            else if (div == Periodo.Vespertino)
                return 2;
            else if (div == Periodo.Noturno)
                return 3;
            else
                return 4;
        }
    }
}
