using CalendarioDeProvasOficiais.Comum.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarDisciplina
{
    public class DisciplinaView
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Regime { get; set; }
        public Disciplina Disciplina { get; set; }

        public DisciplinaView()
        {

        }

        public DisciplinaView(string nome, string regime)
        {
            Nome = nome;
            Regime = regime;
        }
    }
}
