using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente
{
    public enum TipoTree
    {
        Materiais = 10,
        Produtos = 13,
        Servicos = 15
    }

    public static class TipoTreeConversor
    {
        public static int TipoTreeToNum(TipoTree tipo)
        {
            return (int)tipo;
        }

        public static TipoTree NumToTipoTree(int num)
        {
            try
            {
                return (TipoTree)num;
            }
            catch
            {
                return TipoTree.Materiais;
            }
            
        }

        public static TipoTree SiglaToTipoTree(string sigla)
        {
            switch (sigla)
            {               
                case "P":
                    return TipoTree.Produtos;
                case "S":
                    return TipoTree.Servicos;
                case "M":
                default:
                    return TipoTree.Materiais;
            }
        }
    }
}
