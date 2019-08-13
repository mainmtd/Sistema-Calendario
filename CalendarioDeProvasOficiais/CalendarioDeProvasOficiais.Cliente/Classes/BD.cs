using CalendarioDeProvasOficiais.BD;
using CalendarioDeProvasOficiais.Cliente.VM.GerenciarCurso;
using CalendarioDeProvasOficiais.Cliente.VM.GerenciarDisciplina;
using CalendarioDeProvasOficiais.Cliente.VM.GerenciarTurma;
using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Objetos;
using CalendarioDeProvasOficiais.Comum.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Cliente.Classes
{

	public class BD
	{
		public static List<Disciplina> RetornaTodosTiposDeDesciplinas()
		{
			List<Disciplina> list = BD_Util.RetornaLista<Disciplina>("SELECT TipoDisciplina FROM Disciplina GROUP BY TipoDisciplina");
			return list;
		}
		public static List<Professor> RetornaTodosProfessores()
		{
			List<Professor> list = BD_Util.RetornaLista<Professor>("SELECT * FROM Professor ORDER BY Nome");
			return list;
		}
		public static bool RetornaSeProfessorVinculadoAAlgumaProva(int codProfessor)
		{
			//string sql = "SELECT * FROM Prova WHERE Professor_Codigo = " + codProfessor + " LIMIT 1";
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("ProfCodigo", codProfessor));
			parametros.Add(new ParametrosBD("Ano", DateTime.Now.Year));
			parametros.Add(new ParametrosBD("Semestre", DateTime.Now.Month > 6 ? 2 : 1)); //se mes maior que 6, então segundo semestre

			string sql = "SELECT '' FROM Prova INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo " +
						" INNER JOIN Calendario ON Calendario_has_Prova.Calendario_Codigo = Calendario.Codigo " +
						"WHERE Prova.Professor_Codigo = @ProfCodigo AND Calendario.Ano = @Ano AND Calendario.Semestre = @Semestre LIMIT 1";

			IDataReader dr = BD_Util.RetornaDataReader(sql, parametros);

			if (dr.Read())
				return true;
			else
				return false;
		}
		public static bool RetornaSeCursoVinculadoAAlgumaProva(int codCurso)
		{
			//string sql = "SELECT '' FROM Prova INNER JOIN Turma ON Prova.Turma_Codigo = Turma.Curso_Codigo WHERE Turma.Curso_Codigo = " + codCurso + " LIMIT 1";

			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("codCurso", codCurso));
			parametros.Add(new ParametrosBD("Ano", DateTime.Now.Year));
			parametros.Add(new ParametrosBD("Semestre", DateTime.Now.Month > 6 ? 2 : 1)); //se mes maior que 6, então segundo semestre

			string sql = "SELECT '' FROM Prova INNER JOIN Turma on Prova.Turma_Codigo = Turma.Codigo " +
						 "INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo " +
						 "INNER JOIN Calendario ON Calendario_has_Prova.Calendario_Codigo = Calendario.Codigo " +
						 "WHERE Turma.Curso_Codigo = @codCurso AND Calendario.Ano = @Ano AND Calendario.Semestre =  @Semestre LIMIT 1;";

			IDataReader dr = BD_Util.RetornaDataReader(sql, parametros);

			if (dr.Read())
				return true;
			else
				return false;
		}
		public static bool RetornaSeSalaVinculadoAAlgumaProva(int codSala)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("codSala", codSala));
			parametros.Add(new ParametrosBD("Ano", DateTime.Now.Year));
			parametros.Add(new ParametrosBD("Semestre", DateTime.Now.Month > 6 ? 2 : 1)); //se mes maior que 6, então segundo semestre

			string sql = "SELECT '' FROM Prova " +
						 "INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo " +
						 "INNER JOIN Calendario ON Calendario_has_Prova.Calendario_Codigo = Calendario.Codigo " +
						 "WHERE Prova.Sala_Codigo = @codSala AND Calendario.Ano = @Ano AND Calendario.Semestre =  @Semestre LIMIT 1;";

			IDataReader dr = BD_Util.RetornaDataReader(sql, parametros);

			if (dr.Read())
				return true;
			else
				return false;
		}
		public static bool RetornaSeTurmaVinculadoAAlgumaProva(int codTurma)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("CodTurma", codTurma));
			parametros.Add(new ParametrosBD("Ano", DateTime.Now.Year));
			parametros.Add(new ParametrosBD("Semestre", DateTime.Now.Month > 6 ? 2 : 1)); //se mes maior que 6, então segundo semestre

			string sql = "SELECT '' FROM Prova " +
						 "INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo " +
						 "INNER JOIN Calendario ON Calendario_has_Prova.Calendario_Codigo = Calendario.Codigo " +
						 "WHERE Prova.Turma_Codigo = @CodTurma AND Calendario.Ano = @Ano AND Calendario.Semestre =  @Semestre LIMIT 1;";

			IDataReader dr = BD_Util.RetornaDataReader(sql, parametros);

			if (dr.Read())
				return true;
			else
				return false;
		}
		public static bool RetornaSeDisciplinaVinculadaAAlgumaProva(int codDisciplina)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("CodDisciplina", codDisciplina));
			parametros.Add(new ParametrosBD("Ano", DateTime.Now.Year));
			parametros.Add(new ParametrosBD("Semestre", DateTime.Now.Month > 6 ? 2 : 1)); //se mes maior que 6, então segundo semestre

			string sql = "SELECT '' FROM Prova " +
						 "INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo " +
						 "INNER JOIN Calendario ON Calendario_has_Prova.Calendario_Codigo = Calendario.Codigo " +
						 "WHERE Prova.Disciplina_Codigo = @CodDisciplina AND Calendario.Ano = @Ano AND Calendario.Semestre =  @Semestre LIMIT 1;";

			IDataReader dr = BD_Util.RetornaDataReader(sql, parametros);

			if (dr.Read())
				return true;
			else
				return false;
		}

		public static List<Disciplina> RetornaTodasDisciplinas()
		{
			List<Disciplina> list = BD_Util.RetornaLista<Disciplina>("SELECT * FROM Disciplina");
			return list;
		}

		public static void DeletarProf(Professor cp)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", cp.Codigo));


			//Deleta todas as provas vinculadas a esse professor, pois o mesmo não vai existir mais.
			//string sqlProva = "DELETE FROM Prova WHERE Professor_Codigo = @Codigo";
			//BD_Util.AcaoBD(sqlProva, parametros);

			//Deleta Disponibilidade vinculadas a esse professor

			//Deleta Fila reserva vinculada a esse professor

			//Deleta PRofessor_has_Disciplina where CodProf

			
			string sql = "DELETE FROM calendario_usjt.Professor WHERE Codigo = @Codigo;";
			BD_Util.AcaoBD(sql, parametros);

			

		}
		public static void AlterarProf(Professor cp)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", cp.Codigo));
			parametros.Add(new ParametrosBD("CPF", cp.CPF));
			parametros.Add(new ParametrosBD("Nome", cp.Nome));
			parametros.Add(new ParametrosBD("CargaHoraria", cp.Carga_Horaria));
			string sql = "update Professor set CPF = @CPF, Nome = @Nome, Carga_Horaria = @CargaHoraria where Codigo = @Codigo";
			BD_Util.AcaoBD(sql, parametros);

		}
		public static void IncluirProf(Professor cp)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Nome", cp.Nome));
			parametros.Add(new ParametrosBD("Carga_Horaria", cp.Carga_Horaria));
			parametros.Add(new ParametrosBD("CPF", cp.CPF));
			string sql = "INSERT INTO calendario_usjt.Professor (Nome, Carga_Horaria, CPF) VALUES (@Nome, @Carga_Horaria, @CPF)";
			BD_Util.AcaoBD(sql, parametros);
			
		}

		public static void DeletarCurso(Curso curso)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", curso.Codigo));

			////Deleta todas as provas vinculadas a esse curso, pois o mesmo não vai existir mais.
			//string sqlProva = "DELETE FROM Prova WHERE Curso_Codigo = @Codigo";
			//BD_Util.AcaoBD(sqlProva, parametros);


			string sql = "DELETE FROM calendario_usjt.Curso WHERE Codigo = @Codigo;";
			BD_Util.AcaoBD(sql, parametros);
		}
		public static void AlterarCurso(Curso curso)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", curso.Codigo));
			parametros.Add(new ParametrosBD("TipoCurso", TipoCursoConversor.TipoCursoParaNumero(curso.TipoCurso)));
			parametros.Add(new ParametrosBD("Nome", curso.Nome));
			parametros.Add(new ParametrosBD("Coordenador", curso.CodigoProfessor));
			parametros.Add(new ParametrosBD("Faculdade", curso.CodigoFaculdade));
			string sql = "update Curso set TipoCurso = @TipoCurso, Nome = @Nome, Coordenador = @Coordenador, Faculdade_Codigo = @Faculdade where Codigo = @Codigo";
			BD_Util.AcaoBD(sql, parametros);
		}
		public static void IncluirCurso(Curso curso)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("TipoCurso", TipoCursoConversor.TipoCursoParaNumero(curso.TipoCurso)));
			parametros.Add(new ParametrosBD("Nome", curso.Nome));
			parametros.Add(new ParametrosBD("Coordenador", curso.CodigoProfessor));
			parametros.Add(new ParametrosBD("Faculdade", curso.CodigoFaculdade));
			string sql = "insert into Curso (TipoCurso, Nome, Coordenador, Faculdade_Codigo) VALUES (@TipoCurso, @Nome, @Coordenador, @Faculdade)";
			BD_Util.AcaoBD(sql, parametros);
		}

		public static void DeletarTurma(Turma turma)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", turma.Codigo));
			string sql = "DELETE FROM calendario_usjt.Turma WHERE Codigo = @Codigo;";
			BD_Util.AcaoBD(sql, parametros);
		}
		public static void AlterarTurma(Turma turma, List<Disciplina> disciplinas)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", turma.Codigo));
			parametros.Add(new ParametrosBD("TipoCurso", TipoCursoConversor.TipoCursoParaNumero(turma.TipoCurso)));
			parametros.Add(new ParametrosBD("Nome", turma.Nome));
			parametros.Add(new ParametrosBD("Curso_Codigo", turma.CodigoCurso));
			string sql = "update Turma set TipoCurso = @TipoCurso, Nome = @Nome, Curso_Codigo = @Curso_Codigo where Codigo = @Codigo";
			BD_Util.AcaoBD(sql, parametros);

			if (disciplinas != null)
			{
				string sql2 = "delete from Disciplina_has_Turma where Turma_Codigo = @Codigo";
				BD_Util.AcaoBD(sql2, parametros);

				foreach (Disciplina d in disciplinas)
				{
					string sqlInsert = "insert into Disciplina_has_Turma (Disciplina_Codigo, Turma_Codigo) values (" + d.Codigo + ", @Codigo)";
					BD_Util.AcaoBD(sqlInsert, parametros);
				}
			}
		}
		public static void IncluirTurma(Turma turma, List<Disciplina> disciplinas)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("TipoCurso", TipoCursoConversor.TipoCursoParaNumero(turma.TipoCurso)));
			parametros.Add(new ParametrosBD("Nome", turma.Nome));
			parametros.Add(new ParametrosBD("Curso_Codigo", turma.CodigoCurso));
			string sql = "insert into Turma (TipoCurso, Nome, Curso_Codigo) VALUES (@TipoCurso, @Nome, @Curso_Codigo)";
			BD_Util.AcaoBD(sql, parametros);

			Turma t = RetornaUltimaTurma();

			if (disciplinas != null)
			{
				parametros.Add(new ParametrosBD("Codigo", t.Codigo));
				foreach (Disciplina disciplina in disciplinas)
				{

					string sqlInsert = "insert into Disciplina_has_Turma (Disciplina_Codigo, Turma_Codigo) values (" + disciplina.Codigo + ", @Codigo)";
					BD_Util.AcaoBD(sqlInsert, parametros);
				}
			}
		}


		public static void DeletarSala(Sala sala)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", sala.Codigo));
			string sql = "DELETE FROM calendario_usjt.Sala WHERE Codigo = @Codigo;";
			BD_Util.AcaoBD(sql, parametros);
		}
		public static void AlterarSala(Sala sala)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", sala.Codigo));
			parametros.Add(new ParametrosBD("Nome", sala.Nome));
			string sql = "update Sala set Nome = @Nome where Codigo = @Codigo";
			BD_Util.AcaoBD(sql, parametros);
		}
		public static void IncluirSala(Sala sala)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Nome", sala.Nome));
			string sql = "insert into Sala (Nome) VALUES (@Nome)";
			BD_Util.AcaoBD(sql, parametros);
		}

		public static void DeletarDisciplina(Disciplina disciplina)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", disciplina.Codigo));

			//string sql2 = "DELETE FROM Professor_has_Disciplina WHERE Disciplina_Codigo = @Codigo";
			//BD_Util.AcaoBD(sql2, parametros);

			string sql = "DELETE FROM calendario_usjt.Disciplina WHERE Codigo = @Codigo;";
			BD_Util.AcaoBD(sql, parametros);
			
		}
		public static void AlterarDisciplina(Disciplina disciplina, List<Professor> professores)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Codigo", disciplina.Codigo));
			parametros.Add(new ParametrosBD("Nome", disciplina.Nome));
			parametros.Add(new ParametrosBD("TipoDisciplina", TipoCursoConversor.TipoCursoParaNumero(disciplina.TipoCurso)));
			string sql = "update Disciplina set Nome = @Nome, TipoDisciplina = @TipoDisciplina where Codigo = @Codigo";
			BD_Util.AcaoBD(sql, parametros);

			//alterar lista de Professor_hasDisciplina
			//deleta todos os registros onde Disciplina = X
			//inclui tudo dnv
			if (professores != null)
			{
				string sql2 = "delete from Professor_has_Disciplina where Disciplina_Codigo = @Codigo";
				BD_Util.AcaoBD(sql2, parametros);

				foreach (Professor prof in professores)
				{
					string sqlInsert = "insert into Professor_has_Disciplina (Disciplina_Codigo, Professor_Codigo) values (@Codigo, " + prof.Codigo + ")";
					BD_Util.AcaoBD(sqlInsert, parametros);
				}
			}
		}
		public static void IncluirDisciplina(Disciplina disciplina, List<Professor> professores)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Nome", disciplina.Nome));
			parametros.Add(new ParametrosBD("TipoDisciplina", TipoCursoConversor.TipoCursoParaNumero(disciplina.TipoCurso)));
			string sql = "insert into Disciplina (Nome, TipoDisciplina) VALUES (@Nome, @TipoDisciplina)";
			BD_Util.AcaoBD(sql, parametros);
			Disciplina d = RetornaUltimaDisciplina();

			if (professores != null)
			{
				parametros.Add(new ParametrosBD("Codigo", d.Codigo));
				foreach (Professor prof in professores)
				{
					
					string sqlInsert = "insert into Professor_has_Disciplina (Professor_Codigo, Disciplina_Codigo, Responsavel) values (" + prof.Codigo + ", @Codigo, NULL)";
					BD_Util.AcaoBD(sqlInsert, parametros);
				}
			}
		}

		private static Disciplina RetornaUltimaDisciplina()
		{
			string sql = "SELECT * FROM Disciplina ORDER BY 1 DESC LIMIT 1";
			return BD_Util.RetornaItem<Disciplina>(sql);
		}

		private static Turma RetornaUltimaTurma()
		{
			string sql = "SELECT * FROM Turma ORDER BY 1 DESC LIMIT 1";
			return BD_Util.RetornaItem<Turma>(sql);
		}

		public static List<Prova> RetornaTodasProvas()
		{
			List<Prova> list = BD_Util.RetornaLista<Prova>("SELECT * FROM Prova");
			list.OrderBy(p => p.Data).ThenBy(p => p.Hora);
			return list;
		}

		public static List<Professor> RetornaProfessoresQueLecionamDisciplina(int codDisciplina)
		{
			List<ProfessorDisciplina> listPD = BD_Util.RetornaLista<ProfessorDisciplina>("SELECT * FROM Professor_has_Disciplina WHERE Disciplina_Codigo = " + codDisciplina);
			List<Professor> result = new List<Professor>();

			foreach (ProfessorDisciplina pd in listPD)
			{
				result.Add(RetornaProfessor(pd.Professor_Codigo));
			}
			return result;
		}

		public static List<Disciplina> RetornaDisciplinasDaTurma(int codTurma)
		{
			List<DisciplinaTurma> listPD = BD_Util.RetornaLista<DisciplinaTurma>("SELECT * FROM Disciplina_has_Turma WHERE Turma_Codigo = " + codTurma);
			List<Disciplina> result = new List<Disciplina>();

			foreach (DisciplinaTurma pd in listPD)
			{
				result.Add(RetornaDisciplinaPorCodigo(pd.CodigoDisciplina));
			}
			return result;
		}

		public static List<Prova> RetornaProvasCalendario(int cod)
		{

			string sql = "SELECT "
						+ "Turma.Nome AS 'Turma', "
						+ "Disciplina.Nome AS 'Disciplina', "
						+ "Prova.Divisao AS 'Divisao', "
						+ "Prova.data AS 'Data', "
						+ "Prova.Hora AS 'Horario', "
						+ "Professor.Nome AS 'Aplicador', "
						+ "ProfResp.Nome AS 'Responsavel', "
						+ "ProfSup.Nome AS ProfSuplente, "
						+ "Sala.Nome AS 'Sala', "
						+ "Prova.Unidade AS 'Unidade', "
						+ "Prova.Codigo as Prova_Codigo "
						+ "FROM Prova AS Prova "
						+ "INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo "
						+ "INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo "
						+ "INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo "
						+ "LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo "
						+ "INNER JOIN (SELECT * FROM Professor_has_Disciplina "
						+ "	WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo "
						+ "INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo "
						+ "INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo "
						+ "INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo "
						+ "LEFT JOIN Professor AS ProfSup ON Prova.Professor_Suplente_Codigo = Professor.Codigo "
						+ "WHERE Calendario.Codigo = " + cod + " and Professor.Nome is null ORDER BY Prova.Data, Prova.Hora;";

			List<Prova> list = BD_Util.RetornaLista<Prova>(sql);

			//List<Prova> list = BD_Util.RetornaLista<Prova>("SELECT Prova.Codigo, Prova.Divisao, Prova.Unidade, Prova.Tipo, Prova.Duracao, Prova.Data, Prova.Hora, Prova.Sala, Prova.Professor_Codigo, Prova.Disciplina_Codigo, Prova.Turma_Codigo FROM Prova INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo where Calendario_has_Prova.Calendario_Codigo = "+ cod + ";");
			list.OrderBy(p => p.Data).ThenBy(p => p.Hora);
			return list;
		}

		public static List<CapaDeProva> RetornaCapaDeProva(int cod, String Curso)
		{
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>("SELECT 'Mooca' AS Unidade, CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS'Disciplina', Prova.Divisao AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Prova.Sala AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN(SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo WHERE Calendario.Codigo = " + cod + " AND Curso.Nome Like" + "'" + Curso + "'" + ";");
			return list;

		}

		public static List<Curso> RetornaTodosCursosCrudCurso()
		{
			List<Curso> list = BD_Util.RetornaLista<Curso>("SELECT * FROM Curso ORDER BY Nome, TipoCurso");
			return list;
		}

		public static List<Turma> RetornaTodasTurmas()
		{
			List<Turma> list = BD_Util.RetornaLista<Turma>("SELECT * FROM Turma GROUP BY Nome");
			return list;
		}
		public static List<Sala> RetornaTodasSalas()
		{
			List<Sala> list = BD_Util.RetornaLista<Sala>("SELECT * FROM Sala");
			return list;
		}

		public static List<DataProva> RetornaDatasPorCalendario(int cod)
		{
			List<DataProva> list = BD_Util.RetornaLista<DataProva>("SELECT Prova.Data FROM Prova INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo where Calendario_has_Prova.Calendario_Codigo = "+ cod + " GROUP BY Data;");
			foreach (DataProva d in list)
			{
				d.Horas = RetornaHorasPorCalendario(d.Data, cod);
			}

			return list;
		}

		public static List<HoraProva> RetornaHorasPorCalendario(DateTime data, int cod)
		{
			List<HoraProva> list = BD_Util.RetornaLista<HoraProva>("SELECT Prova.Hora FROM Prova INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo where Calendario_has_Prova.Calendario_Codigo = " + cod + " GROUP BY Hora;");
			foreach (HoraProva h in list)
			{
				h.Provas = RetornaProvasPorHorarioDataCalendario(data, h.Hora, cod);
			}

			return list;
		}

		public static List<Prova> RetornaProvasPorHorarioDataCalendario(DateTime data, TimeSpan hora, int cod)
		{
			List<ParametrosBD> paramentros = new List<ParametrosBD>();
			paramentros.Add(new ParametrosBD("codCalendario", cod));
			paramentros.Add(new ParametrosBD("data", data.ToString("yyyy-MM-dd")));
			paramentros.Add(new ParametrosBD("hora", hora));

			string sql = 
				"SELECT Prova.Codigo, Prova.Divisao, Prova.Unidade, Prova.Tipo, " +
				"Prova.Duracao, Prova.Data, Prova.Hora, Prova.Regime, Prova.Sala_Codigo, Prova.Professor_Codigo, " +
				"Prova.Disciplina_Codigo, Prova.Turma_Codigo, Prova.Professor_Suplente_Codigo, Prova.Especial " +
				"FROM Prova INNER JOIN Calendario_has_Prova " +
				"ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo " +
				"WHERE " +
				"Calendario_has_Prova.Calendario_Codigo = " + cod + " AND " +
				"Prova.Data = '" + data.ToString("yyyy-MM-dd") + "' AND Hora = '" + hora + "';";

			List<Prova> list = BD_Util.RetornaLista<Prova>(sql, paramentros);

			

			return list;
		}

		public static CalendarioProva RetornaCalendarioProvaPorCodigo(int codigo)
		{
			string sql = "SELECT * FROM Calendario_has_Prova WHERE Codigo = " + codigo;

			CalendarioProva result = BD_Util.RetornaItem<CalendarioProva>(sql);

			return result;

		}

		public static List<ResultEscalacao> RetornaProvasPorCalendario(int cod)
		{
			string sql = "SELECT "
						+ "Turma.Nome AS 'Turma', "
						+ "Disciplina.Nome AS 'Disciplina', "
						+ "Prova.Divisao AS 'Divisao', "
						+ "Prova.data AS 'Data', "
						+ "Prova.Hora AS 'Horario', "
						+ "Professor.Nome AS 'Aplicador', "
						+ "ProfResp.Nome AS 'Responsavel', "
						+ "Professor.Codigo AS 'codProfAplicador', "
						+ "ProfSup.Codigo as 'codProfSuplente', "
						+ "ProfSup.Nome AS ProfSuplente, "
						+ "Sala.Nome AS 'Sala', "
						+ "Prova.Unidade AS 'Unidade', "
						+ "Prova.Codigo as Prova_Codigo "
						+ "FROM Prova AS Prova "
						+ "INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo "
						+ "INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo "
						+ "INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo "
						+ "LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo "
						+ "INNER JOIN (SELECT * FROM Professor_has_Disciplina "
						+ "	WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo "
						+ "LEFT JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo "
						+ "INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo "
						+ "INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo "
						+ "LEFT JOIN Professor AS ProfSup ON Prova.Professor_Suplente_Codigo = Professor.Codigo "
						+ "WHERE Calendario.Codigo = " + cod + " ORDER BY Prova.Data, Prova.Hora, Disciplina.Nome;";

			List<ResultEscalacao> list = BD_Util.RetornaLista<ResultEscalacao>(sql);
			return list;
		}
		public static bool DiminuiCargaHoraria(double carga, int codCalendario, int codProfessor)
		{
			string sql = "UPDATE Calendario_has_Professor SET Carga_Horaria_Disponivel = (Carga_Horaria_Disponivel - @carga) " +
				"WHERE Calendario_Codigo = @codCalendario AND Professor_Codigo = @codProfessor";

			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("carga", carga));
			parametros.Add(new ParametrosBD("codCalendario", codCalendario));
			parametros.Add(new ParametrosBD("codProfessor", codProfessor));

			bool sucesso = BD_Util.AcaoBD(sql, parametros);

			return sucesso;
		}

		public static List<Calendario> RetornaCalendariosFaculdade(int fac)
		{
			List<ParametrosBD> parametros1 = new List<ParametrosBD>();
			parametros1.Add(new ParametrosBD("codigoF", fac));
			List<Calendario> list = BD_Util.RetornaLista<Calendario>("SELECT * FROM Calendario WHERE Faculdade_Codigo = " + fac);
			return list;
		}

		public static List<Faculdade> RetornaTodasFaculdades()
		{
			List<Faculdade> list = BD_Util.RetornaLista<Faculdade>("SELECT * FROM Faculdade");
			return list;
		}

		public static List<Disponibilidade> RetornaDisponibilidade(List<Professor> profs, DateTime date, int hora)
		{
			List<Disponibilidade> list = BD_Util.RetornaLista<Disponibilidade>("SELECT * FROM Disponibilidade");
			List<Disponibilidade> result = new List<Disponibilidade>();

			foreach(Professor prof in profs)
			{
				Disponibilidade d = list.Where
					(p => 
						(p.Horario.Hours == hora || p.Horario.Hours == (hora - 1))
						&&
						((p.Dia_Semana - 1 ) == (int)date.DayOfWeek))
					.FirstOrDefault();

				if (d != null)
					result.Add(d);
			}

			return result;
		}

		public static List<Professor> RetornaProfessoresDisponiveisDiaHorario(DateTime date, int hora, Unidade unidade, TipoCurso tipoCurso, int ano)
		{
			List<Professor> result = new List<Professor>();

			 string hora1 = (hora + ":00:00"), hora2 = ((hora+1) + ":00:00");

			string sql = "SELECT * FROM Disponibilidade " +
						 "WHERE Dia_Semana = " + (((int)date.DayOfWeek) + 1) +
						 " AND Horario BETWEEN '" + hora1 + "' AND '" + hora2 + "'" + 
						 " AND Unidade = " + UnidadeConversor.TipoCursoParaNumero(unidade) +
						 " AND TipoCurso = " + TipoCursoConversor.TipoCursoParaNumero(tipoCurso) +
						 " AND Ano = " + ano;

			 List<Disponibilidade> dispo = BD_Util.RetornaLista<Disponibilidade>(sql);

			 foreach (Disponibilidade d in dispo)
			 {
				 Professor prof = RetornaProfessor(d.Professor_Codigo);
				 if (prof != null)
					 result.Add(prof);
			 }
			 result = result.OrderBy(p => p.Nome).ToList();
			return result;
		}

		public static List<Prova> RetornaProvaPorDia(DateTime dia)
		{
			string sql = "SELECT * FROM Prova WHERE Data = @data";
			List<ParametrosBD> param = new List<ParametrosBD>();
			param.Add(new ParametrosBD("data", dia));

			List<Prova> list = BD_Util.RetornaLista<Prova>(sql, param);

			return list;

		}

		public static List<Prova> RetornaProvaSeProfAplicaDiaHorario(DateTime data, int hora, int codProf)
		{
			string hora1 = (hora + ":00:00"), hora2 = ((hora + 1) + ":00:00");

			string sql = "SELECT * FROM Prova WHERE" +
			" Data = '" + data.ToString("yyyy-MM-dd") + "'" +
			" AND Hora BETWEEN '" + hora1 +"' AND '" + hora2 + "' " +
			" AND (Professor_Codigo = " + codProf + " OR Professor_Suplente_Codigo = " + codProf + ")";

			List<Prova> result = BD_Util.RetornaLista<Prova>(sql);

			return result;
		}

		public static Disponibilidade RetornaDisponibilidade(Professor prof, DateTime date, int hora, int ano, bool ehEngenharia, Unidade uni, TipoCurso tipoCurso)
		{

			return RetornaDisponibilidade(prof.Codigo, date, hora, ano, uni, tipoCurso, ehEngenharia);
		}


		public static Disponibilidade RetornaDisponibilidade(int profCod, DateTime date, int hora, int ano, Unidade uni, TipoCurso tipoCurso = TipoCurso.NaoEspecificado, bool? ehEngenharia = null)
		{
			string hora1 = (hora + ":00:00"), hora2 = ((hora+1) + ":00:00");

			string sql = "Select Disponibilidade.* FROM Disponibilidade " +
						 "LEFT JOIN Curso ON Disponibilidade.Curso_Codigo = Curso.Codigo " +
						 "WHERE " +
						 " Professor_Codigo = " + profCod +
						 " and Disponibilidade.Ano = " + ano +
						 " and Disponibilidade.Unidade = " + UnidadeConversor.TipoCursoParaNumero(uni) +
						 " and Disponibilidade.Dia_Semana = " + ((int)date.DayOfWeek + 1) +
						 " and Disponibilidade.Horario BETWEEN '" + hora1 + "' AND '" + hora2 + "'";



			if (tipoCurso != TipoCurso.NaoEspecificado)
			{
				sql += " and Disponibilidade.TipoCurso = " + TipoCursoConversor.TipoCursoParaNumero(tipoCurso);
			}

			if (ehEngenharia != null)
			{
				if (ehEngenharia.GetValueOrDefault())
					sql += " and Curso.Nome like '%Engenharia%'";
				else
					sql += " and Curso.Nome not like '%Engenharia%'";
			}
			

			List<Disponibilidade> disponibilidade = BD_Util.RetornaLista<Disponibilidade>(sql);
			Disponibilidade d = disponibilidade.FirstOrDefault();

			return d;
		}

		public static List<Disponibilidade> RetornaDisponibilidade()
		{
			string sql = "select * from Disponibilidade";
			return BD_Util.RetornaLista<Disponibilidade>(sql);
		}

		public static List<DataHora> RetornaDataHora()
		{
			IDataReader dr = BD_Util.RetornaDataReader("select Data, Hora from Prova group by Data, Hora", null);
			List<DataHora> result = new List<DataHora>();

			while (dr.Read())
			{
				TimeSpan hora = new TimeSpan();
				DateTime data = new DateTime();

				DateTime.TryParse(dr["Data"].ToString(), out data);
				TimeSpan.TryParse(dr["Hora"].ToString(), out hora);

				if(data != new DateTime() && hora != new TimeSpan())
					result.Add(new DataHora(data, hora.Hours));
			}

			return result;
		}

		public static Disciplina RetornaDisciplinaPorCodigo(int codigo)
		{
			List<ParametrosBD> parametros1 = new List<ParametrosBD>();
			parametros1.Add(new ParametrosBD("codigo", codigo));
			Disciplina d = BD_Util.RetornaItem<Disciplina>("SELECT * FROM Disciplina WHERE codigo = @codigo", parametros1);

			return d;
		}

		public static Professor RetornaProfResponsavel(int codDisciplina)
		{
			ProfessorDisciplina pd = BD_Util.RetornaItem<ProfessorDisciplina>
				("SELECT * FROM Professor_has_Disciplina WHERE disciplina_codigo = " + codDisciplina);


			if (pd == null)
				return null;

			Professor prof = BD_Util.RetornaItem<Professor>("SELECT * FROM Professor WHERE codigo = " + pd.Professor_Codigo);

			return prof;
		}

		public static string RetornaNomeCurso(int codProva)
		{
			string nomeCurso = "";
			string sql = "SELECT Curso.Nome From Curso " +
						 "LEFT JOIN Turma ON Curso.Codigo = Turma.Curso_Codigo " +
						 "LEFT JOIN Prova ON Turma.Codigo = Prova.Turma_Codigo " +
						 "WHERE Prova.Codigo = " + codProva;

			IDataReader dr = BD_Util.RetornaDataReader(sql);

			if (dr.Read())
			{
				nomeCurso = dr["nome"].ToString();
			}

			return nomeCurso;
		}

		public static bool? RetornaSeCursoEhEngenharia(int codProva)
		{
			string nomeCurso = RetornaNomeCurso(codProva);

			if (nomeCurso == "")
				return null;

			return nomeCurso.Contains("Engenharia");
		}

		public static bool ProfLecionaDisciplina(int codProf, int codDisciplina)
		{
			ProfessorDisciplina pd = BD_Util.RetornaItem<ProfessorDisciplina>
				("SELECT * FROM Professor_has_Disciplina WHERE disciplina_codigo = " + codDisciplina +
				" AND professor_codigo = " + codProf);

			if (pd != null)
				return true;
			else
				return false;
		}

		public static List<Fila_Reserva> RetornaFilaReserva(int codCalendario)
		{
			string sql = "SELECT * FROM Fila_Reserva WHERE Calendario_Codigo = @CodCalendario";
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("CodCalendario", codCalendario));

			List<Fila_Reserva> list = BD_Util.RetornaLista<Fila_Reserva>(sql, parametros);

			return list;
		}

		public static void InsereFilaReserva(int codProva, int codCalendario, int codProfessor)
		{
			string sql = "INSERT INTO Fila_Reserva (Calendario_Codigo, Prova_Codigo, Professor_Codigo) values (@Calendario, @Prova, @Professor)";
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("Calendario", codCalendario));
			parametros.Add(new ParametrosBD("Prova", codProva));
			parametros.Add(new ParametrosBD("Professor", codProfessor));

			BD_Util.AcaoBD(sql, parametros);
		}

		public static string RetornaNomesProfsReservas(int codCalendario)
		{
			string result = "";
			List<Fila_Reserva> fila = RetornaFilaReserva(codCalendario);
			foreach (Fila_Reserva f in fila)
			{
				Professor p = RetornaProfessor(f.Professor_Codigo);
				if (p != null)
				{
					result += p.Nome + " | ";
				}
			}

			if(result.LastIndexOf(" | ") > 0)
				result = result.Remove(result.LastIndexOf(" | "));

			return result;
		}

		public static void ZerarEscalacao(int codCalendario)
		{
			string sql = "SELECT * FROM Calendario_has_Prova WHERE Calendario_Codigo = " + codCalendario;
			List<CalendarioProva> listCP = BD_Util.RetornaLista<CalendarioProva>(sql);

			foreach (CalendarioProva cp in listCP)
			{
				string sql2 = "UPDATE Prova set Professor_Codigo = NULL, Professor_Suplente_Codigo = NULL WHERE Codigo = " + cp.Prova_Codigo;
				BD_Util.AcaoBD(sql2);
			}

			//zera lista reservas
			BD_Util.AcaoBD("DELETE FROM Fila_Reserva WHERE Calendario_Codigo = " + codCalendario);

			//renova calendario_has_professor
			BD_Util.AcaoBD("DELETE FROM Calendario_has_Professor WHERE Calendario_Codigo = " + codCalendario);
			string sqlCP = "INSERT INTO Calendario_has_Professor (Calendario_Codigo, Professor_Codigo, Carga_Horaria_Disponivel) SELECT " + codCalendario + ", Codigo, Carga_Horaria FROM Professor;";
			BD_Util.AcaoBD(sqlCP);
		}

		public static Professor RetornaProfQueLecionaDisciplinaHorario(int codDisciplina, DateTime dia, int hora, int ano, Unidade uni)
		{
			string sqlPD = "SELECT * FROM Professor_has_Disciplina where Disciplina_Codigo = " + codDisciplina;
			List<ProfessorDisciplina> pdL = BD_Util.RetornaLista<ProfessorDisciplina>(sqlPD);

			foreach (ProfessorDisciplina pd in pdL)
			{
				Disponibilidade disp = RetornaDisponibilidade(pd.Professor_Codigo, dia, hora, ano, uni);
				if(disp != null)
				{
					Professor prof = BD_Util.RetornaItem<Professor>("SELECT * FROM Professor WHERE Codigo = " + pd.Professor_Codigo);
					return prof;
				}
			}

			return null;
		}


		public static bool ProfDaAulaPraAnoECurso(Professor prof, Prova prova)
		{
			List<ProfessorDisciplina> pd = BD_Util.RetornaLista<ProfessorDisciplina>(
				"SELECT * FROM Professor_has_Disciplina WHERE professor_codigo = " + prof.Codigo);

			return true;
		}

		public static void InsereCalendarioProva(List<CalendarioProva> cp)
		{
			foreach (CalendarioProva c in cp)
			{
				List<ParametrosBD> parametros = new List<ParametrosBD>();
				parametros.Add(new ParametrosBD("codCalendario", c.Calendario_Codigo));
				parametros.Add(new ParametrosBD("codProva", c.Prova_Codigo));
				string sql = "INSERT INTO Calendario_has_Prova VALUES (@codCalendario, @codProva)";
				BD_Util.AcaoBD(sql, parametros);
			}
		}

		public static void InsereCalendarioProvaIndividual(int codCalendario, int codProva)
		{
				List<ParametrosBD> parametros = new List<ParametrosBD>();
				parametros.Add(new ParametrosBD("codCalendario", codCalendario));
				parametros.Add(new ParametrosBD("codProva", codProva));
				string sql = "INSERT INTO Calendario_has_Prova (Calendario_Codigo, Prova_Codigo) VALUES (@codCalendario, @codProva)";
				BD_Util.AcaoBD(sql, parametros);
			
		}


		public static void InsereCalendario(Calendario cal)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("nome", cal.Nome));
			parametros.Add(new ParametrosBD("semestre", cal.Semestre));
			parametros.Add(new ParametrosBD("ano", cal.Ano));
			parametros.Add(new ParametrosBD("faculdade", cal.CodigoFaculdade));
			string sql = "INSERT INTO Calendario(Nome, Semestre, Ano, Faculdade_Codigo) VALUES (@nome, @semestre, @ano, @faculdade)";
			BD_Util.AcaoBD(sql, parametros);

		}

		

		public static void InsereProvas(List<Prova> provas)
		{
			foreach (Prova p in provas)
			{

				List<ParametrosBD> parametros = new List<ParametrosBD>();
				parametros.Add(new ParametrosBD("unidade", UnidadeConversor.TipoCursoParaNumero(p.Unidade)));
				parametros.Add(new ParametrosBD("divisao", DivisaoConversor.DivisaoParaNumero(p.Divisao)));
				parametros.Add(new ParametrosBD("tipoProva", TipoProvaConversor.TipoCursoParaNumero(p.Tipo)));
				parametros.Add(new ParametrosBD("regime", TipoCursoConversor.TipoCursoParaNumero(p.Regime)));
				parametros.Add(new ParametrosBD("duracao", p.Duracao));
				parametros.Add(new ParametrosBD("data", p.Data));
				parametros.Add(new ParametrosBD("hora", p.Hora));
				parametros.Add(new ParametrosBD("codSala", p.Sala_Codigo));
				parametros.Add(new ParametrosBD("codDisciplina", p.Disciplina_Codigo));
				parametros.Add(new ParametrosBD("codTurma", p.Turma_Codigo));
				string sql = "INSERT INTO Prova (Unidade, Divisao, Tipo, Regime, Duracao, Data, Hora, Sala_Codigo,  Disciplina_Codigo, Turma_Codigo)" +
									"VALUES (@unidade, @divisao, @tipoProva, @regime, @duracao, @data, @hora, @codSala, @codDisciplina, @codTurma)";
				parametros.Add(new ParametrosBD("especial", p.Especial));
				
				BD_Util.AcaoBD(sql, parametros);
			}
		}

		public static void InsereProva(Prova p)
		{

			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("unidade", UnidadeConversor.TipoCursoParaNumero(p.Unidade)));
			parametros.Add(new ParametrosBD("divisao", DivisaoConversor.DivisaoParaNumero(p.Divisao)));
			parametros.Add(new ParametrosBD("tipoProva", TipoProvaConversor.TipoCursoParaNumero(p.Tipo)));
			parametros.Add(new ParametrosBD("regime", TipoCursoConversor.TipoCursoParaNumero(p.Regime)));
			parametros.Add(new ParametrosBD("duracao", p.Duracao));
			parametros.Add(new ParametrosBD("data", p.Data));
			parametros.Add(new ParametrosBD("hora", p.Hora));
			parametros.Add(new ParametrosBD("codSala", p.Sala_Codigo));
			parametros.Add(new ParametrosBD("codDisciplina", p.Disciplina_Codigo));
			parametros.Add(new ParametrosBD("codTurma", p.Turma_Codigo));
			string sql = "INSERT INTO Prova (Unidade, Divisao, Tipo, Regime, Duracao, Data, Hora, Sala_Codigo, Disciplina_Codigo, Turma_Codigo)" +
								"VALUES (@unidade, @divisao, @tipoProva, @regime, @duracao, @data, @hora, @codSala, @codDisciplina, @codTurma)";
			parametros.Add(new ParametrosBD("especial", p.Especial));
				BD_Util.AcaoBD(sql, parametros);
		}

		public static int RetornaUltimaProva(){
			int cod = 0;
			string sql = "SELECT MAX(Codigo) AS Codigo FROM Prova";

			IDataReader dr = BD_Util.RetornaDataReader(sql);

			if (dr.Read())
			{
				cod = (int)dr["codigo"];
			}

			return cod;
		}


		public static void AlteraProfAplicador(int codprof, int codprova)
		{
			List<ParametrosBD> parametros = new List<ParametrosBD>();
			parametros.Add(new ParametrosBD("codProf", codprof));
			parametros.Add(new ParametrosBD("codProva", codprova));
			string sql = "UPDATE Prova SET Professor_Codigo = @codProf where Codigo = @codProva;";
			BD_Util.AcaoBD(sql, parametros);
		}

		public static CalendarioProfessor RetornaCalendarioProfessor (int codProf, int codCalendario)
		{
			string sql = "SELECT * FROM Calendario_has_Professor WHERE Calendario_Codigo = " + codCalendario + " AND Professor_Codigo = " + codProf;
			CalendarioProfessor cp = BD_Util.RetornaItem<CalendarioProfessor>(sql);

			return cp;
		}
		public static int RetornaProvaCount()
		{
			string sql = "SELECT COUNT(*) qtdProva FROM Prova";
			int count = 0;

			IDataReader dr = BD_Util.RetornaDataReader(sql);

			if (dr.Read())
			{
				int.TryParse(dr["qtdProva"].ToString(), out count);
			}

			return count;
		}

		public static List<CursoView> RetornaCursoView()
		{
			List<CursoView> list = new List<CursoView>();

			List<Curso> cursos = RetornaTodosCursosCrudCurso();

			foreach (Curso c in cursos)
			{
				CursoView cv = new CursoView();
				cv.Curso = c;
				cv.Codigo = c.Codigo;
				Professor p = RetornaProfessor(c.CodigoProfessor);
				cv.Coordenador = p != null ? p.Nome : " - ";
				Faculdade f = RetornaFaculdade(c.CodigoFaculdade);
				cv.Faculdade = f != null ? f.Nome : " - ";
				cv.Nome = c.Nome;
				if (c.TipoCurso == TipoCurso.Anual)
					cv.Tipo = "Anual";
				else if (c.TipoCurso == TipoCurso.Semestral)
					cv.Tipo = "Semestral";
				else
					cv.Tipo = "Não específicado";

				list.Add(cv);
			}

			return list;
		}

		public static List<TurmaView> RetornaTurmaView()
		{
			List<TurmaView> list = new List<TurmaView>();

			List<Turma> turmas = RetornaTodasTurmas();

			foreach (Turma t in turmas)
			{
				TurmaView cv = new TurmaView();
				cv.Turma = t;
				cv.Codigo = t.Codigo;               
				cv.Nome = t.Nome;
				Curso c = RetornaCurso(t.CodigoCurso);
				if (c != null)
					cv.Curso = c.Nome;
				else
					cv.Curso = " - ";

				if (t.TipoCurso == TipoCurso.Anual)
					cv.TipoCurso = "Anual";
				else if (t.TipoCurso == TipoCurso.Semestral)
					cv.TipoCurso = "Semestral";
				else
					cv.TipoCurso = "Não específicado";

				list.Add(cv);
			}

			return list.OrderBy(p => p.TipoCurso).ThenBy(p => p.Nome).ToList();
		}
		public static List<DisciplinaView> RetornaDisciplinaView()
		{
			List<DisciplinaView> list = new List<DisciplinaView>();

			List<Disciplina> disciplinas = RetornaTodasDisciplinas();

			foreach (Disciplina d in disciplinas)
			{
				DisciplinaView dv = new DisciplinaView();

				dv.Codigo = d.Codigo;
				dv.Disciplina = d;
				dv.Nome = d.Nome;

				if (d.TipoCurso == TipoCurso.Anual)
					dv.Regime = "Anual";
				else if (d.TipoCurso == TipoCurso.Semestral)
					dv.Regime = "Semestral";
				else
					dv.Regime = "Não específicado";

				list.Add(dv);
			}

			return list.OrderBy(p => p.Regime).ThenBy(p => p.Nome).ToList();
		}

		public static Curso RetornaCurso(int cod)
		{
			string sql = "select * from Curso where Codigo = " + cod;

			return BD_Util.RetornaItem<Curso>(sql);
		}

		public static int RetornaCodCurso(string nome, int tipoCurso)
		{
			int codcur = 0;
			string sql = "select * from Curso where Nome like '" + nome + "' and TipoCurso = " + tipoCurso + ";";

			Curso cur = new Curso();
			cur = BD_Util.RetornaItem<Curso>(sql);
			codcur = cur.Codigo;

			return codcur;
		}

		public static Professor RetornaProfessor(int codProf)
		{
			string sql = "SELECT * FROM Professor WHERE Codigo = " + codProf;

			return BD_Util.RetornaItem<Professor>(sql);
		}
		public static Faculdade RetornaFaculdade(int codFaculdade)
		{
			string sql = "SELECT * FROM Faculdade WHERE Codigo = " + codFaculdade;

			return BD_Util.RetornaItem<Faculdade>(sql);
		}

		public static bool ValidaCPFExiste(string cpf) 
		{
			string sql = "SELECT * FROM Professor WHERE CPF = " + cpf;
			IDataReader dr = BD_Util.RetornaDataReader(sql);

			if (dr.Read())
				return true;
			else
				return false;
		}

		public static bool ValidarSeJaExisteSala(string sala)
		{
			string sql = "SELECT * FROM Sala WHERE Nome = '" + sala + "'";

			 IDataReader dr = BD_Util.RetornaDataReader(sql);

			if (dr.Read())
				return true;
			else
				return false;
		}

		public static List<CapaDeProva> RetornaCapaDeProva(int cod, String Curso, String DataDeProva, String Hora, String Unidade, String Faculdade, String Periodo)
		{
			if (DataDeProva.Equals("%"))
			{
				DataDeProva = "'%'";
			}
			else
			{
				DataDeProva = "DATE_FORMAT(STR_TO_DATE('" + DataDeProva + "', '%d-%m-%Y'), '%Y-%m-%d')";
			}
			String Query = "SELECT CapaProva.* FROM(SELECT CASE WHEN Prova.Unidade LIKE '1' THEN 'Mooca' WHEN Prova.Unidade LIKE '2' THEN 'Butantã' END AS 'Unidade', CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', CASE WHEN Prova.Divisao = 1 THEN 'Par' WHEN Prova.Divisao = 2 THEN 'Ímpar' WHEN Prova.Divisao = 3 THEN 'Única' END AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Sala.Nome AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS CapaProva ";
			Query = Query + " WHERE Curso LIKE '" + Curso + "' AND DatadaProva LIKE " + DataDeProva + " AND Horario LIKE '" + Hora + "' AND Unidade LIKE '" + Unidade + "' AND Faculdade LIKE '" + Faculdade + "' ;";  
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>(Query);
			return list;

		}
		public static List<CapaDeProva> RetornaTodosCursos()
		{
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>("  SELECT 'Mooca' AS Unidade, CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS'Disciplina', Prova.Divisao AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Sala.Nome AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN(SELECT * FROM calendario_usjt.Professor_has_Disciplina WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo GROUP BY Curso.Nome");
			return list;
		}

		public static List<CapaDeProva> RetornaTodasDatasProvas()
		{
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>("SELECT 'Mooca' AS Unidade, CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS'Disciplina', Prova.Divisao AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Sala.Nome AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN(SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo GROUP BY  Prova.data;");
			return list;
		}

		public static List<CapaDeProva> RetornaTodosPeriodos()
		{
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>(" SELECT 'Mooca' AS Unidade, CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' ELSE 'Outro' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS'Disciplina', Prova.Divisao AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Sala.Nome AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN(SELECT * FROM calendario_usjt.Professor_has_Disciplina WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo GROUP BY Periodo");
			return list;
		}


		public static List<CapaDeProva> RetornaTodasHorasProvas()
		{
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>("  SELECT 'Mooca' AS Unidade, CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS'Disciplina', Prova.Divisao AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Sala.Nome AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN(SELECT * FROM calendario_usjt.Professor_has_Disciplina WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo GROUP BY Prova.Hora");
			return list;
		}

		public static List<CapaDeProva> RetornaTodasFaculdade()
		{
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>(" SELECT 'Mooca' AS Unidade, CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' ELSE 'Outro' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS'Disciplina', Prova.Divisao AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Sala.Nome AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN(SELECT * FROM calendario_usjt.Professor_has_Disciplina WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo GROUP BY Faculdade.Nome");
			return list;
		}

		public static List<CapaDeProva> RetornaTodasUnidades()
		{
			List<CapaDeProva> list = BD_Util.RetornaLista<CapaDeProva>("SELECT 'Mooca' AS Unidade, CURDATE() AS DATA, 'Mooca' AS LocalDeEntrega, 'Botter' AS Solicitante, '' AS Ramal, Curso.Nome AS 'Curso', Faculdade.Nome AS 'Faculdade', CASE WHEN Prova.Hora LIKE '%07:00:00%' THEN 'Manhã 1' WHEN Prova.Hora LIKE '%09:15:00%' THEN 'Manhã 2' WHEN Prova.Hora LIKE '%19:15:00%' THEN 'Noturno 1' WHEN Prova.Hora LIKE '%21:00:00%' THEN 'Noturno 2' END AS 'Periodo', Turma.Nome AS 'Turma', Disciplina.Nome AS'Disciplina', Prova.Divisao AS 'Divisao', Prova.data AS 'DatadaProva', Prova.Hora AS 'Horario', Prova.Duracao AS 'Duracao', Sala.Nome AS 'Sala', Professor.Nome AS 'Aplicador', ProfResp.Nome AS 'Responsavel' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN(SELECT * FROM calendario_usjt.Professor_has_Disciplina WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo GROUP BY LocalDeEntrega");
			return list;
		}

		public static List<Calendario> RetornaTodosCalendarios()
		{
			List<Calendario> list = BD_Util.RetornaLista<Calendario>("SELECT * FROM Calendario;");
			return list;
		}

		internal static void TiraAplicador(Prova prova)
		{
			string sql = "UPDATE Prova set Professor_Codigo = NULL, Professor_Suplente_Codigo = NULL WHERE Codigo = " + prova.Codigo;
			BD_Util.AcaoBD(sql);
		}

		internal static void TiraSuplente(Prova prova)
		{
			string sql = "UPDATE Prova set Professor_Suplente_Codigo = NULL WHERE Codigo = " + prova.Codigo;
			BD_Util.AcaoBD(sql);
		}

		internal static void AlteraAplicador(int provaCod, int profAplicadorCod)
		{
			string sql = "UPDATE Prova SET Professor_Codigo = " + profAplicadorCod + " WHERE Codigo = " + provaCod;
			BD_Util.AcaoBD(sql);
		}

		internal static List<Prova> RetornaProvasMesmaSala(DateTime data, int hora, int codProva, int codSala)
		{
			string hora1 = (hora + ":00:00"), hora2 = ((hora + 1) + ":00:00");
			string sql = "SELECT * FROM Prova WHERE Data = @Data AND " +
				"Hora BETWEEN '@Hora1' AND 'Hora2' AND Sala_Codigo = @CodSala AND Codigo <> @CodProva";
			List<ParametrosBD> param = new List<ParametrosBD>();
			param.Add(new ParametrosBD("Data", data));
			param.Add(new ParametrosBD("Hora1", hora1));
			param.Add(new ParametrosBD("Hora2", hora2));
			param.Add(new ParametrosBD("CodSala", codSala));
			param.Add(new ParametrosBD("CodProva", codProva));

			List<Prova> result = BD_Util.RetornaLista<Prova>(sql, param);
			return result;
		}

		internal static void AlteraSuplente(int provaCod, int profSupCod)
		{
			string sql = "UPDATE Prova SET Professor_Suplente_Codigo = " + profSupCod + " WHERE Codigo = " + provaCod;
			BD_Util.AcaoBD(sql);
		}
		internal static void TiraProfSuplente(int provaCod)
		{
			string sql = "UPDATE Prova SET Professor_Suplente_Codigo = NULL WHERE Codigo = " + provaCod;
			BD_Util.AcaoBD(sql);
		}

		internal static Prova RetornaProva(int codigo)
		{
			string sql = "SELECT * FROM Prova where Codigo = " + codigo;

			return BD_Util.RetornaItem<Prova>(sql);
		}

		public static List<Relatorio> Relatorio_RetornaTodosCurso()
		{
			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>("SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario', CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mocca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE_FORMAT(STR_TO_DATE(Prova.Data, '%Y-%m-%d'), '%d-%m-%Y') AS 'Data', Prova.Hora AS 'Hora' , Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS TABELA GROUP BY Curso;");
			return list;
		}


		public static List<Relatorio> Relatorio_RetornaTodosData()
		{
			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>("SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario', CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mocca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE_FORMAT(STR_TO_DATE(Prova.Data, '%Y-%m-%d'), '%d-%m-%Y') AS 'Data', Prova.Hora AS 'Hora' , Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS TABELA GROUP BY DATA;");
			return list;
		}



		public static List<Relatorio> Relatorio_RetornaTodosHora()
		{
			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>("SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario', CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mocca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE_FORMAT(STR_TO_DATE(Prova.Data, '%Y-%m-%d'), '%d-%m-%Y') AS 'Data', Prova.Hora AS 'Hora' , Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS TABELA GROUP BY Hora;");
			return list;
		}



		public static List<Relatorio> Relatorio_RetornaTodosTurno()
		{
			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>("SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario',  CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mocca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE_FORMAT(STR_TO_DATE(Prova.Data, '%Y-%m-%d'), '%d-%m-%Y') AS 'Data', Prova.Hora AS 'Hora' , Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS TABELA GROUP BY Turno;");
			return list;
		}


		public static List<Relatorio> Relatorio_RetornaTodosDisciplina()
		{
			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>("SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario', CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mocca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE_FORMAT(STR_TO_DATE(Prova.Data, '%Y-%m-%d'), '%d-%m-%Y') AS 'Data', Prova.Hora AS 'Hora' , Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS TABELA GROUP BY Disciplina;");
			return list;
		}



		public static List<Relatorio> Relatorio_RetornaTodosRegime()
		{
			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>("SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario', CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mocca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE_FORMAT(STR_TO_DATE(Prova.Data, '%Y-%m-%d'), '%d-%m-%Y') AS 'Data', Prova.Hora AS 'Hora' , Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS TABELA GROUP BY Regime;");
			return list;
		}


		public static List<Relatorio> Relatorio_RetornaTodosCalendario()
		{
			String Query = "SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario',CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mocca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE_FORMAT(STR_TO_DATE(Prova.Data, '%Y-%m-%d'), '%d-%m-%Y') AS 'Data', Prova.Hora AS 'Hora', Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo INNER JOIN calendario_usjt.Faculdade AS Faculdade ON Faculdade.Codigo = Curso.Faculdade_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo INNER JOIN (SELECT * FROM `calendario_usjt`.`Professor_has_Disciplina` WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo INNER JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo) AS TABELA GROUP BY Calendario;";
			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>(Query);
			return list;
		}
		

		public static List<Relatorio> Relatorio_RetornaTodosRelatorios(String Curso, String Data, String Turno, String Hora, String Regime, String Calendario)
		{
			String Query = "SELECT TABELA.* FROM(SELECT Calendario.Nome AS 'Calendario', CASE WHEN Prova.Regime LIKE '%1%' THEN 'Anual' WHEN Prova.Regime LIKE '%2%' THEN 'Semestral' END AS 'Regime', CASE WHEN Prova.Unidade LIKE '%1%' THEN 'Mooca' WHEN Prova.Unidade LIKE '%2%' THEN 'Butantã' END AS 'Campus', CASE WHEN Prova.Hora BETWEEN '07:00:00' AND '11:59:00' THEN 'Manhã' WHEN Prova.Hora BETWEEN '12:00:00' AND '18:59:00' THEN 'Tarde' WHEN Prova.Hora BETWEEN '19:00:01' AND '23:00:00' THEN 'Noite' END AS 'Turno', Curso.Nome AS 'Curso', (CASE WEEKDAY(Prova.Data) WHEN 0 THEN 'Segunda-feira' WHEN 1 THEN 'Terça-feira' WHEN 2 THEN 'Quarta-feira' WHEN 3 THEN 'Quinta-feira' WHEN 4 THEN 'Sexta-feira' WHEN 5 THEN 'Sábado' WHEN 6 THEN 'Domingo' END) AS 'DiaDaSemana', DATE(Prova.Data) AS 'Data', Prova.Hora AS 'Hora', Turma.Nome AS 'Turma', Disciplina.Nome AS 'Disciplina', ProfResp.Nome AS 'ProfessorResponsavel', Professor.Nome AS 'ProfessorAplicador', CASE WHEN Prova.Divisao LIKE '%1%' THEN 'Par' WHEN Prova.Divisao LIKE '%2%' THEN 'Impar' WHEN Prova.Divisao LIKE '%3%' THEN 'Unica' END AS 'Divisao', Sala.Nome AS 'Sala' FROM Prova AS Prova INNER JOIN calendario_usjt.Sala AS Sala ON Prova.Sala_Codigo = Sala.Codigo INNER JOIN calendario_usjt.Turma AS Turma ON Turma.Codigo = Prova.Turma_Codigo INNER JOIN calendario_usjt.Disciplina AS Disciplina ON Disciplina.Codigo = Prova.Disciplina_Codigo LEFT JOIN calendario_usjt.Professor AS Professor ON Prova.Professor_Codigo = Professor.Codigo LEFT JOIN (SELECT * FROM Professor_has_Disciplina WHERE Responsavel = 1 GROUP BY `Disciplina_Codigo`) AS ProfDis ON Prova.Disciplina_Codigo = ProfDis.Disciplina_Codigo LEFT JOIN Professor AS ProfResp ON ProfResp.Codigo = ProfDis.Professor_Codigo INNER JOIN Calendario_has_Prova AS CalProv ON CalProv.Prova_Codigo = Prova.Codigo INNER JOIN Calendario AS Calendario ON Calendario.Codigo = CalProv.Calendario_Codigo LEFT JOIN Professor AS ProfSup ON Prova.Professor_Suplente_Codigo = Professor.Codigo INNER JOIN calendario_usjt.Curso AS Curso ON Curso.Codigo = Turma.Curso_Codigo ORDER BY Prova.Data, Prova.Hora) AS TABELA WHERE TABELA.Curso LIKE '" + Curso + "' AND TABELA.Data LIKE '" + Data + "' AND TABELA.Turno LIKE '" + Turno + "' AND TABELA.Hora LIKE '" + Hora + "' AND TABELA.Regime LIKE '" + Regime + "'AND TABELA.Calendario  LIKE '" + Calendario + "'";

			List<Relatorio> list = BD_Util.RetornaLista<Relatorio>(Query);
			return list;
		}

		public static int RetornaCodTurma(string nomeTurma, int tipoCurso)
		{
			int cod = 0;
			Turma tur = new Turma();
			string sql = "SELECT * FROM Turma where Nome like'" + nomeTurma + "' AND TipoCurso = " + tipoCurso + ";";
			tur = BD_Util.RetornaItem<Turma>(sql);
			if (tur != null)
			{
				cod = tur.Codigo;
			}
			else
			{
				cod = -1;
			}
			return cod;
		}


		public static int RetornaCodDisciplina(string nomeDisciplina, int tipoDisciplina, int codCurso)
		{
			int cod = 0;
			Disciplina disc = new Disciplina();
			string sql = "SELECT * FROM Disciplina where Nome like'" + nomeDisciplina + "' AND TipoDisciplina = " + tipoDisciplina + " AND Curso_Codigo = " + codCurso + ";";
			disc = BD_Util.RetornaItem<Disciplina>(sql);
			if (disc != null)
			{
				cod = disc.Codigo;
			}
			else
			{
				cod = -1;
			}

			return cod;
		}

		public static int RetornaCodSala(string nomeSala)
		{
			int cod = 0;
			Sala sala = new Sala();
			string sql = "SELECT * FROM Sala where Nome like'" + nomeSala + "';";
			sala = BD_Util.RetornaItem<Sala>(sql);
			if (sala != null)
			{
				cod = sala.Codigo;
			}
			else
			{
				cod = -1;
			}

			return cod;
		}

		public static void InsereCalHProf(int codCalendario)
		{
			string sql = "INSERT INTO Calendario_has_Professor (Calendario_Codigo, Carga_Horaria_Disponivel, Professor_Codigo ) select " + codCalendario + ", Carga_Horaria, Codigo from Professor;";
			BD_Util.AcaoBD(sql);
		}


		public static int RetornaUltimoCalendario()
		{
			int cod = 0;
			string sql = "SELECT MAX(Codigo) AS Codigo FROM Calendario";

			IDataReader dr = BD_Util.RetornaDataReader(sql);

			if (dr.Read())
			{
				cod = (int)dr["codigo"];
			}

			return cod;
		}


		internal static List<ProfessorDisciplina> RetornaProfessorDisciplina()
		{
			string sql = "select * from Professor_has_Disciplina";
			return BD_Util.RetornaLista<ProfessorDisciplina>(sql);
		}

		internal static List<CalendarioProfessor> RetornaCalendarioProfessor()
		{
			string sql = "select * from Calendario_has_Professor";
			return BD_Util.RetornaLista<CalendarioProfessor>(sql);
		}

		internal static List<CalendarioProva> RetornaCalendarioProva()
		{
			string sql = "select * from Calendario_has_Prova";
			return BD_Util.RetornaLista<CalendarioProva>(sql);
		}

		internal static List<Turma> RetornaTurmas()
		{
			string sql = "select * from Turma";
			return BD_Util.RetornaLista<Turma>(sql);
		}

		internal static List<Curso> RetornaCurso()
		{
			string sql = "select * from Curso";
			return BD_Util.RetornaLista<Curso>(sql);
		}

		internal static void IncluirFilaReserva(List<Fila_Reserva> fila_Reserva)
		{
			foreach (Fila_Reserva fl in fila_Reserva)
			{
				List<ParametrosBD> parametros = new List<ParametrosBD>();
				parametros.Add(new ParametrosBD("Calendario_Codigo", fl.Calendario_Codigo));
				parametros.Add(new ParametrosBD("Data_Fila", fl.Data_Fila));
				parametros.Add(new ParametrosBD("Engenharia", fl.Engenharia));
				parametros.Add(new ParametrosBD("Hora_Fila", fl.Hora_Fila));
				parametros.Add(new ParametrosBD("Professor_Codigo", fl.Professor_Codigo));
				parametros.Add(new ParametrosBD("Unidade", fl.Unidade));

				string sql = "INSERT INTO Fila_Reserva (Calendario_Codigo, Professor_Codigo, Data_Fila, Hora_Fila, Unidade, Engenharia) "+
					"VALUES (@Calendario_Codigo, @Professor_Codigo, @Data_Fila, @Hora_Fila, @Unidade, @Engenharia)";

				BD_Util.AcaoBD(sql, parametros);
			}
		}

		internal static void ExcluirCalendario(int codCalendario)
		{
			string sql1 = "DELETE Prova.* FROM Prova INNER JOIN Calendario_has_Prova ON Prova.Codigo = Calendario_has_Prova.Prova_Codigo " +
						  "INNER JOIN Calendario ON Calendario_has_Prova.Calendario_Codigo = Calendario.Codigo where Calendario.Codigo = " + codCalendario;
			string sql2 = "DELETE FROM Calendario WHERE Codigo = " + codCalendario;

			BD_Util.AcaoBD(sql1);
			BD_Util.AcaoBD(sql2);
		}

		internal static bool ExisteCalendario()
		{
			string sql = "SELECT * FROM Calendario limit 1";

			List<Calendario> x = BD_Util.RetornaLista<Calendario>(sql);

			return x.Count > 0;

		}
	}
}
