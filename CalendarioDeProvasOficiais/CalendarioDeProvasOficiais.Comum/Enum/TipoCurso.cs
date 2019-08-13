using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Enum
{
    public enum TipoCurso
    {
        [Description("Anual")]
        Anual = 1,
        [Description("Semestral")]
        Semestral = 2,
        [Description("Não especificado")]
        NaoEspecificado = 99
    }

    public class TipoCursoConversor
    {
        public static TipoCurso NumeroParaTipoCurso(int num)
        {
            if (num == 1)
                return TipoCurso.Anual;
            else if (num == 2)
                return TipoCurso.Semestral;
            else
                return TipoCurso.NaoEspecificado;
        }

        public static int TipoCursoParaNumero(TipoCurso tipo)
        {
            if (tipo == TipoCurso.Anual)
                return 1;
            else if (tipo == TipoCurso.Semestral)
                return 2;
            else
                return 99;
        }
    }



}
