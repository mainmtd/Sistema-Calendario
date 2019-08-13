using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Enum
{
    public enum Divisao
    {
        Par = 1,
        Impar = 2,
        Unica = 3
    }


    public class DivisaoConversor
    {
        public static Divisao NumeroParaDivisao(int num)
        {
            if (num == 1)
                return Divisao.Par;
            else if (num == 2)
                return Divisao.Impar;
            else
                return Divisao.Unica;
        }

        public static int DivisaoParaNumero(Divisao div)
        {
            if (div == Divisao.Par)
                return 1;
            else if (div == Divisao.Impar)
                return 2;
            else
                return 3;
        }
    }
}
