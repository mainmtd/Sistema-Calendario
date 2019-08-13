using br.com.nucleosplog.GI_BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente.Classes
{
    public class GIComponentsController
    {
        #region ComboTree

        public static List<CM_CTABM> ReturnCTABMFromTabG(int tabG)
        {
            //Faz select cm_ctabm
            List<CM_CTABM> listaTodosItens = new List<CM_CTABM>();
            string sql = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = " + tabG + " Order by 1,2";

            using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
            {
                var dr = bd.AbreSQL(sql);

                while (dr.Read())
                {
                    listaTodosItens.Add(new CM_CTABM().Preenche(dr));
                }
            }

            return listaTodosItens;
        }

        #endregion
    }
}
