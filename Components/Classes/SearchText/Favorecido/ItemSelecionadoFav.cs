using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class ItemSelecionadoFav
    {
        public string CodFavorec { get; set; }
        public string Seq { get; set; }
        public string Nome { get; set; }

        public ItemSelecionadoFav(string codFavorec, string seq, string nome)
        {
            this.CodFavorec = codFavorec;
            this.Seq = seq;
            this.Nome = nome;
        }
    }
}
