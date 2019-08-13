using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Cliente.Classes
{
    public class DataHora
    {
        public DateTime Data { get; set; }
        public int Hora { get; set; }


        public DataHora(DateTime data, int hora)
        {
            Data = data;
            Hora = hora;
        }
    }
}
