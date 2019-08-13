using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente.Classes.ComboTree.PayRec
{
    public enum PayRecTree
    {
        Recebimento = 3,
        Pagamento = 4
    }
    public static class PayRecTreeConversor
    {
        public static int TipoTreeToNum(PayRecTree tipo)
        {
            return (int)tipo;
        }

        public static PayRecTree NumToTipoTree(int num)
        {
            try
            {
                return (PayRecTree)num;
            }
            catch
            {
                return PayRecTree.Recebimento;
            }

        }

        public static PayRecTree SiglaToTipoTree(string sigla)
        {
            switch (sigla)
            {
                case "P":
                    return PayRecTree.Pagamento;
                case "R":
                default:
                    return PayRecTree.Recebimento;
            }
        }
    }
   
}
