using CalendarioDeProvasOficiais.BD;
using CalendarioDeProvasOficiais.Comum.Interfaces;
using CalendarioDeProvasOficiais.Infra.BancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Uteis
{
    public class BD_Util
    {
        public static List<T> RetornaLista<T>(string query, List<ParametrosBD> parametros = null) where T : IBaseBD<T>, new()
        {
            List<T> list = new List<T>();

            IDataReader x = ConexaoBD.RetornaBD(query, parametros);

            while (x.Read())
            {
                T t = new T().Preenche(x);
                list.Add(t);
            }

            return list;
        }

        public static T RetornaItem<T>(string query, List<ParametrosBD> parametros = null) where T : IBaseBD<T>, new()
        {
            IDataReader x = ConexaoBD.RetornaBD(query, parametros);

            if (x.Read())
                return new T().Preenche(x);
            else
                return default(T);
        }

        public static IDataReader RetornaDataReader(string query, List<ParametrosBD> parametros = null)
        {
            IDataReader x = ConexaoBD.RetornaBD(query, parametros);
            return x;
        }

        public static bool AcaoBD(string query, List<ParametrosBD> parametros = null)
        {
            bool sucesso = ConexaoBD.AcaoBD(query, parametros);

            return sucesso;
        }
    }
}
