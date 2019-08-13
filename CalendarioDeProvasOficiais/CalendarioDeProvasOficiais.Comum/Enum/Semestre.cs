using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Enum
{
    public enum Semestre
    {
        Primeiro = 1,
        Segundo  = 2
    }

    public class SemestreConversor
    {
        public static Semestre NumeroParaSemestre(int num)
        {
            if (num == 1)
                return Semestre.Primeiro;
            else
                return Semestre.Segundo;
            
        }

        public static int SemestreParaNumero(Semestre sem)
        {
            if (sem == Semestre.Primeiro)
                return 1;
            else
                return 2;
        }
    }
}
