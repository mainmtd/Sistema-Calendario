using br.com.nucleosplog.GI_BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente.Classes.ComboTree.MatProdServ
{
    public class GIComboTreeVM
    {
        //private List<CM_CTABM> _listMat = new List<CM_CTABM>();
        //private List<SomeHierarchyViewModel> _treeMat = new List<SomeHierarchyViewModel>();

        //private List<CM_CTABM> _listProd = new List<CM_CTABM>();
        //private List<SomeHierarchyViewModel> _treeProd = new List<SomeHierarchyViewModel>();

        //private List<CM_CTABM> _listServ = new List<CM_CTABM>();
        //private List<SomeHierarchyViewModel> _treeServ = new List<SomeHierarchyViewModel>();

        public GIComboTreeVM()
        {
            //Items = new List<SomeHierarchyViewModel>();
            //MontaTree(tipo);
        }

        //public GIComboTreeVM()
        //{
        //    //Items = new List<SomeHierarchyViewModel>();
        //    //MontaTree();

            
        //}

        //public void MontaTree()
        //{
        //    string sqlMat = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = 10 Order by 1,2";
        //    string sqlProd = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = 13 Order by 1,2";
        //    string sqlServ = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = 15 Order by 1,2";

        //    using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
        //    {
        //        //Preenche lista de materiais
        //        var dr = bd.AbreSQL(sqlMat);
        //        while (dr.Read())
        //            _listMat.Add(new CM_CTABM().Preenche(dr));

        //        //Preenche lista de produtos
        //        var dr1 = bd.AbreSQL(sqlProd);
        //        while (dr1.Read())
        //            _listProd.Add(new CM_CTABM().Preenche(dr1));

        //        //Preenche lista de servicos
        //        var dr2 = bd.AbreSQL(sqlServ);
        //        while (dr2.Read())
        //            _listServ.Add(new CM_CTABM().Preenche(dr2));
        //    }

        //    //Preenche tree de Materiais
        //    List<SomeHierarchyViewModel> TodosMaeMat = RetornaListaCodMaeNulo(_listMat, _treeMat);
        //    foreach (var m in TodosMaeMat)
        //        RetornaListaFilhosRecursivo(_listMat, m);
            

        //    //Preenche tree de Produtos
        //    List<SomeHierarchyViewModel> TodosMaeProd = RetornaListaCodMaeNulo(_listProd, _treeProd);
        //    foreach (var m in TodosMaeProd)
        //        RetornaListaFilhosRecursivo(_listProd, m);
            

        //    //Preenche tree de Serviços
        //    List<SomeHierarchyViewModel> TodosMaeServ = RetornaListaCodMaeNulo(_listServ, _treeServ);
        //    foreach (var m in TodosMaeServ)
        //        RetornaListaFilhosRecursivo(_listServ, m);
            
        //}

        //public void MontaTree(TipoTree tipo)
        //{
        //    //Faz select cm_ctabm
        //    List<CM_CTABM> ListaTodosItens = new List<CM_CTABM>();
        //    string sql = "";
        //    if (tipo == TipoTree.Materiais)
        //        sql = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = 10 Order by 1,2";
        //    if (tipo == TipoTree.Produtos)
        //        sql = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = 13 Order by 1,2";
        //    if (tipo == TipoTree.Servicos)
        //        sql = "Select CodMae, Codigo, CodInt, Texto, Descr, Ativo FROM cm_ctabm Where Tab = 15 Order by 1,2";

        //    using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
        //    {
        //        var dr = bd.AbreSQL(sql);

        //        while (dr.Read())
        //        {
        //            ListaTodosItens.Add(new CM_CTABM().Preenche(dr));
        //        }
        //    }


        //    //Lista de Todos os nodes cod mae nulo (nodes 1º nível)
        //    List<SomeHierarchyViewModel> TodosMae = RetornaListaCodMaeNulo(ListaTodosItens, Items);

        //    foreach (var m in TodosMae)
        //    {
        //        RetornaListaFilhosRecursivo(ListaTodosItens, m);
        //    }

        //    if (Items.Count > 0)
        //        this.SelectedItem = this.Items[0];
        //}


        //public List<SomeHierarchyViewModel> RetornaListaCodMaeNulo(List<CM_CTABM> ListaTodosItens, List<SomeHierarchyViewModel> tree)
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
        //        tree.Add(o);
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

        //public /*List<CM_CTABM>*/ void ReturnAllChildren(SomeHierarchyViewModel sh, List<CM_CTABM> tabm)
        //{
        //    if (tabm == null)
        //        tabm = new List<CM_CTABM>();


        //    if (sh != null && sh.Children != null && sh.ItemAtual != null && sh.ItemAtual.CodInt > 0)
        //    {
        //        tabm.Add(sh.ItemAtual);

        //        if(sh.Children != null)
        //        {
        //            foreach (SomeHierarchyViewModel s in sh.Children)
        //            {
        //                ReturnAllChildren(s, tabm);
        //            }
        //        }
                
        //    }


        //    //if (sh != null && sh.ItemAtual != null && sh.ItemAtual.CodInt > 0)
        //    //{
        //    //    tabm.Add(sh.ItemAtual);
        //    //    if(sh.Children != null && sh.Children.Count > 0)
        //    //    {
        //    //        foreach (SomeHierarchyViewModel s in sh.Children)
        //    //        {
        //    //            /*return*/ ReturnAllChildren(s, tabm);
        //    //        }
        //    //    }
                
        //    //}

        //    //return tabm;
        //}

        //public List<SomeHierarchyViewModel> RetornaTreeMateriais()
        //{
        //    if (_treeMat.Count > 0)
        //        this.SelectedItem = this._treeMat[0];

        //    return this._treeMat;
        //}

        //public SomeHierarchyViewModel RetornaItemDaTree(int codint, List<SomeHierarchyViewModel> list)
        //{

        //    if (list != null)
        //    {
        //        foreach (SomeHierarchyViewModel sh in list)
        //        {
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
        //}

        //public List<SomeHierarchyViewModel> RetornaTreeProdutos()
        //{
        //    if (_treeProd.Count > 0)
        //        this.SelectedItem = this._treeProd[0];
        //    return this._treeProd;
        //}

        //public List<SomeHierarchyViewModel> RetornaTreeServicos()
        //{
        //    if (_treeServ.Count > 0)
        //        this.SelectedItem = this._treeServ[0];

        //    return this._treeServ;
        //}

        public List<SomeHierarchyViewModel> Items { get; set; }

        public SomeHierarchyViewModel SelectedItem { get; set; }
    }
}
