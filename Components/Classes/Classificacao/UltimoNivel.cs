using br.com.nucleosplog.GI_BD;
using NL.GI.ComponentesWPF.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF
{
    public enum Tab
    {
        Favorecidos = 1,
        CentroCustos = 2,
        PlanoContasEntrada = 3,
        PlanoContasSaida = 4,
        Materiais = 10,
        Produtos = 13,
        Servicos = 15

    }
    public static class UltimoNivel
    {
        private static List<SomeHierarchyViewModel> Items = new List<SomeHierarchyViewModel>();
        private static List<SomeHierarchyViewModel> LastItems = new List<SomeHierarchyViewModel>();
        public static List<GIInputCheckbox> Retorna(Tab tab, string checks)
        {
            if (Items.Count > 0)
                Items.Clear();

            if (LastItems.Count > 0)
                LastItems.Clear();

            var itemsCheck = RetornaListaItemsChecados(checks);


            List<GIInputCheckbox> list = new List<GIInputCheckbox>();
            List<CM_CTABM> listCM = new List<CM_CTABM>();


            string sql = "SELECT CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm WHERE Tab = " + TabToString(tab) +" ORDER BY 1,2";
            
            using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
            {
                var dr = bd.AbreSQL(sql);

                while (dr.Read())
                {
                    var x = new CM_CTABM().Preenche(dr);

                    foreach (string s in itemsCheck)
                    {
                        var teste = s.Trim();
                        if (teste.Equals(x.CodInt))
                            x.Checked = true;
                    }

                    listCM.Add(x);
                }
                    
            }


            List<SomeHierarchyViewModel> TodosMae = RetornaListaCodMaeNulo(listCM);

            foreach (var m in TodosMae)
            {
                RetornaListaFilhosRecursivo(listCM, m);
            }

            
            RetornaUltimoNivel(Items);

            foreach (var x in LastItems)
            {
                GIInputCheckbox check = new GIInputCheckbox();
                
                check.Content = x.Title;
                check.Tag = x.CodInt;
                check.IsChecked = x.IsChecked;

                list.Add(check);
            }



            return list;
        }

        private static List<string> RetornaListaItemsChecados(string checks)
        {
            
            List<string> list = new List<string>();

            if (checks == null)
                return list;

            var x = checks.Split(',');
            list.AddRange(x.ToList());


            return list;
        }

        private static List<SomeHierarchyViewModel> RetornaListaCodMaeNulo(List<CM_CTABM> ListaTodosItens)
        {
            List<SomeHierarchyViewModel> outerItems = new List<SomeHierarchyViewModel>();

            foreach (CM_CTABM c in ListaTodosItens.Where(p => p.CodMae == ""))
            {
                SomeHierarchyViewModel o = new SomeHierarchyViewModel(c.Texto, null);
                o.ItemAtual = c;
                o.CodInt = c.CodInt;
                o.IsChecked = c.Checked;
                outerItems.Add(o);
                Items.Add(o);
            }

            return outerItems;
        }

        private static void RetornaListaFilhosRecursivo(List<CM_CTABM> ListaTodosItens, SomeHierarchyViewModel s)
        {
            List<CM_CTABM> ls = ListaTodosItens.Where(p => p.CodMae == s.ItemAtual.CodMae + s.ItemAtual.Codigo).ToList();

            if (s.Children == null)
                s.Children = new List<SomeHierarchyViewModel>();

            foreach (CM_CTABM m in ls)
            {
                //Cria um objeto de hirarquia falando quem é o pai do mesmo para quando ele for selecionado
                //achar sua hierarquia certa
                SomeHierarchyViewModel hierarchy = new SomeHierarchyViewModel(m.Texto, null, s);
                hierarchy.ItemAtual = m;
                hierarchy.CodInt = m.CodInt;
                hierarchy.IsChecked = m.Checked;
                s.Children.Add(hierarchy);
                RetornaListaFilhosRecursivo(ListaTodosItens, hierarchy);
            }
        }


        private static void RetornaUltimoNivel(List<SomeHierarchyViewModel> list)
        {
            foreach (SomeHierarchyViewModel sm in list)
            {
                //if (!(sm.Children == null || sm.Children.Count == 0))
                if ((sm.Children != null) && (sm.Children.Count > 0))
                    RetornaUltimoNivel(sm.Children);
                else
                    LastItems.Add(sm);
            }
            
        }

        private static string TabToString(Tab tab)
        {
            return ((int)tab).ToString();
        }
    }
}
