using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Enum
{
    public enum Unidade
    {
        Butata = 2,
        Mooca = 1        
    }

    public class UnidadeConversor
    {
        public static Unidade NumeroParaTipoCurso(int num)
        {
            if (num == 1)
                return Unidade.Mooca;
            else
                return Unidade.Butata;
        }

        public static int TipoCursoParaNumero(Unidade tipo)
        {
            if (tipo == Unidade.Mooca)
                return 1;
            else
                return 2;
        }
    }
}
