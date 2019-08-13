using br.com.nucleosplog.GI_Uteis.Classes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class CM_CTABM
    {
        public string CodMae { get; set; }
        public string Codigo { get; set; }
        public int CodInt { get; set; }
        public string Texto { get; set; }
        public bool Checked { get; set; }




        public CM_CTABM Preenche(string codMae, string codigo, int codInt, string texto, bool check)
        {
            this.CodMae = codMae;
            this.Codigo = codigo;
            this.CodInt = codInt;
            this.Texto = texto;
            this.Checked = check;

            return this;
        }


        public CM_CTABM Preenche(string codMae, string codigo, int codInt, string texto)
        {
            this.CodMae = codMae;
            this.Codigo = codigo;
            this.CodInt = codInt;
            this.Texto = texto;

            return this;
        }



        public CM_CTABM Preenche(DbDataReader dr)
        {
            return this.Preenche(dr["codmae"].ToString(),
                                dr["codigo"].ToString(),
                                GI_Conversores.ObjectToInt32(dr["codint"]),
                                dr["texto"].ToString());
        }








    }
}
