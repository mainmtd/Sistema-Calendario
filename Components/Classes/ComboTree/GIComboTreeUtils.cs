using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente.Classes.ComboTree
{
    public static class GIComboTreeUtils
    {
        public static List<SomeHierarchyViewModel> ReturnTree(int tabg)
        {
            List<CM_CTABM> ListaTodosItens = GIComponentsController.ReturnCTABMFromTabG(tabg);
            List<SomeHierarchyViewModel> list = new List<SomeHierarchyViewModel>();

            //Lista de Todos os nodes cod mae nulo (nodes 1º nível)
            List<SomeHierarchyViewModel> TodosMae = RetornaListaCodMaeNulo(ListaTodosItens, list);

            foreach (var m in TodosMae)
            {
                RetornaListaFilhosRecursivo(ListaTodosItens, m);
            }

            return list;
        }
        public static List<SomeHierarchyViewModel> RetornaListaCodMaeNulo(List<CM_CTABM> ListaTodosItens, List<SomeHierarchyViewModel> list)
        {
            List<SomeHierarchyViewModel> outerItems = new List<SomeHierarchyViewModel>();

            bool _addEmptyLevel = true;

            foreach (CM_CTABM c in ListaTodosItens.Where(p => p.CodMae == ""))
            {
                //Cria um item primeiro nível vazio, caso o usuário não deseje selecionar nada
                if (_addEmptyLevel)
                {
                    CM_CTABM cm = new CM_CTABM();
                    cm.Texto = "                                          ";
                    cm.CodMae = "";
                    cm.CodInt = 0;
                    cm.Codigo = "0";

                    SomeHierarchyViewModel s = new SomeHierarchyViewModel(cm.Texto, null);
                    s.ItemAtual = cm;
                    s.CodInt = cm.CodInt;
                    outerItems.Add(s);
                    list.Add(s);

                    _addEmptyLevel = false;
                }


                SomeHierarchyViewModel o = new SomeHierarchyViewModel(c.Texto, null);
                o.ItemAtual = c;
                o.CodInt = c.CodInt;
                outerItems.Add(o);
                list.Add(o);
            }

            return outerItems;
        }
        public static void RetornaListaFilhosRecursivo(List<CM_CTABM> ListaTodosItens, SomeHierarchyViewModel s)
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
                s.Children.Add(hierarchy);
                RetornaListaFilhosRecursivo(ListaTodosItens, hierarchy);
            }
        }
        public static void ReturnAllChildren(SomeHierarchyViewModel sh, List<CM_CTABM> tabm)
        {
            if (tabm == null)
                tabm = new List<CM_CTABM>();


            if (sh != null && sh.Children != null && sh.ItemAtual != null && sh.ItemAtual.CodInt > 0)
            {
                tabm.Add(sh.ItemAtual);

                if (sh.Children != null)
                {
                    foreach (SomeHierarchyViewModel s in sh.Children)
                    {
                        ReturnAllChildren(s, tabm);
                    }
                }

            }
        }
        public static SomeHierarchyViewModel RetornaItemDaTree(List<SomeHierarchyViewModel> list, int codint)
        {

            if (list == null)
                return null;

            var x = list.Where(p => p.CodInt == codint).FirstOrDefault();
            SomeHierarchyViewModel z = null;

            if (x != null)
                return x;
            else
            {
                foreach (var y in list)
                {
                    if (y.Children.Count <= 0)
                        continue;

                    z = RetornaItemDaTree(y.Children, codint);

                    if (z != null)
                        return z;
                }
            }

            return null;

        //    if (list != null)
        //    {
        //        foreach (SomeHierarchyViewModel sh in list)
        //        {
        //            if (codint == 360)
        //            {

        //            }
        //            if (sh.ItemAtual.CodInt == codint)
        //                return sh;
        //            else
        //            {
        //                if (sh.Children != null && sh.Children.Count > 0)
        //                    return RetornaItemDaTree(codint, sh.Children);

        //            }
        //        }
        //    }


        //    return null;
        }
        public static List<SomeHierarchyViewModel> RetornaTreeMateriais()
        {
            return ReturnTree(10);
        }        
        public static List<SomeHierarchyViewModel> RetornaTreeProdutos()
        {
            return ReturnTree(13);
        }
        public static List<SomeHierarchyViewModel> RetornaTreeServicos()
        {
            return ReturnTree(15);
        }
        public static List<SomeHierarchyViewModel> RetornaTreeRecebimento()
        {
            return ReturnTree(3);
        }
        public static List<SomeHierarchyViewModel> RetornaTreePagamento()
        {
            return ReturnTree(4);
        }
        //public static List<SomeHierarchyViewModel> ReturnItemsHierarchy(int tabG)
        //{
        //    ComboTreeGenericVM vm = new ComboTreeGenericVM(tabG);

        //    if (vm.Items != null)
        //        return vm.Items.OrderBy(p => p.ItemAtual.CodMae + p.ItemAtual.Codigo).ToList();
        //    else
        //        return new List<SomeHierarchyViewModel>();

        //}

        /// <summary>
        /// Retorna todos todos os itens pai do objeto, na ordem da hierarquia.
        /// </summary>
        /// <param name="sh"></param>
        /// <param name="tabm"></param>
        public static void ReturnMae(SomeHierarchyViewModel sh, List<CM_CTABM> tabm)
        {
            if (tabm == null)
                tabm = new List<CM_CTABM>();

            if (sh != null && sh.ItemAtual != null && sh.ItemAtual.CodInt > 0)
            {
                tabm.Insert(0, sh.ItemAtual);

                if (sh.Parent != null)
                    ReturnMae(sh.Parent, tabm);

            }
        }
    }
}
