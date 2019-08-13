using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Enum
{
    public enum TipoProva
    {
        Oficial = 1,
        Substitutiva = 2
    }


    public class TipoProvaConversor
    {
        public static TipoProva NumeroParaTipoCurso(int num)
        {
            if (num == 1)
                return TipoProva.Oficial;
            else
                return TipoProva.Substitutiva;
        }

        public static int TipoCursoParaNumero(TipoProva tipo)
        {
            if (tipo == TipoProva.Oficial)
                return 1;
            else
                return 2;
        }
    }
}
