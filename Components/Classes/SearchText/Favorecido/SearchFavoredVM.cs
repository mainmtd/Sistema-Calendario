using br.com.nucleosplog.GI_BD;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using NL.GI.ComponentesWPF.Cliente;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace NL.GI.ComponentesWPF.Cliente
{
    public class SearchFavoredVM : BindableBase
    {

        private SearchFor _searchFor;
        private String _texto;
        private String _itemSelecionado;
        private ObservableCollection<String> _itensCombo;
        private Object _itemSelecionadoGrid;
        private ObservableCollection<Object> _campos;
        private DelegateCommand _cmdPesquisar;
        private DelegateCommand _cmdOK;
        private DelegateCommand _cmdCancelar;
        public ItemSelecionadoFav fav;
        public Material mat;

        
        public String Texto
        {
            get { return _texto; }
            set 
            { 
                SetProperty(ref _texto, value);
                this._cmdPesquisar.RaiseCanExecuteChanged();
            }
        }
        public String ItemSelecionado
        {
            get { return _itemSelecionado; }
            set 
            { 
                SetProperty(ref _itemSelecionado, value);
            }
        }
        public Object ItemSelecionadoGrid
        {
            get { return _itemSelecionadoGrid; }
            set 
            { 
                SetProperty(ref _itemSelecionadoGrid, value);
                this._cmdOK.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<String> ItensCombo
        {
            get { return _itensCombo; }
            set { SetProperty(ref _itensCombo, value); }
        }
        public ObservableCollection<Object> Campos
        {
            get { return _campos; }
            set { SetProperty(ref _campos, value); }
        }
        public ICommand CmdPesquisar
        {
            get { return _cmdPesquisar; }
            set { this._cmdPesquisar = (DelegateCommand)value; }
        }
        public ICommand CmdOk
        {
            get { return _cmdOK; }
            set { this._cmdOK = (DelegateCommand)value; }
        }
        public ICommand CmdCancelar
        {
            get { return _cmdCancelar; }
            set { this._cmdCancelar = (DelegateCommand)value; }
        }
        public Window JanelaPai { get; set; }

        public SearchFavoredVM(SearchFor s)
        {
            _searchFor = s;
            PreencheCombo();
            this._cmdPesquisar = new DelegateCommand(this.OnPesquisar, this.CanPesquisar);
            this._cmdOK = new DelegateCommand(this.OnOk, this.CanOk);
            this.CmdCancelar = new DelegateCommand(this.OnCancelar);          
        }

        public void RecebePressedKey(string obj)
        {
            this.Texto = obj.ToLower();
        }

        public void RecebeWindow(Window obj)
        {
            this.JanelaPai = obj;
        }

        private void OnCancelar()
        {
            if (JanelaPai != null)
                JanelaPai.Close();
        }
        private bool CanOk()
        {
            return ItemSelecionadoGrid != null;
        }

        private bool CanPesquisar()
        {
            return Texto != null;
        }
       
        //MÉTODOS Q VÃO PRECISAR DA VERIFICAÇÃO 
        private void OnOk()
        {
            if (ChecksRelationship())
            {
                Favored f = ItemSelecionadoGrid as Favored;

                fav = new ItemSelecionadoFav(f.CodFavorec, f.Seq, f.Nome);

                OnCancelar();
            }

            else if (ChecksMaterials())
            {
                mat = ItemSelecionadoGrid as Material;
                OnCancelar();
            } 
            

        }
        private void OnPesquisar()
        {
            if (ChecksRelationship())
            {
                this.Campos = new ObservableCollection<object>(PreencheGridFavored(RetornaSQLFavored()));

                this.ItemSelecionadoGrid = Campos.FirstOrDefault();
            }

            else if (ChecksMaterials())
            {
                this.Campos = new ObservableCollection<object>(PreencheGridMateriais(RetornaSQLMateriais()));
                this.ItemSelecionadoGrid = Campos.FirstOrDefault();
            }  

        }
        private void PreencheCombo()
        {
            ItensCombo = new ObservableCollection<string>();

            if (ChecksRelationship())
            {
                foreach (var x in RetornaListaRelacionamentoFavored())
                {
                    ItensCombo.Add(x.Key);
                }
                ItemSelecionado = ItensCombo.ElementAt(ItensCombo.IndexOf("Nome"));
            }
            
            else if (ChecksMaterials())
            {
                foreach (var x in RetornaListaMateriais())
                {
                    ItensCombo.Add(x.Key);
                }
                ItemSelecionado = ItensCombo.ElementAt(ItensCombo.IndexOf("Descrição"));           
            }

            
        }

        //AQUI PRA BAIXO OS MÉTODO ESPECIFICOS

        private Dictionary<string, string> RetornaListaRelacionamentoFavored()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            list.Add("C. Externo", "fv_favor.codext");
            list.Add("Nome", "fv_favor.nome");
            list.Add("Apelido", "fv_end.apelido");
            list.Add("Razão Social", "fv_end.razao");
            list.Add("Endereço", "fv_end.endereco");
            list.Add("Número", "fv_end.numero");
            list.Add("Complemento", "fv_end.complemento");
            list.Add("Bairro", "fv_end.bairro");
            list.Add("Cidade", "fv_end.cidade");
            list.Add("CEP", "fv_end.cep");
            list.Add("Estado", "fv_end.estado");
            list.Add("DDD", "fv_end.ddd");
            list.Add("Fone1", "fv_end.fone1");
            list.Add("Fone2", "fv_end.fone2");
            list.Add("Fone3", "fv_end.fone3");
            list.Add("Fax", "fv_end.fax");
            list.Add("E-mail", "fv_end.email");
            list.Add("Home Page", "fv_end.homepage");
            list.Add("CNPJ/CPF", "fv_end.cgccpf");
            list.Add("CCM", "fv_end.ccm");
            list.Add("Inscriçao Estadual", "fv_end.ierg");
            list.Add("Cód. Filial", "fv_end.codigofilial");
            list.Add("Sigla", "sigla");
            list.Add("CD", "cd");


            return list;
        }

        private Dictionary<string, string> RetornaListaMateriais()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            list.Add("C. Externo", "gm_cmat.codext");
            list.Add("Descrição", "gm_cmat.descr");
            list.Add("Cód. Barra", "gm_cmat.codbarra");
            list.Add("Cód. Material", "gm_cmat.codmat");

            return list;
        }
        private List<Favored> PreencheGridFavored(string sql)
        {
            List<Favored> list = new List<Favored>();

            using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
            {
                var dr = bd.AbreSQL(sql);

                while (dr.Read())
                {
                    Favored fav = new Favored().Preenche(dr);
                    list.Add(fav);
                }

            }

            return list;

        }
        private List<Material> PreencheGridMateriais(string sql)
        {
            List<Material> list = new List<Material>();

            using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
            {
                var dr = bd.AbreSQL(sql);

                while (dr.Read())
                {
                    Material mat = new Material().Preenche(dr);
                    list.Add(mat);
                }

            }

            return list;

        }

        private string RetornaSQLFavored()
        {
            string sql = "SELECT fv_end.codfavorec, fv_end.seq, fv_favor.nome, fv_favor.codext, fv_end.razao, " +
                         "fv_end.cgccpf, fv_end.tipoender,fv_end.endereco, fv_end.numero, fv_end.cidade, " +
                         "fv_end.estado, fv_favor.ecliente, fv_favor.efornec, fv_favor.evend, fv_favor.etransp, fv_favor.eprospect FROM " +
                         " fv_favor INNER JOIN fv_end on (fv_favor.codfavorec = fv_end.codfavorec)";

            if (Texto != null && ItemSelecionado != null)
                sql += " WHERE upper(" + RetornaNomeCampoFavored(RetornaListaRelacionamentoFavored(), ItemSelecionado) + ") LIKE '%" + Texto.ToUpper() + "%'";

            return sql;
        }

        private string RetornaSQLMateriais()
        {
            string sql = "SELECT gm_cmat.codmat, gm_cmat.codext, gm_cmat.descr FROM gm_cmat INNER JOIN gm_ctpes on (gm_cmat.codmat = gm_ctpes.codmat) ";

            if (Texto != null && ItemSelecionado != null)
                sql += " WHERE upper(" + RetornaNomeCampoMateriais(RetornaListaMateriais(), ItemSelecionado) + ") LIKE '%" + Texto.ToUpper() + "%'";

            return sql;
        }

        private string RetornaNomeCampoFavored(Dictionary<string, string> lista, string itemSelecionado)
        {
            foreach (var x in lista)
            {
                if (x.Key.Equals(itemSelecionado))
                    return x.Value;
            }
            return "nome";            
        }

        private string RetornaNomeCampoMateriais(Dictionary<string, string> lista, string itemSelecionado)
        {

            foreach (var x in lista)
            {
                if (x.Key.Equals(itemSelecionado))
                    return x.Value;
            }

            return "gm_cmat.descr";

        }

        /*
         * FAZER A 2ª TELA ENVIAR O NOME DO FAVORECIDO PRO COMPONENTE APÓS APERTAR O OK --------------------------------------------------------------------          
         */


        private bool ChecksRelationship()
        {            
            bool _flag = false;

                if (_searchFor == SearchFor.Relacionamento)
                {
                    _flag = true;
                }


            return _flag;
        }

        private bool ChecksMaterials()
        {
            bool _flag = false;

            if (_searchFor == SearchFor.Materiais)
            {
                _flag = true;
            }
            return _flag;
        }



    }
}

