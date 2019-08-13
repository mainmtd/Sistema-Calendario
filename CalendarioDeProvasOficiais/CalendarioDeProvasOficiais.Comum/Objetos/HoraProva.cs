using CalendarioDeProvasOficiais.Comum.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Objetos
{
    public class HoraProva : BaseBD, IBaseBD<HoraProva>
    {
        public TimeSpan Hora { get; set; }
        public List<Prova> Provas { get; set; }

        public HoraProva(TimeSpan hora, List<Prova> provas)
        {
            Hora = hora;
            Provas = provas;
        }

        public HoraProva()
        {
                
        }

        public HoraProva Preenche(IDataReader dr)
        {
            TimeSpan hora;
            TimeSpan.TryParse(dr["hora"].ToString(), out hora);
            Hora = hora;

            return this;
        }
     
    }
}
