using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    internal class ControllerTarefa
    {

        // Criação de uma Tarefa, para trabalhar com a sua lógica
        public bool CriarTarefa(string descricao, DateTime previstaInicio, DateTime previstaFim, int storyPoints,
                                int programadorId, int gestorId, int tipoTarefaId, int ordemExecucao)
        {
            using (var db = new Basededados())
            {
                // Verificar se o programador pertence ao gestor
                var programador = db.Programadors.FirstOrDefault(p => p.Id == programadorId && p.Gestor.Id == gestorId);
                if (programador == null)
                    return false;

                // Conferir se a tarefa já está responsável por um Programador
                bool ordemExiste = db.Tarefas.Any(t => t.ProgramadorID == programadorId && t.Ordem == ordemExecucao);
                if (ordemExiste)
                    return false;

                var novaTarefa = new Tarefa
                {
                    Descricao = descricao,
                    DataPrevistaInicio = previstaInicio,
                    DataPrevistaFim = previstaFim,
                    StoryPoints = storyPoints,
                    GestorID = gestorId,
                    ProgramadorID = programadorId,
                    TipoTarefaId = tipoTarefaId,
                    Ordem = ordemExecucao,
                    EstadoAtual = Tarefa.estadoatual.todo,
                    DataCreation = DateTime.Now
                };

                db.Tarefas.Add(novaTarefa);
                db.SaveChanges();
                return true;
            }
        }



        // Atualiza o estado da tarefa com todas as validações necessárias
        public bool MoverTarefa(int tarefaId, int programadorId, Tarefa.estadoatual novoEstado)
        {
            using (var db = new Basededados())
            {

                var tarefa = db.Tarefas.FirstOrDefault(t => t.Id == tarefaId);

                if (tarefa == null || tarefa.ProgramadorID != programadorId)
                {
                    MessageBox.Show("Tarefa não encontrada ou não pertence ao programador.");
                    return false;
                }


                if (tarefa.EstadoAtual == Tarefa.estadoatual.done)
                {
                    MessageBox.Show("Tarefa já está concluída.");
                    return false;
                }



                if (novoEstado == Tarefa.estadoatual.doing)
                {

                    // Máximo 2 tarefas Doing por programador
                    int doingCount = db.Tarefas.Count(t => t.ProgramadorID == programadorId && t.EstadoAtual == Tarefa.estadoatual.doing);
                    if (doingCount >= 2)
                    {
                        MessageBox.Show("Já tens 2 tarefas em execução.");
                        return false;
                    }

                    // Só pode passar para doing se todas as tarefas anteriores estiverem concluídas
                    var tarefasAnteriores = db.Tarefas.Where(t => t.ProgramadorID == programadorId && t.Ordem < tarefa.Ordem);
                    if (tarefasAnteriores.Any(t => t.EstadoAtual != Tarefa.estadoatual.done))
                    {
                        MessageBox.Show("Tens tarefas anteriores por concluir (Ordem).");
                        return false;
                    }

                    //Inicia a DataInicial
                    tarefa.DataRealInicio = DateTime.Now;

                }
                else if (novoEstado == Tarefa.estadoatual.done)
                {
                    if (tarefa.EstadoAtual != Tarefa.estadoatual.doing)
                    { 
                        return false;
                    }

                    tarefa.DataRealFim = DateTime.Now;
                }

                tarefa.EstadoAtual = novoEstado;
                db.SaveChanges();
                return true;

            }
        }




        // Listagem todas as tarefas por estado e Programador
        public List<Tarefa> ObterTarefasPorEstado(Tarefa.estadoatual estado, int? programadorId = null)
        {
            using (var db = new Basededados())
            {
                var query = db.Tarefas.Include("Programador").Include("TipoTarefa").AsQueryable();

                if (programadorId.HasValue)
                    query = query.Where(t => t.ProgramadorID == programadorId.Value);

                return query.Where(t => t.EstadoAtual == estado).ToList();
            }
        }




        // Exportar as Tarefas para formato de Texto CSV, ou seja, arquivo separado por virgulas
        public bool ExportarTarefasConcluidasParaCSV(int gestorId, string caminhoFicheiro)
        {
            try
            {
                using (var db = new Basededados())
                {
                    var tarefas = db.Tarefas
                                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.done && t.GestorID == gestorId)
                                    .ToList();


                    using (StreamWriter writer = new StreamWriter(caminhoFicheiro))
                    {
                        writer.WriteLine("Programador;Descricao;DataPrevistaInicio;DataPrevistaFim;TipoTarefa;DataRealInicio;DataRealFim");


                        foreach (var t in tarefas)
                        {

                            // Se por algum motivo os dados relacionados não existirem, evitamos null reference
                            var programador = db.Programadors.FirstOrDefault(p => p.Id == t.ProgramadorID);

                            var tipo = db.tipoTarefas.FirstOrDefault(tp => tp.TipoTarefaId == t.TipoTarefaId);

                            // Reforço para quando o Programador for deletado ou o Tipo de Tarefa não for carregado, este apresenta como "Desconhecido"
                            string nomeProgramador = programador?.Nome ?? "Desconhecido";
                            string tipoDesc = tipo?.TipoTarefaDesc ?? "Desconhecido";

                            writer.WriteLine($"{programador?.Nome};{t.Descricao};{t.DataPrevistaInicio:dd/MM/yyyy};{t.DataPrevistaFim:dd/MM/yyyy};{tipo?.TipoTarefaDesc};{t.DataRealInicio:dd/MM/yyyy};{t.DataRealFim:dd/MM/yyyy}");
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        // Lógica para o Botão de Previsão com Base em StoryPoints
        public DateTime? PreverConclusaoTarefas(int utilizadorId, bool ProgramadorLogado)
        {
            using (var db = new Basededados())
            {
                // 1. Tarefas concluídas
                var tarefasConcluidas = db.Tarefas
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.done &&
                                ((ProgramadorLogado && t.ProgramadorID == utilizadorId) || (!ProgramadorLogado && t.GestorID == utilizadorId)) &&
                                t.DataRealInicio != null && t.DataRealFim != null &&
                                t.StoryPoints > 0)
                    .ToList();

                if (!tarefasConcluidas.Any())
                    return null;

                // 2. Média de dias por SP
                double totalDias = tarefasConcluidas.Sum(t => (t.DataRealFim - t.DataRealInicio)?.TotalDays ?? 0);

                int totalSP = tarefasConcluidas.Sum(t => t.StoryPoints);
                double mediaDiasPorSP = totalDias / totalSP;

                // 3. Tarefas pendentes
                var tarefasPendentes = db.Tarefas
                    .Where(t => (t.EstadoAtual == Tarefa.estadoatual.todo || t.EstadoAtual == Tarefa.estadoatual.doing) &&
                                ((ProgramadorLogado && t.ProgramadorID == utilizadorId) || (!ProgramadorLogado && t.GestorID == utilizadorId)))
                    .ToList();

                int storyPointsPendentes = tarefasPendentes.Sum(t => t.StoryPoints);

                if (storyPointsPendentes == 0)
                    return null;

                // 4. Estimativa
                double diasEstimados = mediaDiasPorSP * storyPointsPendentes;
                DateTime previsao = DateTime.Now.AddDays(diasEstimados);

                return previsao;
            }
        }



    }
}
