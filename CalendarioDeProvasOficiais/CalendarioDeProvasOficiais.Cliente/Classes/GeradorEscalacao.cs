using CalendarioDeProvasOficiais.Comum.Enum;
using CalendarioDeProvasOficiais.Comum.Objetos;
//using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioDeProvasOficiais.Cliente.Classes
{
    public class GeradorEscalacao
    {
        private int _cargaHorariaDiminuir = 2;

        public List<CalendarioProva> GerarEscalacao(Calendario c)
        {            
            List<DataProva> listDataProva = BD.RetornaDatasPorCalendario(c.Codigo);
            List<Professor> listProfessores = BD.RetornaTodosProfessores(); //PEGA TODOS OS PROFESSORES
            List<Professor> profsNaoMesarios = listProfessores.Where(p => !p.Mesario && p.Carga_Horaria >= 2).ToList(); //pegar pela carga horaria disponivel
            List<Disponibilidade> disponibilidades = BD.RetornaDisponibilidade(); //retorna toda disponibilidade
            List<ProfessorDisciplina> prof_disciplina = BD.RetornaProfessorDisciplina();
            List<Turma> todasTurmas = BD.RetornaTurmas();
            List<Curso> todosCursos = BD.RetornaCurso();
            List<CalendarioProfessor> calendario_prof = BD.RetornaCalendarioProfessor();

            int cProva = 0;
            int segTotal = 0;
            DateTime dtInicial, dtFim;

            foreach (DataProva dp in listDataProva)
            {

                if (dp.Data.Day == 1 && dp.Data.Month == 11)
                    continue;

                foreach (HoraProva h in dp.Horas)
                {
                    List<Professor> profReservaMoocaEng = new List<Professor>();
                    List<Professor> profReservaMooca = new List<Professor>();
                    List<Professor> profReservaButataEng = new List<Professor>();
                    List<Professor> profReservaButata = new List<Professor>();

                    //pega todos os profs na disponibilidade, ignorando o Campus
                    List<Professor> listProfessorN = RetornaListaProfsDisponiveis(disponibilidades, h, dp, profsNaoMesarios);

                    ZeraPontos(listProfessorN);
                    List<Prova> provasEscaladasAtualizar = new List<Prova>();
                    List<Professor> professorAtualizar = new List<Professor>();

                    foreach (Prova prova in h.Provas)
                    {
                        dtInicial = DateTime.Now;
                        cProva++;

                        //se ja tem professor escalado, pula pra não escalar dnv
                        if (prova.Professor_Codigo > 0)
                            continue;

                        //Se for prova especial, pega professor que da aula praquela disciplina e escala ele, saindo do loop
                        if (prova.Especial)
                        {

                            Professor profL = RetornaProfQueLecionaDisciplinaHorario(prof_disciplina, prova.Disciplina_Codigo, prova.Data, prova.Hora.Hours, c.Ano, prova.Unidade, disponibilidades, listProfessorN, prova.Regime);
                            if (profL != null)
                            {
                                if (profL != null)
                                {
                                    prova.Professor_Codigo = profL.Codigo;

                                    //Escala prova e atualiza carga_horaria_disponível a cada horario

                                    //Pega o Professor pra diminuir a carga horaria disponível do professor que vai aplicar a prova
                                    professorAtualizar.Add(profL);

                                    //Pega prova pra atualizar
                                    provasEscaladasAtualizar.Add(new Prova().Preenche(prova));

                                    //Remove professor da lista, pois o mesmo não pode mais ser escalado nesse horário
                                    listProfessorN.Remove(profL);

                                    Console.WriteLine(prova.Codigo + " - " + DateTime.Now + " - Escalou DP");
                                }
                                else
                                {
                                    Console.WriteLine(prova.Codigo + " - " + DateTime.Now + " - Não Escalou DP");
                                }
                            }


                            continue;
                        }

                        //valida se alguma prova daquele horario é na mesma sala, se for já coloca o aplicador igual
                        List<Prova> provaMesmaSala = h.Provas.Where(p => p.Sala_Codigo == prova.Sala_Codigo).ToList();
                        provaMesmaSala.Remove(prova);
                        bool saiLoop = false;

                        foreach (Prova prv in provaMesmaSala)
                        {
                            if (prv.Professor_Codigo > 0)
                            {
                                prova.Professor_Codigo = prv.Professor_Codigo;
                                professorAtualizar.Add(profsNaoMesarios.Where(p => p.Codigo == prv.Professor_Codigo).FirstOrDefault());
                                provasEscaladasAtualizar.Add(prova);

                                Console.WriteLine(prova.Codigo + " - " + DateTime.Now + " - Escalou prova mesma sala");

                                saiLoop = true;
                            }

                        }

                        if (saiLoop)
                            continue;


                        List<Professor> profElegivel = new List<Professor>();
                        bool? ehEngenharia = RetornaSeCursoEhEngenharia(todasTurmas, todosCursos, prova.Turma_Codigo);

                        if (ehEngenharia == null)
                        {
                            //não tem como escalar professor se não sabe se naquele horario ele da aula pra engenharia ou outros.
                            continue;
                        }

                        List<Prova> todasProvasDoDia = dp.RetornaProvasPorDia();
                        //pega a lista de professores disponiveis naquele dia/horario semestral ou anual de acordo com a prova
                        foreach (Professor prof in listProfessorN)
                        {
                            //valida se professor é responsavel
                            if (ProfessorEResponsavel(prof_disciplina, prof, prova))
                            {
                                continue;
                            }
                                

                            //valida se professor tem carga horaria disponível pra aplicar a prova
                            CalendarioProfessor cp = calendario_prof.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();
                            if (cp != null && cp.Carga_Horaria_Disponivel < 2)
                            {
                                continue;
                            }
                            prof.Carga_Horaria_Disponivel = cp.Carga_Horaria_Disponivel;

                            if (ProfessorPodeAplicarAProva(prof, todasTurmas, todosCursos, ehEngenharia.GetValueOrDefault(), prova.Turma_Codigo, dp.Data, h.Hora.Hours, c.Ano, prova.Unidade, disponibilidades, prova.Regime))
                            {
                                
                                prof.Pontos += RetornaPontosProfLecionaDisciplina(prof, prova, prof_disciplina);
                                prof.Pontos += RetornaPontosProfAplicouProvaPrimeiroHorario(prof, todasProvasDoDia);
                                List<Prova> provasDiaAnterior = RetornaProvaDiaAnterior(listDataProva, dp);
                                prof.Pontos += RetornaPontosProfAplicouProvaDiaAnterior(prof, provasDiaAnterior, dp.Data);
                                prof.Pontos += RetornaPontosProfResponsavelProvaDiaAnterior(prof, provasDiaAnterior, dp.Data, prof_disciplina);
                                profElegivel.Add(prof);
                            }

                        }

                        if (profElegivel.Count > 0)
                        {
                            profElegivel = profElegivel.OrderBy(p => p.Pontos).ThenByDescending(p => p.Carga_Horaria_Disponivel).ToList();
                            Professor profSelecionado = profElegivel.FirstOrDefault();

                            if (profSelecionado != null)
                            {
                                prova.Professor_Codigo = profSelecionado.Codigo;

                                //Escala prova e atualiza carga_horaria_disponível a cada horario
                                //Pega o Professor pra diminuir a carga horaria disponível do professor que vai aplicar a prova
                                professorAtualizar.Add(profSelecionado);
                                //Pega prova pra atualizar
                                provasEscaladasAtualizar.Add(new Prova().Preenche(prova));

                                //Remove professor da lista, pois o mesmo não pode mais ser escalado nesse horário
                                listProfessorN.Remove(profSelecionado);
                                profElegivel.Remove(profSelecionado);


                                if (prova.Unidade == Unidade.Mooca && ehEngenharia == true)
                                    profReservaMoocaEng.AddRange(profElegivel);
                                else if (prova.Unidade == Unidade.Mooca)
                                    profReservaMooca.AddRange(profElegivel);
                                else if (prova.Unidade == Unidade.Butata && ehEngenharia == true)
                                    profReservaButataEng.AddRange(profElegivel);
                                else if (prova.Unidade == Unidade.Butata)
                                    profReservaButata.AddRange(profElegivel);


                                Console.WriteLine(prova.Codigo + " - " + DateTime.Now + " - Escalou");
                            }
                            else
                            {
                                Console.WriteLine(prova.Codigo + " - " + DateTime.Now + " - Não Escalou");
                            }
                        }
                        else
                        {
                            Console.WriteLine(prova.Codigo + " - " + DateTime.Now + " - Não Escalou");
                        }

                        dtFim = DateTime.Now;
                        segTotal += (int)(dtFim - dtInicial).TotalSeconds;
                        Console.WriteLine("Segundos: " + (int)(dtFim - dtInicial).TotalSeconds + ". Média: " + (segTotal / cProva) + ". Provas: " + cProva);
                    }

                    //Atualiza Provas, Calendario_has_Professor e adiciona fila reserva no BD
                    AtualizaProvas(provasEscaladasAtualizar);
                    AtualizaCalendarioProfessor(professorAtualizar, calendario_prof, c.Codigo);
                    //GeraListaReservaHorario(h.Provas, calendario_prof, disponibilidades, listProfessorN, c, todasTurmas, todosCursos);
                    GeraListaReservasHorario(h.Provas, profReservaMoocaEng, profReservaMooca, profReservaButataEng, profReservaButata, c, dp.Data, h.Hora);
                }
            }

            Console.WriteLine("Média em seg por prova: " + (segTotal / cProva));

            return BD.RetornaCalendarioProva();           
        }

        private void GeraListaReservasHorario(List<Prova> provas, List<Professor> profReservaMoocaEng,
            List<Professor> profReservaMooca, List<Professor> profReservaButataEng, List<Professor> profReservaButata, Calendario c, DateTime dia, TimeSpan hora)
        {

            List<Fila_Reserva> fila_Reserva = new List<Fila_Reserva>();

            #region Mooca - Eng

            profReservaMoocaEng = profReservaMoocaEng.GroupBy(elem => elem.Codigo).Select(group => group.First()).ToList();
            foreach (Professor prof in profReservaMoocaEng)
            {
                Prova prova = provas.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();
                if (prova == null)
                {
                    Fila_Reserva f = new Fila_Reserva()
                    {
                        Calendario_Codigo = c.Codigo,
                        Data_Fila = dia,
                        Hora_Fila = hora,
                        Professor_Codigo = prof.Codigo,
                        Unidade = Unidade.Mooca,
                        Engenharia = true
                    };

                    fila_Reserva.Add(f);
                }

            }

            #endregion

            #region Mooca - Not Eng

            profReservaMooca = profReservaMooca.GroupBy(elem => elem.Codigo).Select(group => group.First()).ToList();

            foreach (Professor prof in profReservaMooca)
            {
                Prova prova = provas.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();
                if (prova == null)
                {
                    Fila_Reserva f = new Fila_Reserva()
                    {
                        Calendario_Codigo = c.Codigo,
                        Data_Fila = dia,
                        Hora_Fila = hora,
                        Professor_Codigo = prof.Codigo,
                        Unidade = Unidade.Mooca,
                        Engenharia = false
                    };

                    fila_Reserva.Add(f);
                }

            }

            #endregion

            #region Butata - Eng
            profReservaButataEng = profReservaButataEng.GroupBy(elem => elem.Codigo).Select(group => group.First()).ToList();

            foreach (Professor prof in profReservaButataEng)
            {
                Prova prova = provas.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();
                if (prova == null)
                {
                    Fila_Reserva f = new Fila_Reserva()
                    {
                        Calendario_Codigo = c.Codigo,
                        Data_Fila = dia,
                        Hora_Fila = hora,
                        Professor_Codigo = prof.Codigo,
                        Unidade = Unidade.Butata,
                        Engenharia = true
                    };

                    fila_Reserva.Add(f);
                }

            }

            #endregion

            #region Butata - Not Eng
            profReservaButata = profReservaButata.GroupBy(elem => elem.Codigo).Select(group => group.First()).ToList();
            foreach (Professor prof in profReservaButata)
            {
                Prova prova = provas.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();
                if (prova == null)
                {
                    Fila_Reserva f = new Fila_Reserva()
                    {
                        Calendario_Codigo = c.Codigo,
                        Data_Fila = dia,
                        Hora_Fila = hora,
                        Professor_Codigo = prof.Codigo,
                        Unidade = Unidade.Butata,
                        Engenharia = false
                    };

                    fila_Reserva.Add(f);
                }

            }

            #endregion

            BD.IncluirFilaReserva(fila_Reserva);

            
        }

        private bool ProfessorEResponsavel(List<ProfessorDisciplina> prof_disciplina, Professor prof, Prova prova)
        {
            bool result = false;

            ProfessorDisciplina pd = prof_disciplina.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();

            if (pd != null)
                if (pd.Responsavel && pd.Unidade == prova.Unidade)
                    result = true;

            return result;
        }
        
        private bool ProfessorPodeAplicarAProva(Professor prof, List<Turma> todasTurmas, List<Curso> todosCursos, bool ehEngenharia, int turma_codigo, DateTime data, int hora, int ano, Unidade unidade, List<Disponibilidade> disponibilidades, TipoCurso regime)
        {
            bool result = false;

            Curso c = RetornaCurso(todasTurmas, todosCursos, turma_codigo);

            if (c != null)
            {
                if ((c.Nome.ToLower().Contains("engenharia") && ehEngenharia) || (!c.Nome.ToLower().Contains("engenharia") && !ehEngenharia))
                {
                    Disponibilidade disp = RetornaDisponibilidadeCurso(data, prof.Codigo, hora, ano, unidade, disponibilidades, regime, c);
                    if (disp != null)
                        result = true;
                }
               
            }

            return result;
        }

        private bool? RetornaSeCursoEhEngenharia(List<Turma> todasTurmas, List<Curso> todosCursos, int turma_codigo)
        {
            Turma turma = todasTurmas.Where(p => p.Codigo == turma_codigo).FirstOrDefault();
            if (turma != null)
            {
                Curso curso = todosCursos.Where(p => p.Codigo == turma.CodigoCurso).FirstOrDefault();
                if (curso != null)
                {
                    return curso.Nome.ToLower().Contains("engenharia");
                }
            }
            return null;
        }

        private Curso RetornaCurso(List<Turma> todasTurmas, List<Curso> todosCursos, int turma_codigo)
        {

             Turma turma = todasTurmas.Where(p => p.Codigo == turma_codigo).FirstOrDefault();
            if (turma != null)
            {
                Curso curso = todosCursos.Where(p => p.Codigo == turma.CodigoCurso).FirstOrDefault();
                if (curso != null)
                {
                    return curso;
                }
            }

            return null;
        }

        private void AtualizaCalendarioProfessor(List<Professor> profs, List<CalendarioProfessor> calendario_prof, int codCalendario)
        {
            foreach (Professor prof in profs)
            {
                BD.DiminuiCargaHoraria(_cargaHorariaDiminuir, codCalendario, prof.Codigo);

                CalendarioProfessor cp = calendario_prof.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();
                if (cp != null)
                    cp.Carga_Horaria_Disponivel -= _cargaHorariaDiminuir;

            }
        }

        private void AtualizaProvas(List<Prova> provasEscaladasAtualizar)
        {
            foreach (Prova p in provasEscaladasAtualizar)
            {
                BD.AlteraAplicador(p.Codigo, p.Professor_Codigo);
            }

        }

        private Disponibilidade RetornaDisponibilidadeCurso(DateTime data, int profCod, int hora, int ano, Unidade unidade, List<Disponibilidade> disponibilidades, TipoCurso regime, Curso c)
        {
            Disponibilidade disp = disponibilidades.Where(p =>
                    p.Professor_Codigo == profCod &&
                    p.Ano == ano &&
                    p.Unidade == unidade &&
                    (p.Dia_Semana - 1) == ((int)data.DayOfWeek) &&
                    (p.Horario.Hours >= hora && p.Horario.Hours <= (hora + 1)) &&
                    p.TipoCurso == regime &&
                    p.Curso_Codigo == c.Codigo).FirstOrDefault();

            return disp;
        }

        private Disponibilidade RetornaDisponibilidade(DateTime data, int profCod, int hora, int ano, Unidade unidade, List<Disponibilidade> disponibilidades, TipoCurso regime)
        {
            Disponibilidade disp = disponibilidades.Where(p =>
                    p.Professor_Codigo == profCod &&
                    p.Ano == ano &&
                    p.Unidade == unidade &&
                    (p.Dia_Semana - 1) == ((int)data.DayOfWeek) &&
                    (p.Horario.Hours >= hora && p.Horario.Hours <= (hora + 1)) &&
                    p.TipoCurso == regime).FirstOrDefault();

            return disp;
        }

        private Professor RetornaProfQueLecionaDisciplinaHorario(List<ProfessorDisciplina> prof_disciplina, int disciplinaCodigo, DateTime data, int hora, int ano, Unidade unidade, List<Disponibilidade> disponibilidades, List<Professor> profs, TipoCurso regime)
        {
            List<ProfessorDisciplina> aux = prof_disciplina.Where(p => p.Disciplina_Codigo == disciplinaCodigo).ToList();

            foreach (ProfessorDisciplina pd in aux)
            {
                Disponibilidade disp = RetornaDisponibilidade(data, pd.Professor_Codigo, hora, ano, unidade, disponibilidades, regime);

                if (disp != null)
                {
                    Professor prof = profs.Where(p => p.Codigo == pd.Professor_Codigo).FirstOrDefault();
                    return prof;
                }
            }

            return null;
        }

        private List<Professor> RetornaListaProfsDisponiveis(List<Disponibilidade> disponibilidades, HoraProva h, DataProva dp, List<Professor> profsNaoMesarios)
        {

            List<Professor> result = new List<Professor>();

            foreach (Professor prof in profsNaoMesarios)
            {
                Disponibilidade d = disponibilidades.Where(p =>
                    p.Professor_Codigo == prof.Codigo &&
                    (p.Horario.Hours >= h.Hora.Hours && p.Horario.Hours <= (h.Hora.Hours + 1)) &&
                    ((p.Dia_Semana - 1) == (int)dp.Data.DayOfWeek)).FirstOrDefault();

                if (d != null)
                {
                    result.Add(prof);
                }
            }



            return result;
        }

        private void ZeraPontos(List<Professor> listProfDisponiveis)
        {
            foreach (Professor prof in listProfDisponiveis)
            {
                prof.Pontos = 0;
            }
        }

        private int RetornaPontosProfLecionaDisciplina(Professor prof, Prova prova, List<ProfessorDisciplina> prof_disciplina)
        {
            //Professor que leciona a disciplina da prova que esta sendo aplicada, tem menos prioridade
            if (prof_disciplina.Where(p => p.Disciplina_Codigo == prova.Disciplina_Codigo && p.Professor_Codigo == prof.Codigo).FirstOrDefault() != null)
                return 80;
            else
                return 0;
        }

        private int RetornaPontosProfAplicouProvaPrimeiroHorario(Professor prof, List<Prova> provas)
        {
            Prova x = provas.Where(p => p.Professor_Codigo == prof.Codigo).LastOrDefault();

            if (x != null && (x.Hora.Hours == 7 || x.Hora.Hours == 13 || x.Hora.Hours == 19))
            {
                return 70;
            }
            else
            {
                return 0;
            }

        }

        private int RetornaPontosProfAplicouProvaDiaAnterior(Professor prof, List<Prova> provas, DateTime today)
        {

            var x = provas.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();

            if (x != null)
            {
                return 60;
            }
            else
                return 0;

        }

        private List<Prova> RetornaProvaDiaAnterior(List<DataProva> listDataProva, DataProva dp)
        {
            List<Prova> diaAnterior = new List<Prova>();

            DataProva dt2 = listDataProva.Where(p => p.Data == dp.Data.AddDays(-1)).FirstOrDefault();
            if (dt2 != null)
            {
                diaAnterior = dt2.RetornaProvasPorDia();
            }

            return diaAnterior;
        }

        private int RetornaPontosProfResponsavelProvaDiaAnterior(Professor prof, List<Prova> provas, DateTime today, List<ProfessorDisciplina> prof_disciplina)
        {
            int ponto = RetornaPontosSeProfResponsavelHorario(prof, provas, prof_disciplina);

            if (ponto == 100)
                ponto = ponto - 50;

            return ponto;

        }

        private int RetornaPontosSeProfResponsavelHorario(Professor prof, List<Prova> provas, List<ProfessorDisciplina> prof_disciplina)
        {
            foreach (Prova prova in provas)
            {
                //retorna professor responsavel daquela disciplina
                ProfessorDisciplina pr = prof_disciplina.Where(p => p.Professor_Codigo == prof.Codigo && p.Disciplina_Codigo == prova.Disciplina_Codigo).FirstOrDefault();

                if (pr != null)
                    return 100;
            }

            return 0;
        }


        #region Old


        //public List<CalendarioProva> GerarEscalacao(Calendario c)
        //{            List<CalendarioProva> listCalendarioProva = new List<CalendarioProva>();

        //    List<DataProva> listDataProva = BD.RetornaDatasPorCalendario(c.Codigo);            
        //    List<Professor> listProfessores = BD.RetornaTodosProfessores(); //PEGA TODOS OS PROFESSORES
        //    List<Professor> profsNaoMesarios = listProfessores.Where(p => !p.Mesario && p.Carga_Horaria >= 2).ToList(); //pegar pela carga horaria disponivel

        //    foreach (DataProva dp in listDataProva)
        //    {
        //        List<Prova> todasProvasDoDia = BD.RetornaProvaPorDia(dp.Data);
        //        foreach (HoraProva h in dp.Horas)
        //        {
        //            //pega lista de professores que nao aplicam prova naquele horario
        //            List<Professor> listProfessorN = RetornaListaProfsQueNaoAplicaramProvaNaqueleHorario
        //                (h.Provas, profsNaoMesarios, h.Hora.Hours, dp.Data);

        //            ZeraPontos(listProfessorN);
                    

        //            foreach (Prova prova in h.Provas)
        //            {
        //                //se ja tem professor escalado, pula pra não escalar dnv
        //                if (prova.Professor_Codigo > 0)
        //                    continue;

        //                //Se for prova especial, pega professor que da aula praquela disciplina e escala ele, saindo do loop
        //                if (prova.Especial)
        //                {
                            
        //                    Professor profL = BD.RetornaProfQueLecionaDisciplinaHorario(prova.Disciplina_Codigo, prova.Data, prova.Hora.Hours, c.Ano, prova.Unidade);
        //                    if (profL != null)
        //                    {
        //                        Professor profListaOficial = listProfessorN.Where(p => p.Codigo == profL.Codigo).FirstOrDefault();
        //                        if (profListaOficial != null)
        //                        {
        //                            prova.Professor_Codigo = profListaOficial.Codigo;
        //                            profListaOficial.Carga_Horaria -= 2;
        //                            BD.DiminuiCargaHoraria(2, c.Codigo, profListaOficial.Codigo);
        //                            BD.AlteraProfAplicador(profListaOficial.Codigo, prova.Codigo);


        //                            listProfessorN.Remove(profListaOficial);

        //                            CalendarioProva cp = new CalendarioProva();
        //                            cp.Calendario_Codigo = c.Codigo;
        //                            cp.Prova_Codigo = prova.Codigo;
        //                            listCalendarioProva.Add(cp);
        //                        }
        //                    }


        //                    continue;
        //                }

        //                //valida se alguma prova daquele horario é na mesma sala, se for já coloca o aplicador igual
        //                List<Prova> provaMesmaSala = h.Provas.Where(p => p.Sala_Codigo == prova.Sala_Codigo).ToList();
        //                provaMesmaSala.Remove(prova);
        //                bool saiLoop = false;

        //                foreach (Prova prv in provaMesmaSala)
        //                {
        //                    if (prv.Professor_Codigo > 0)
        //                    {
        //                        prova.Professor_Codigo = prv.Professor_Codigo;
        //                        BD.AlteraProfAplicador(prova.Professor_Codigo, prova.Codigo);

        //                        CalendarioProva cp = new CalendarioProva();
        //                        cp.Calendario_Codigo = c.Codigo;
        //                        cp.Prova_Codigo = prova.Codigo;
        //                        listCalendarioProva.Add(cp);

        //                        //pega os mesmos professores reservas da prova com o mesmo codigo
        //                        List<Fila_Reserva> fila_reserva = BD.RetornaFilaReserva(prv.Codigo, c.Codigo);
        //                        foreach (Fila_Reserva f in fila_reserva)
        //                        {
        //                            BD.InsereFilaReserva(prova.Codigo, c.Codigo, f.Professor_Codigo);
        //                        }


        //                        saiLoop = true;
        //                        continue;
        //                    }

        //                }

        //                if (saiLoop)
        //                    continue;



        //                List<Professor> profRegime = new List<Professor>();
        //                bool foiDisponibilidade = true;
        //                bool? ehEngenharia = BD.RetornaSeCursoEhEngenharia(prova.Codigo);

        //                if (ehEngenharia == null)
        //                {
        //                    foiDisponibilidade = false;
        //                    continue;
        //                }

        //                Disciplina d = BD.RetornaDisciplinaPorCodigo(prova.Disciplina_Codigo);
        //                if (d != null)
        //                {
        //                    //pega a lista de professores disponiveis naquele dia/horario semestral ou anual de acordo com a prova
        //                    foreach (Professor prof in listProfessorN)
        //                    {
        //                        CalendarioProfessor cp = BD.RetornaCalendarioProfessor(prof.Codigo, c.Codigo);
        //                        if (cp != null && cp.Carga_Horaria_Disponivel < 2)
        //                        {
        //                            continue;
        //                        }
        //                        prof.Carga_Horaria_Disponivel = cp.Carga_Horaria_Disponivel;

                                

        //                        Disponibilidade disponibilidade = BD.RetornaDisponibilidade(prof, dp.Data, h.Hora.Hours, c.Ano, ehEngenharia.GetValueOrDefault(), prova.Unidade, d.TipoCurso);

        //                        if (disponibilidade != null)
        //                        {
        //                            prof.Pontos += RetornaPontosSeProfResponsavelHorario(prof, h.Provas);
        //                            prof.Pontos += RetornaPontosProfLecionaDisciplina(prof, prova);
        //                            prof.Pontos += RetornaPontosProfAplicouProvaPrimeiroHorario(prof, todasProvasDoDia);
        //                            List<Prova> provasDiaAnterior = RetornaProvaDiaAnterior(listDataProva, dp);
        //                            prof.Pontos += RetornaPontosProfAplicouProvaDiaAnterior(prof, provasDiaAnterior, dp.Data);
        //                            prof.Pontos += RetornaPontosProfResponsavelProvaDiaAnterior(prof, provasDiaAnterior, dp.Data);
        //                            profRegime.Add(prof);
        //                            foiDisponibilidade = true;
        //                        }

        //                    }
        //                }



        //                if (foiDisponibilidade)
        //                {

        //                    profRegime = profRegime.OrderBy(p => p.Pontos).ThenByDescending(p => p.Carga_Horaria_Disponivel).ToList();


        //                    Professor profSelecionado = profRegime.FirstOrDefault();

        //                    if (profSelecionado != null)
        //                    {
        //                        prova.Professor_Codigo = profSelecionado.Codigo;
        //                        profSelecionado.Carga_Horaria -= 2;
        //                        BD.DiminuiCargaHoraria(2, c.Codigo, profSelecionado.Codigo);
        //                        BD.AlteraProfAplicador(profSelecionado.Codigo, prova.Codigo);
        //                        listProfessorN.Remove(profSelecionado);

        //                        CalendarioProva cp = new CalendarioProva();
        //                        cp.Calendario_Codigo = c.Codigo;
        //                        cp.Prova_Codigo = prova.Codigo;
        //                        listCalendarioProva.Add(cp);

        //                        profRegime.Remove(profSelecionado);

        //                        //adiciona prof reserva
        //                        List<Professor> listReservas = RetornaReservas(profRegime, h.Provas, prova);
        //                        foreach (Professor p in listReservas)
        //                        {
        //                            BD.InsereFilaReserva(prova.Codigo, c.Codigo, p.Codigo);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        CalendarioProva cp = new CalendarioProva();
        //                        cp.Calendario_Codigo = c.Codigo;
        //                        cp.Prova_Codigo = prova.Codigo;
        //                        listCalendarioProva.Add(cp);
        //                    }


        //                }

                        
        //            }
        //        }
        //    }

        //    return listCalendarioProva;

        //}

        private List<Professor> RetornaReservas(List<Professor> profRegime, List<Prova> provas, Prova pAtual)
        {
            List<Professor> result = new List<Professor>();

            foreach (Professor p in profRegime)
            {
                if (!ProfAplicouProvaNoDiaHorario(p, provas, pAtual))
                {
                    result.Add(p);
                }

                if (result.Count() == 3)
                    break;
            }

            return result;
        }
        private bool ProfAplicouProvaNoDiaHorario(Professor prof, List<Prova> provas, Prova pAtual)
        {

            Prova prova = provas.Where(p => p.Professor_Codigo == prof.Codigo && p.Codigo != pAtual.Codigo).FirstOrDefault();

            if (prova == null)
                return false;
            else
                return true;
        }

       
        //private List<Prova> RetornaProvaDiaAnterior(List<DataProva> listDataProva, DataProva dp)
        //{
        //    List<Prova> diaAnterior = new List<Prova>();

        //    DataProva dt2 = listDataProva.Where(p => p.Data == dp.Data.AddDays(-1)).FirstOrDefault();
        //    if (dt2 != null)
        //    {
        //        diaAnterior = dt2.RetornaProvasPorDia();
        //    }

        //    return diaAnterior;
        //}
        //private List<Professor> RetornaProfDisponivel(List<Professor> profs, DateTime dia, int hora, int ano, TipoCurso tipoCurso, bool ehEngenharia, Unidade unidade)
        //{
        //    List<Professor> result = new List<Professor>();
        //    foreach (Professor profNM2 in profs)
        //    {
        //        //retorna disponibilidade daquele professor
        //        Disponibilidade disponibilidade = BD.RetornaDisponibilidade(profNM2, dia, hora, ano, ehEngenharia, unidade, tipoCurso);

        //        if (disponibilidade != null)
        //            result.Add(profNM2);

        //    }
        //    return result;
        //}
        //private List<Professor> RetornaListaProfsQueNaoAplicaramProvaNaqueleHorario(List<Prova> listProvas,
        //    List<Professor> professores, int hora, DateTime day)
        //{
        //    List<Professor> list = new List<Professor>();
        //    List<Prova> provas = listProvas.Where(p => p.Data == day && p.Hora.Hours == hora).ToList();

        //    foreach (Professor prof in professores)
        //    {
        //        Prova prova = provas.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();

        //        if (prova == null)
        //            list.Add(prof);
        //    }

        //    return list;
        //}
        //private int RetornaPontosSeProfResponsavelHorario(Professor prof, List<Prova> provas)
        //{
        //    foreach (Prova prova in provas)
        //    {
        //        //retorna professor responsavel daquela disciplina
        //        Professor p = BD.RetornaProfResponsavel(prova.Disciplina_Codigo);

        //        if (p != null && p.Codigo == prof.Codigo)
        //            return 100;
        //    }

        //    return 0;
        //}
        //private int RetornaPontosProfLecionaDisciplina(Professor prof, Prova prova)
        //{
        //    //Professor que leciona a disciplina da prova que esta sendo aplicada, tem menos prioridade
        //    if (BD.ProfLecionaDisciplina(prof.Codigo, prova.Disciplina_Codigo))
        //        return 80;
        //    else
        //        return 0;
        //}
        //private int RetornaPontosProfAplicouProvaPrimeiroHorario(Professor prof, List<Prova> provas)
        //{
        //    Prova x = provas.Where(p => p.Professor_Codigo == prof.Codigo).LastOrDefault();

        //    if (x != null && (x.Hora.Hours == 7 || x.Hora.Hours == 13 || x.Hora.Hours == 19))
        //    {
        //        return 70;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
                
        //}
        //private int RetornaPontosProfAplicouProvaDiaAnterior(Professor prof, List<Prova> provas, DateTime today)
        //{

        //    var x = provas.Where(p => p.Professor_Codigo == prof.Codigo).FirstOrDefault();

        //    if (x != null)
        //    {
        //        return 60;
        //    }
        //    else
        //        return 0;

        //}
        //private int RetornaPontosProfResponsavelProvaDiaAnterior(Professor prof, List<Prova> provas, DateTime today)
        //{
        //    int ponto = RetornaPontosSeProfResponsavelHorario(prof, provas);

        //    if (ponto == 100)
        //        ponto = ponto - 50;

        //    return ponto;

        //}

        #endregion
    }
}
