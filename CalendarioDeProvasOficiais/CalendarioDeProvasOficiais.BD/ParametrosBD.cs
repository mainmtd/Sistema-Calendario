using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.BD
{
    public class ParametrosBD
    {
        public string NomeParametro { get; set; }
        public object ValorParametro { get; set; }

        public ParametrosBD(string nomeParametro, object valorParametro)
        {
            NomeParametro = nomeParametro;
            ValorParametro = valorParametro;
        }
    }
}
