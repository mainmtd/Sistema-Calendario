using CalendarioDeProvasOficiais.Comum.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarTurma
{
    public class TurmaView
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Curso { get; set; }
        public string TipoCurso { get; set; }
        public Turma Turma { get; set; }
    }
}
