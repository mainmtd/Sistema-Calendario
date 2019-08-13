using br.com.nucleosplog.GI_BD;
using NL.GI.ComponentesWPF.Cliente.Classes;
using NL.GI.ComponentesWPF.Cliente.Classes.ComboTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class ComboTreeGenericVM
    {
        public List<SomeHierarchyViewModel> Items { get; set; }
        public SomeHierarchyViewModel SelectedItem { get; set; }

        public ComboTreeGenericVM(int tabg)
        {
            //Items = new List<SomeHierarchyViewModel>();
            //MontaTree(tabg);
            Items = GIComboTreeUtils.ReturnTree(tabg);

            if (Items.Count > 0)
                this.SelectedItem = this.Items[0];
        }



        //public List<CM_CTABM> ReturnCTABM(int tabG)
        //{
        //    //Faz select cm_ctabm
        //    List<CM_CTABM> listaTodosItens = new List<CM_CTABM>();
        //    string sql = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = " + tabG + " Order by 1,2";

        //    using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
        //    {
        //        var dr = bd.AbreSQL(sql);

        //        while (dr.Read())
        //        {
        //            listaTodosItens.Add(new CM_CTABM().Preenche(dr));
        //        }
        //    }

        //    return listaTodosItens;
        //}

        //public void MontaTree(int tabg)
        //{
        //    List<CM_CTABM> ListaTodosItens = GIComponentsController.ReturnCTABMFromTabG(tabg);


        //    //Lista de Todos os nodes cod mae nulo (nodes 1º nível)
        //    List<SomeHierarchyViewModel> TodosMae = RetornaListaCodMaeNulo(ListaTodosItens);

        //    foreach (var m in TodosMae)
        //    {
        //        RetornaListaFilhosRecursivo(ListaTodosItens, m);
        //    }
        //}


        //public List<SomeHierarchyViewModel> RetornaListaCodMaeNulo(List<CM_CTABM> ListaTodosItens)
        //{
        //    List<SomeHierarchyViewModel> outerItems = new List<SomeHierarchyViewModel>();

        //    bool _addEmptyLevel = true;

        //    foreach (CM_CTABM c in ListaTodosItens.Where(p => p.CodMae == ""))
        //    {
        //        //Cria um item primeiro nível vazio, caso o usuário não deseje selecionar nada
        //        if (_addEmptyLevel)
        //        {
        //            CM_CTABM cm = new CM_CTABM();
        //            cm.Texto = "                                          ";
        //            cm.CodMae = "";
        //            cm.CodInt = 0;
        //            cm.Codigo = "0";

        //            SomeHierarchyViewModel s = new SomeHierarchyViewModel(cm.Texto, null);
        //            s.ItemAtual = cm;
        //            s.CodInt = cm.CodInt;
        //            outerItems.Add(s);
        //            Items.Add(s);

        //            _addEmptyLevel = false; 
        //        }


        //        SomeHierarchyViewModel o = new SomeHierarchyViewModel(c.Texto, null);
        //        o.ItemAtual = c;
        //        o.CodInt = c.CodInt;
        //        outerItems.Add(o);
        //        Items.Add(o);
        //    }

        //    return outerItems;
        //}

        //public void RetornaListaFilhosRecursivo(List<CM_CTABM> ListaTodosItens, SomeHierarchyViewModel s)
        //{
        //    List<CM_CTABM> ls = ListaTodosItens.Where(p => p.CodMae == s.ItemAtual.CodMae + s.ItemAtual.Codigo).ToList();

        //    if (s.Children == null)
        //        s.Children = new List<SomeHierarchyViewModel>();

        //    foreach (CM_CTABM m in ls)
        //    {
        //        //Cria um objeto de hirarquia falando quem é o pai do mesmo para quando ele for selecionado
        //        //achar sua hierarquia certa
        //        SomeHierarchyViewModel hierarchy = new SomeHierarchyViewModel(m.Texto, null, s);
        //        hierarchy.ItemAtual = m;
        //        hierarchy.CodInt = m.CodInt;
        //        s.Children.Add(hierarchy);
        //        RetornaListaFilhosRecursivo(ListaTodosItens, hierarchy);
        //    }
        //}

        
    }
}
 