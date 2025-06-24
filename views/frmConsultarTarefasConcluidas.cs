using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmConsultarTarefasConcluidas : Form
    {
        private Utilizador utilizadorLogado;

        public frmConsultarTarefasConcluidas(Utilizador utilizador)
        {
            InitializeComponent();

            utilizadorLogado = utilizador;

            // carrega as tarefas done logo ao abrir
            LoadTarefasConcluidas();
        }

        private void LoadTarefasConcluidas()
        {
            using (var db = new Basededados())
            {
                // busca tarefas com estado done
                var tarefasConcluidas = db.Tarefas
                    .Include(t => t.Programador) // inclui info programador
                    .Include(t => t.Gestor) // inclui info gestor
                    .Include(t => t.TipoTarefa) // inclui tipo
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.done) // filtra por done
                    .Select(t => new
                    {
                        // pega os campos para mostrar
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

                // mete na grid
                gvTarefasConcluidas.DataSource = tarefasConcluidas;
            }
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            // fecha o form
            this.Close();
        }

        private void frmConsultarTarefasConcluidas_Load(object sender, EventArgs e)
        {
     
        }

        // exportar CSV
        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // so gestor pode exportar
            if (!(utilizadorLogado is Gestor gestor))
            {
                MessageBox.Show("Apenas gestores podem exportar tarefas.");
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV files (*.csv)|*.csv";
                saveDialog.FileName = "tarefas_concluidas.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // chama o controller pra exportar
                    var controller = new Controllers.ControllerTarefa();
                    bool sucesso = controller.ExportarTarefasConcluidasParaCSV(gestor.Id, saveDialog.FileName);

                    // mostra resultado
                    MessageBox.Show(sucesso ? "Exportação realizada com sucesso!" : "Erro na exportação.");
                }
            }
        }

        // ver kanban
        private void verKanbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form kanban;

            // abre form certo consoante tipo user
            if (utilizadorLogado is Gestor gestor)
                kanban = new frmKanban(gestor);
            else if (utilizadorLogado is Programador programador)
                kanban = new frmKanban(programador);
            else
            {
                MessageBox.Show("Tipo de utilizador desconhecido.");
                return;
            }

            // mostra kanban e fecha este
            kanban.Show();
            this.Close();
        }

        // ver tarefas em curso
        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmConsultaTarefasEmCurso(utilizadorLogado);
            form.Show();
            this.Close();
        }

        // sair
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
