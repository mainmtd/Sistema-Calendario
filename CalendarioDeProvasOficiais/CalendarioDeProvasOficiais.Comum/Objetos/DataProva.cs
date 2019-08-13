using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class DataProva : BaseBD, IBaseBD<DataProva>
    {
        
        public DateTime Data { get; set; }
        public List<HoraProva> Horas { get; set; }
        

        public DataProva(DateTime data, List<HoraProva> horas)
        {
            Data = data;
            Horas = horas;
        }

        public DataProva()
        {   
        }

        public DataProva Preenche(IDataReader dr)
        {
            DateTime data;
            DateTime.TryParse(dr["data"].ToString(), out data);
            Data = data;

            return this;
        }

        public List<Prova> RetornaProvasPorDia()
        {
            List<Prova> listProva = new List<Prova>();


            foreach (HoraProva hora in Horas)
            {
                listProva.AddRange(hora.Provas);
            }


            return listProva;

        }
    }
}
