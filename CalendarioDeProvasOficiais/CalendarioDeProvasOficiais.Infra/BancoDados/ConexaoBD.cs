using CalendarioDeProvasOficiais.BD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Infra.BancoDados
{
    public class ConexaoBD
    {
        public static MySqlConnection RetornaConexaoBD()
        {
            MySqlConnection conn;
            string connString = ConfiguracaoBD.RetornaConexaoBD();
            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = connString;
                return conn;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Metodo faz alguma ação no BD: Insert, Update ou Delete
        /// </summary>
        /// <param name="query">A query a ser executada.</param>
        /// <param name="parametros">Os parametros da query.</param>
        /// <returns>Retorna se a ação foi bem sucedida.</returns>
        public static bool AcaoBD(string query, List<ParametrosBD> parametros = null)
        {
            bool sucesso = false;
            MySqlConnection conn = RetornaConexaoBD();
            try
            {                
                conn.Open();
                var command = conn.CreateCommand();
                MySqlParameterCollection param;
                if (parametros != null)
                   param = RetornaParametros(command.Parameters, parametros);
                command.CommandText = query;
                command.ExecuteNonQuery();

                sucesso = true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Dispose();
            }

            return sucesso;
        }

        public static IDataReader RetornaBD(string query, List<ParametrosBD> parametros = null)
        {
            MySqlConnection conn = RetornaConexaoBD();
            IDataReader result = null;

            try
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandTimeout = 900;
                    MySqlParameterCollection param;
                    if (parametros != null)
                        param = RetornaParametros(command.Parameters, parametros);
                    command.CommandText = query;
                    result = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(result);
                    result = dt.CreateDataReader() as IDataReader;
                }
                
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Dispose();
            }

            return result;
        }

        private static MySqlParameterCollection RetornaParametros(MySqlParameterCollection pMySql, List<ParametrosBD> param)
        {

            if (param == null || param.Count == 0 || pMySql == null)
                return null;

            foreach (ParametrosBD p in param)
            {
                pMySql.AddWithValue(p.NomeParametro, p.ValorParametro);
            }

            return pMySql;
        }
    }
}
