using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class Fila_Reserva : BaseBD, IBaseBD<Fila_Reserva>
    {
        public int Calendario_Codigo { get; set; }
        public int Professor_Codigo { get; set; }
        public DateTime Data_Fila { get; set; }
        public TimeSpan Hora_Fila { get; set; }
        public Unidade Unidade { get; set; }
        public bool Engenharia { get; set; }

        public Fila_Reserva Preenche(System.Data.IDataReader dr)
        {
            int calendario = 0, prof = 0, uni = 0, eng = 0;
            TimeSpan hora = new TimeSpan();
            DateTime data = new DateTime();

            int.TryParse(dr["calendario_codigo"].ToString(), out calendario);
            Calendario_Codigo = calendario;

            int.TryParse(dr["professor_codigo"].ToString(), out prof);
            Professor_Codigo = prof;

            DateTime.TryParse(dr["Data_Fila"].ToString(), out data);
            Data_Fila = data;

            TimeSpan.TryParse(dr["Hora_Fila"].ToString(), out hora);
            Hora_Fila = hora;

            int.TryParse(dr["unidade"].ToString(), out uni);
            Unidade = UnidadeConversor.NumeroParaTipoCurso(uni);

            int.TryParse(dr["engenharia"].ToString(), out eng);
            Engenharia = (eng == 1);

            return this;
        }
    }
}
