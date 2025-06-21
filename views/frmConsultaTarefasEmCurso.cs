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
        public frmConsultaTarefasEmCurso()
        {
            InitializeComponent();
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

    }
}