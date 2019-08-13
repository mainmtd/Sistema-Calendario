using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class Material
    {
        public string CodExt { get; set; }
        public string Descr { get; set; }
        public string CodMat { get; set; }


        public Material Preenche(string codExt, string descr, string codMat)
        {
            this.CodExt = codExt;
            this.Descr = descr;
            this.CodMat = codMat;

            return this;
        }


        public Material Preenche(DbDataReader dr)
        {
            return this.Preenche(dr["codext"].ToString(), dr["descr"].ToString(), dr["codmat"].ToString());
        }
    }
}
