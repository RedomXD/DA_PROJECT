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



        private void frmConsultarTarefasConcluidas_Load(object sender, EventArgs e)
        {

        }



        //ToolStripMenu Opçºoes

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                    var controller = new Controllers.ControllerTarefa();
                    bool sucesso = controller.ExportarTarefasConcluidasParaCSV(gestor.Id, saveDialog.FileName);

                    MessageBox.Show(sucesso ? "Exportação realizada com sucesso!" : "Erro na exportação.");
                }
            }
        }

        private void verKanbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form kanban;

            if (utilizadorLogado is Gestor gestor)
                kanban = new frmKanban(gestor);
            else if (utilizadorLogado is Programador programador)
                kanban = new frmKanban(programador);
            else
            {
                MessageBox.Show("Tipo de utilizador desconhecido.");
                return;
            }

            kanban.Show();
            this.Close();
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmConsultaTarefasEmCurso(utilizadorLogado);
            form.Show();
            this.Close();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }


        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
