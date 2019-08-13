using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.GI.ComponentesWPF.Cliente
{
    public class Favored
    {
        public string Nome { get; set; }
        public string CExterno { get; set; }
        public string Razao { get; set; }
        public string CNPJ { get; set; }
        public string  TipoEndereco { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string TipoFavorecido { get; set; }
        public string CodFavorec { get; set; }
        public string Seq { get; set; }

        public Favored Preenche(string nome, string cExterno,
                                   string razao, string cnpj, string tipoEndereco, string endereco,
                                   string numero, string cidade, string uf, string tipofavorecido, string codfavorec, string seq)
        {
            this.CodFavorec = codfavorec;
            this.Seq = seq;
            this.Nome = nome;
            this.CExterno = cExterno;
            this.Razao = razao;
            this.CNPJ = cnpj;
            this.TipoEndereco = tipoEndereco;
            this.Endereco = endereco;
            this.Numero = endereco;
            this.Cidade = cidade;
            this.UF = uf;
            this.TipoFavorecido = tipofavorecido;

            return this;
        }

        public Favored Preenche(DbDataReader dr)
        {
            string tipo = "";

                    if (dr["ecliente"].ToString() == "S")
                        tipo += "É cliente, ";

                    if (dr["efornec"].ToString() == "S")
                        if (tipo != string.Empty)
                            tipo += "É Fornecedor, ";

                    if (dr["evend"].ToString() == "S")
                        if (tipo != string.Empty)
                            tipo += "É Vendedor, ";

                    if (dr["etransp"].ToString() == "S")
                        if (tipo != string.Empty)
                            tipo += "É Transportadora, ";

                    if (dr["eprospect"].ToString() == "S")
                        if (tipo != string.Empty)
                            tipo += "É Prospect";

                    
                    //tira a última vírgula
                    if(tipo != null)
                        if (tipo.EndsWith(", "))
                            tipo = tipo.Remove(tipo.LastIndexOf(","));

            return this.Preenche(dr["nome"].ToString(),
                                dr["codext"].ToString(), dr["razao"].ToString(),
                                dr["cgccpf"].ToString(),
                                dr["tipoender"].ToString(), dr["endereco"].ToString(),
                                dr["numero"].ToString(), dr["cidade"].ToString(),
                                dr["estado"].ToString(), tipo, dr["codfavorec"].ToString(), dr["seq"].ToString());

            
        }


    }
}
