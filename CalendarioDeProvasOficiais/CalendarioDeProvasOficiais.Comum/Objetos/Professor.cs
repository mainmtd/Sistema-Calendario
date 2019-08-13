using CalendarioDeProvasOficiais.Comum.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class Professor : BaseBD, IBaseBD<Professor>
    {
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public bool Mesario { get; set; }
        public double Carga_Horaria { get; set; }
        public double Carga_Horaria_Disponivel { get; set; }
        public string CPF { get; set; }

        public Professor(int codigo, string nome, bool mesario, double carga_horaria, string cpf)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Mesario = mesario;
            this.Carga_Horaria = carga_horaria;
            this.CPF = cpf;
        }

        public Professor()
        {
        }

        

        public Professor Preenche(IDataReader dr)
        {
            int cod = 0, mesario = 0;
            double carga_horaria = 0;

            int.TryParse(dr["codigo"].ToString(), out cod);
            Codigo = cod;

            int.TryParse(dr["mesario"].ToString(), out mesario);
            if (mesario == 1)
                Mesario = true;
            else
                Mesario = false;

            double.TryParse(dr["carga_horaria"].ToString(), out carga_horaria);
            Carga_Horaria = carga_horaria;


            Nome = dr["nome"].ToString();

            CPF = dr["CPF"].ToString();

            return this;
        }
    }
}
