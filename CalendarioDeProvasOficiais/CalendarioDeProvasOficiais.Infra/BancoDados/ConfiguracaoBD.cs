using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Infra.BancoDados
{
    public class ConfiguracaoBD
    {
        public static string RetornaConexaoBD()
        {
            //return @"Server=localhost;Database=calendario_usjt;Uid=admin;Pwd=admin;default command timeout=0";
            return @"Server=localhost;Database=calendario_usjt;Uid=root;Pwd=root;default command timeout=0";
            //return @"Server=localhost;Database=calendario_usjt;Uid=root;Pwd=251921;default command timeout=0";
            //return @"Server=calendario-usjt.mysql.uhserver.com;Database=calendario_usjt;Uid=calendariousjt;Pwd=despacito@2017;default command timeout=0";
        }
    }
}
