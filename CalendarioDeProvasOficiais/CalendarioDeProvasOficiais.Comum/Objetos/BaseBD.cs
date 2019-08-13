using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class BaseBD
    {
        public int Codigo { get; set; }

        public BaseBD(int codigo)
        {
            Codigo = codigo;
        }

        public BaseBD()
        {

        }
    }
}
