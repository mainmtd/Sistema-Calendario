using CalendarioDeProvasOficiais.Cliente.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Cliente.Classes
{
    public class Imagens
    {
        public static Stream RetornaNovoLogoUSJT()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("CalendarioDeProvasOficiais.Cliente.Imgs.novoLogoUSJT.png");
        }

        public static Stream RetornaImagemCapaRedimensionada()
        {

            return Assembly.GetExecutingAssembly().GetManifestResourceStream("CalendarioDeProvasOficiais.Cliente.Imgs.ImagemCapaRedimensionada.jpeg");
        }

        public static Stream RetornaImagemOpen()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("CalendarioDeProvasOficiais.Cliente.Imgs.open-folder.jpeg");
        }

        public static Stream RetornaImagemSaoJudas()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("CalendarioDeProvasOficiais.Cliente.Imgs.SaoJudas.png");
        }

        public static Stream RetornaCapaOficial()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("CalendarioDeProvasOficiais.Cliente.Imgs.capa_oficial.docx");
        }
    }
}
