using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmConsultarTarefasConcluidas : Form
    {
        public frmConsultarTarefasConcluidas()
        {
            InitializeComponent();
            LoadTarefasConcluidas();
        }

        private void LoadTarefasConcluidas()
        {
            using (var db = new Basededados())
            {
                // busca as tarefas com estado done
                var tarefasConcluidas = db.Tarefas
                    .Include(t => t.Programador)
                    .Include(t => t.Gestor)
                    .Include(t => t.TipoTarefa)
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.done)
                    .Select(t => new
                    {
                        t.Id,
                        t.Descricao,
                        Estado = t.EstadoAtual.ToString(),
                        Programador = t.Programador.Nome,
                        Gestor = t.Gestor.Nome,
                        TipoTarefa = t.TipoTarefa.TipoTarefaDesc,
                        t.DataPrevistaInicio,
                        t.DataPrevistaFim,
                        t.DataRealInicio,
                        t.DataRealFim,
                        t.DataCreation,
                        t.Ordem,
                        t.StoryPoints
                    })
                    .ToList();

                // associa a lista ao DataGridView
                gvTarefasConcluidas.DataSource = tarefasConcluidas;
            }
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
