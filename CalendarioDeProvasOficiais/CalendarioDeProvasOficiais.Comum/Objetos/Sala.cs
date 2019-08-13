using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class Sala : BaseBD, IBaseBD<Sala>
    {
        public string Nome { get; set; }

        public Sala Preenche(System.Data.IDataReader dr)
        {
            int cod = 0;

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            Nome = dr["nome"].ToString();

            return this;
        }
    }
}
