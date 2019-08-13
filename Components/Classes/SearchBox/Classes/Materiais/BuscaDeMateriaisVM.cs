using br.com.nucleosplog.GI_BD;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
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
    public class BuscaDeMateriaisVM : BindableBase
    {
        private IEventAggregator _eventAggregator = Factory.CriaEventAggregator();
        private SearchFor _searchFor;
        private string _texto;
        private string _itemSelecionado;
        private ObservableCollection<string> _itensCombo;
        private Material _itemSelecionadoGrid;
        private ObservableCollection<Material> _campos;
        private DelegateCommand _cmdPesquisar;
        private DelegateCommand _cmdOK;
        private DelegateCommand _cmdCancelar;
        private bool _isFocused;


        public string Texto
        {
            get { return _texto; }
            set { SetProperty(ref _texto, value); }
        }
        public string ItemSelecionado
        {
            get { return _itemSelecionado; }
            set { SetProperty(ref _itemSelecionado, value); }
        }
        public Material ItemSelecionadoGrid
        {
            get { return _itemSelecionadoGrid; }
            set 
            { 
                SetProperty(ref _itemSelecionadoGrid, value);
                this._cmdOK.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<string> ItensCombo
        {
            get { return _itensCombo; }
            set { SetProperty(ref _itensCombo, value); }
        }
        public ObservableCollection<Material> Campos
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
        public bool IsFocused 
        {
            get { return _isFocused; }
            set { SetProperty(ref _isFocused, value); }
        }



        public BuscaDeMateriaisVM(SearchFor s, Window w)
        {
            _searchFor = s;
            this.JanelaPai = w;
            PreencheCombo();
            this._cmdPesquisar = new DelegateCommand(this.OnPesquisar);
            this._cmdOK = new DelegateCommand(this.OnOk, this.CanOk);
            this.CmdCancelar = new DelegateCommand(this.OnCancelar);

            _eventAggregator.GetEvent<KeyDownEvento>().Subscribe(RecebePressedKey);
            _eventAggregator.GetEvent<EnterPressed>().Subscribe(Pesquisar);
        }
        private void RecebePressedKey(string obj)
        {
            this.Texto = obj.ToLower();
        }
        private void Pesquisar(bool obj)
        {
            if (obj && (Texto != "" && Texto != null))
                this.OnPesquisar();
        }
        private void OnCancelar()
        {
            if(JanelaPai != null)
                JanelaPai.Close();
        }
        private bool CanOk()
        {
            return ItemSelecionadoGrid != null;
        }
        private void OnOk()
        {
            
            this._eventAggregator.GetEvent<DadosItemSelecionadoMat>().Publish(ItemSelecionadoGrid);

            OnCancelar();
        }
        private void OnPesquisar()
        {
            this.Campos = new ObservableCollection<Material>(PreencheGrid(RetornaSQL()));
            this.ItemSelecionadoGrid = Campos.FirstOrDefault();
        }
        private void PreencheCombo()
        {
            ItensCombo = new ObservableCollection<string>();

            if (_searchFor == SearchFor.Materiais)
            {
                foreach (var x in RetornaListaMateriais())
                {
                    ItensCombo.Add(x.Key);
                }
            }

            ItemSelecionado = ItensCombo.ElementAt(ItensCombo.IndexOf("Descrição"));
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
        private List<Material> PreencheGrid(string sql)
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
        private string RetornaSQL()
        {
            string sql = "SELECT gm_cmat.codmat, gm_cmat.codext, gm_cmat.descr FROM gm_cmat INNER JOIN gm_ctpes on (gm_cmat.codmat = gm_ctpes.codmat) ";

            if (Texto != string.Empty)
                sql += " WHERE upper(" + RetornaNomeCampo(RetornaListaMateriais(), ItemSelecionado) + ") LIKE '%" + Texto.ToUpper() + "%'";

            return sql;
        }
        private string RetornaNomeCampo(Dictionary<string, string> lista, string itemSelecionado)
        {

            foreach (var x in lista)
            {
                if (x.Key.Equals(itemSelecionado))
                    return x.Value;
            }

            return "gm_cmat.descr";
            
        }
    }
}
