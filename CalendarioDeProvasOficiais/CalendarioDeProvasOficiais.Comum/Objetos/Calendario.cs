using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class Calendario : BaseBD, IBaseBD<Calendario>
    {
        public int Semestre { get; set; }
        public int CodigoFaculdade { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }

        public Calendario(int codigo, int semestre, int codigoFaculdade, string nome, int ano)
        {
            Codigo = codigo;
            Semestre = semestre;
            CodigoFaculdade = codigoFaculdade;
            Nome = nome;
            Ano = ano;
        }

        public Calendario(int semestre, int codigoFaculdade, string nome, int ano)
        {
            
            Semestre = semestre;
            CodigoFaculdade = codigoFaculdade;
            Nome = nome;
            Ano = ano;
        }

        public Calendario()
        {
        }

        public Calendario Preenche(IDataReader dr)
        {
            int cod = 0, sem = 0, codF = 0, ano = 0;
            

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            int.TryParse(dr["semestre"].ToString(), out sem);
            Semestre = Semestre;

            int.TryParse(dr["faculdade_codigo"].ToString(), out codF);
            CodigoFaculdade = codF;
            
            Nome = dr["nome"].ToString();

            int.TryParse(dr["ano"].ToString(), out ano);
            Ano = ano;

            return this;
        }
    }
}
