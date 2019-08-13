using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class Faculdade : BaseBD, IBaseBD<Faculdade>
    {
        public string Nome { get; set; }

        public Faculdade(int codigo, string nome)
        {
            this.Codigo = codigo;
            this.Nome = nome;
        }

        public Faculdade()
        {

        }

        public Faculdade Preenche(IDataReader dr)
        {
            int cod;


            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;
            Nome = dr["nome"].ToString();
            
            return this;
        }
    }
}
