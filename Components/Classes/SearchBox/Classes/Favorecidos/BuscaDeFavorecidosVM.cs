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
//using UIControls.Eventos;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class BuscaDeFavorecidosVM : BindableBase
    {
        private IEventAggregator _eventAggregator = Factory.CriaEventAggregator();
        private SearchFor _searchFor;
        private string _texto;
        private string _itemSelecionado;
        private ObservableCollection<string> _itensCombo;
        private Favorecido _itemSelecionadoGrid;
        private ObservableCollection<Favorecido> _campos;
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
        public Favorecido ItemSelecionadoGrid
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
        public ObservableCollection<Favorecido> Campos
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



        public BuscaDeFavorecidosVM(SearchFor s, Window w)
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
            ItemSelecionadoFav i = new ItemSelecionadoFav(ItemSelecionadoGrid.CodFavorec, ItemSelecionadoGrid.Seq, ItemSelecionadoGrid.Nome);

            this._eventAggregator.GetEvent<DadosItemSelecionadoFav>().Publish(i);

            OnCancelar();
        }
        private void OnPesquisar()
        {
            this.Campos = new ObservableCollection<Favorecido>(PreencheGrid(RetornaSQL()));
            this.ItemSelecionadoGrid = Campos.FirstOrDefault();
        }
        private void PreencheCombo()
        {
            ItensCombo = new ObservableCollection<string>();

            if (_searchFor == SearchFor.Relacionamento)
            {
                foreach (var x in RetornaListaRelacionamento())
                {
                    ItensCombo.Add(x.Key);
                }
            }

            ItemSelecionado = ItensCombo.ElementAt(ItensCombo.IndexOf("Nome"));
        }
        private Dictionary<string, string> RetornaListaRelacionamento()
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
        private List<Favorecido> PreencheGrid(string sql)
        {
            List<Favorecido> list = new List<Favorecido>();

            using (var bd = FactoryGI_LibBD.GI_LibBD(TipoBDAcessar.bdGI))
            {
                var dr = bd.AbreSQL(sql);

                while (dr.Read())
                {
                    Favorecido fav = new Favorecido().Preenche(dr);
                    list.Add(fav);
                }

            }

            return list;

        }
        private string RetornaSQL()
        {
            string sql = "SELECT fv_end.codfavorec, fv_end.seq, fv_favor.nome, fv_favor.codext, fv_end.razao, " +
                         "fv_end.cgccpf, fv_end.tipoender,fv_end.endereco, fv_end.numero, fv_end.cidade, " +
                         "fv_end.estado, fv_favor.ecliente, fv_favor.efornec, fv_favor.evend, fv_favor.etransp, fv_favor.eprospect FROM " +
                         " fv_favor INNER JOIN fv_end on (fv_favor.codfavorec = fv_end.codfavorec)";

            if (Texto != string.Empty)
                sql += " WHERE upper(" + RetornaNomeCampo(RetornaListaRelacionamento(), ItemSelecionado) + ") LIKE '%" + Texto.ToUpper() + "%'";

            //FAZER: Caso não haja nenhum item selecionado, ele varre todos os campos da tabela procurando o que foi digitado no "Texto"

            return sql;
        }
        private string RetornaNomeCampo(Dictionary<string, string> lista, string itemSelecionado)
        {

            foreach (var x in lista)
            {
                if (x.Key.Equals(itemSelecionado))
                    return x.Value;
            }

            return "nome";
            
        }
    }
}

