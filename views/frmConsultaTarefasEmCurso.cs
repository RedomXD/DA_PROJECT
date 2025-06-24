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
        // user 
        private Utilizador utilizadorLogado;

        public frmConsultaTarefasEmCurso(Utilizador utilizador)
        {
            InitializeComponent();

            utilizadorLogado = utilizador;

            // carrega as tarefas doing ao abrir
            LoadTarefasEmCurso();
        }

        private void LoadTarefasEmCurso()
        {
            using (var db = new Basededados())
            {
                // busca tarefas com estado doing
                var tarefasEmCurso = db.Tarefas
                    .Include(t => t.Programador) // inclui info programador
                    .Include(t => t.Gestor) // inclui info gestor
                    .Include(t => t.TipoTarefa) // inclui tipo tarefa
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.doing) // filtra por doing
                    .Select(t => new
                    {
                        // pega so os campos uteis
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

                // mete a lista na grid
                gvTarefasEmCurso.DataSource = tarefasEmCurso;
            }
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            // fecha o form
            this.Close();
        }

        private void gvTarefasEmCurso_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // nao faz nada por agora
        }

        private void frmConsultaTarefasEmCurso_Load(object sender, EventArgs e)
        {
            // nada aqui
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
                saveDialog.FileName = "tarefas_concluidas.csv"; // nome default

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // chama o controller pra exportar
                    var controller = new Controllers.ControllerTarefa();
                    bool sucesso = controller.ExportarTarefasConcluidasParaCSV(gestor.Id, saveDialog.FileName);

                    // mostra msg conforme correu bem ou nao
                    MessageBox.Show(sucesso ? "Exportação realizada com sucesso!" : "Erro na exportação.");
                }
            }
        }

        // ver kanban
        private void verKanbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form kanban;

            // abre form certo conforme tipo user
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

        // ir ver tarefas terminadas
        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new frmConsultarTarefasConcluidas(utilizadorLogado);
            form.Show();
            this.Close();
        }

        // sair
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
