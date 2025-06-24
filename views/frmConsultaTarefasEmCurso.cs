using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace iTasks
{
    public partial class frmConsultaTarefasEmCurso : Form
    {

        private Utilizador utilizadorLogado;
        public frmConsultaTarefasEmCurso(Utilizador utilizador)
        {
            InitializeComponent();

            utilizadorLogado = utilizador;

            LoadTarefasEmCurso();
        }

        private void LoadTarefasEmCurso()
        {
            using (var db = new Basededados())
            {
                // busca as tarefas com estado doing
                var tarefasEmCurso = db.Tarefas
                    .Include(t => t.Programador)
                    .Include(t => t.Gestor)
                    .Include(t => t.TipoTarefa)
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.doing)
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
                gvTarefasEmCurso.DataSource = tarefasEmCurso;
            }
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void gvTarefasEmCurso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmConsultaTarefasEmCurso_Load(object sender, EventArgs e)
        {

        }



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

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmConsultarTarefasConcluidas(utilizadorLogado);
            form.Show();
            this.Close();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}