using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Comum.Interfaces
{
    public interface IBaseBD<T>
    {
        T Preenche(IDataReader dr);
    }
}
