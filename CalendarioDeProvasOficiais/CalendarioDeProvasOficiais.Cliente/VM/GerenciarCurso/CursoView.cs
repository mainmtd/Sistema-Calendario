using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Cliente.VM.GerenciarCurso
{
    public class CursoView
    {
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Coordenador { get; set; }
        public string Faculdade { get; set; }
        public Curso Curso { get; set; }

        public CursoView(int codigo, string tipo, string nome, string coordenador, string faculdade, Curso curso)
        {
            Codigo = codigo;
            Tipo = tipo;
            Nome = nome;
            Coordenador = coordenador;
            Faculdade = faculdade;
            Curso = curso;
        }

        public CursoView()
        {

        }

    }
}
