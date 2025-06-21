using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace iTasks
{
    public partial class frmKanban : Form
    {

        private Utilizador utilizadorLogado;

        // Contrutor para Programador
        public frmKanban(Programador programador)
        {
            InitializeComponent();

            utilizadorLogado = programador;
            this.Load += frmKanban_Load_1;
        }

        // Contrutor para Gestor
        public frmKanban(Gestor gestor)
        {
            InitializeComponent();

            utilizadorLogado = gestor;
            this.Load += frmKanban_Load_1;
        }



        //O Roman e André Usaram a Rampa dos Açores.

        // Heróis do mar, nobre povo,
        // Nação valente, imortal,
        // Levantai hoje de novo
        // O esplendor de Portugal!!


        private void frmKanban_Load_1(object sender, EventArgs e)
        {
            //Transforma a Label para Demonstrar o Nome do utilizador a usar o Kanban
            label1.Text = $"Bem vindo: {utilizadorLogado.Nome}";

            // Ocultar botões conforme as regras, ou seja, exclusões para os Programadores 
            if (utilizadorLogado is Programador)
            {
                // só o Gestor pode Criar Tarefas, buttão Nova Tarefa desaparece para Programador
                btNova.Visible = false;

                // só o Gestor pode gerir os Utilizadores e Tipo de Tarefas, dropdown desaparece para Programador
                gerirUtilizadoresToolStripMenuItem.Visible = false;
                gerirTiposDeTarefasToolStripMenuItem.Visible = false;
            }

            LoadTarefas();
        }


        private void LoadTarefas()
        {
            using (var db = new Basededados())
            {
                var tarefas = db.Tarefas
                    .Include(t => t.Programador)
                    .Include(t => t.Gestor)
                    .Include(t => t.TipoTarefa)
                    .ToList();


                // É necessário filtrar por Programador cada tarefa ? Ver regras 9. a 11.
                if (utilizadorLogado is Programador p)
                {
                    tarefas = tarefas
                        .Where(t => t.Programador.Id == p.Id)
                        .ToList();
                }


                // Tarefas para To Do 
                lstTodo.DataSource = tarefas
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.todo)
                    .OrderBy(t => t.Ordem)
                    .ToList();
                lstTodo.DisplayMember = "Descricao";
                lstTodo.ValueMember = "Id";

                // Tarefas para Doing
                lstDoing.DataSource = tarefas
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.doing)
                    .OrderBy(t => t.Ordem)
                    .ToList();
                lstDoing.DisplayMember = "Descricao";
                lstDoing.ValueMember = "Id";

                // Tarefas para Done
                lstDone.DataSource = tarefas
                    .Where(t => t.EstadoAtual == Tarefa.estadoatual.done)
                    .OrderBy(t => t.Ordem)
                    .ToList();
                lstDone.DisplayMember = "Descricao";
                lstDone.ValueMember = "Id";
            }
        }

        private void UpdateTarefaEstado(int tarefaId, Tarefa.estadoatual novoEstado)
        {
            using (var db = new Basededados())
            {

                var tarefa = db.Tarefas.Find(tarefaId);

                if (tarefa == null)
                {
                    return;
                }
                else
                {
                    tarefa.EstadoAtual = novoEstado;

                    // markar as datas Reais do Estado Atual
                    // Alterar a Data Real Incio e Data Real Fim do Nova Tarefa
                    if(novoEstado == Tarefa.estadoatual.doing)
                    {
                        tarefa.DataRealInicio = DateTime.Now;
                    }
                    else if (novoEstado == Tarefa.estadoatual.done)
                    {
                        tarefa.DataRealFim = DateTime.Now;
                    }

                    db.SaveChanges();
                }
            }

            LoadTarefas();
        }


        //WARNING !! VER !!

        //BUTTONS QUE ESTAVAM MAL REFERENCIADOS FORAM COMENTADOS E TAMBEM FIZ ALTERAÇOES E APRIMORAÇOES
        // TUDO ENCONTRA-SE MAIS ABAIXO, SAO TODOS QUE TERMINEM COM Click_1
        // Os butaos com Click 1 são os originais do Desgign atual por outras palavras.
        // Estes devem ter sido criado com click_1 pois já existia outros com msm nome mas não referenciados


        // Heróis do mar, nobre povo,
        // Nação valente, imortal,
        // Levantai hoje de novo
        // O esplendor de Portugal!!

        /*
            private void btNova_Click(object sender, EventArgs e)
            {
                using (var db = new Basededados())
                {
                    var novaTarefa = new Tarefa
                    {
                        Descricao = "Nova tarefa",
                        EstadoAtual = Tarefa.estadoatual.todo,
                        DataPrevistaInicio = DateTime.Now,
                        DataPrevistaFim = DateTime.Now.AddDays(7),
                        DataRealInicio = DateTime.Now,
                        DataRealFim = DateTime.Now,
                        DataCreation = DateTime.Now,
                        Ordem = 1,
                        Programador = db.Programadors.Find(1), // n sei como fazer isto, acho que esta a associar sempre ao ID 1?
                        Gestor = db.Gestors.Find(1), // n sei como fazer isto, acho que esta a associar sempre ao ID 1?
                        TipoTarefa = db.tipoTarefas.Find(1), // n sei como fazer isto, acho que esta a associar sempre ao ID 1?
                    };

                    db.Tarefas.Add(novaTarefa);
                    db.SaveChanges();

                    MessageBox.Show("Tarefa adicionada com sucesso!");
                }

                LoadTarefas();
            }
        */

        /*
            private void btSetDoing_Click(object sender, EventArgs e)
            {
                if (lstTodo.SelectedItem is Tarefa tarefa)
                {
                    UpdateTarefaEstado(tarefa.Id, Tarefa.estadoatual.doing);
                }
            }
        */

        /*
            private void btSetTodo_Click(object sender, EventArgs e)
            {
                if (lstDoing.SelectedItem is Tarefa tarefa)
                {
                    UpdateTarefaEstado(tarefa.Id, Tarefa.estadoatual.todo);
                }
            }
        */

        /*
            private void btSetDone_Click(object sender, EventArgs e)
            {
                if (lstDoing.SelectedItem is Tarefa tarefa)
                {
                    UpdateTarefaEstado(tarefa.Id, Tarefa.estadoatual.done);
                }
            }
        */

        /*
         * 
        private void UpdateTarefaEstado(int tarefaId, Tarefa.estadoatual novoEstado)
        {
            using (var db = new Basededados())
            {
                var tarefa = db.Tarefas.FirstOrDefault(t => t.Id == tarefaId);
                if (tarefa != null)
                {
                    tarefa.EstadoAtual = novoEstado;
                    db.SaveChanges();
                }
            }

            LoadTarefas();
        }
        
         * */




        private void btNova_Click_1(object sender, EventArgs e)
        {
            if (!(utilizadorLogado is Gestor gestor))
            {
                MessageBox.Show("Apenas gestores podem criar tarefas.");
                return;
            }

            using (var db = new Basededados())
            {
                // Buscar o primeiro programador associado ao gestor
                var programador = db.Programadors.FirstOrDefault(p => p.GestorID == gestor.Id);

                if (programador == null)
                {
                    MessageBox.Show("Nenhum programador associado ao gestor.");
                    return;
                }

                // Buscar o primeiro tipo de tarefa existente
                var tipoTarefa = db.tipoTarefas.FirstOrDefault();

                if (tipoTarefa == null)
                {
                    MessageBox.Show("Nenhum tipo de tarefa disponível.");
                    return;
                }

                // Criar a nova tarefa com os IDs corretos
                var novaTarefa = new Tarefa
                {
                    Descricao = "Nova tarefa",
                    EstadoAtual = Tarefa.estadoatual.todo,
                    DataPrevistaInicio = DateTime.Now,
                    DataPrevistaFim = DateTime.Now.AddDays(7),
                    DataCreation = DateTime.Now,
                    Ordem = 1,
                    ProgramadorId = programador.Id,
                    GestorID = gestor.Id,
                    TipoTarefaId = tipoTarefa.TipoTarefaId
                };

                // Tentar guardar a tarefa na base de dados
                db.Tarefas.Add(novaTarefa);

                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Tarefa adicionada com sucesso!");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    MessageBox.Show("Erro ao gravar tarefa: " + ex.InnerException?.InnerException?.Message);
                }
            }

            LoadTarefas();
        }



        private void btSetDoing_Click_1(object sender, EventArgs e)
        {
            if (lstTodo.SelectedItem is Tarefa tarefa)
            {
                UpdateTarefaEstado(tarefa.Id, Tarefa.estadoatual.doing);
            }
        }

        private void btSetTodo_Click_1(object sender, EventArgs e)
        {
            if (lstDoing.SelectedItem is Tarefa tarefa)
            {
                UpdateTarefaEstado(tarefa.Id, Tarefa.estadoatual.todo);
            }
        }

        private void btSetDone_Click_1(object sender, EventArgs e)
        {
            if (lstDoing.SelectedItem is Tarefa tarefa)
            {
                UpdateTarefaEstado(tarefa.Id, Tarefa.estadoatual.done);
            }
        }


        private void btPrevisao_Click(object sender, EventArgs e)
        {
            // POR FAZER
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var db = new Basededados())
            {
                var tarefas = db.Tarefas.Include(t => t.Programador).ToList();

                var sb = new StringBuilder();
                sb.AppendLine("Id,Descrição,Estado,Programador,Ordem");

                foreach (var t in tarefas)
                {
                    sb.AppendLine($"{t.Id},\"{t.Descricao}\",{t.EstadoAtual},{t.Programador?.Nome},{t.Ordem}");
                }

                // Perguntar onde guardar
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV Files (*.csv)|*.csv";
                    sfd.FileName = "tarefas_export.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Exportação concluída com sucesso!", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        // Toolstrip redirect
        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gereUtilizadores = new frmGereUtilizadores();

            gereUtilizadores.Show();

            this.Close();
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gereTiposTarefa = new frmGereTiposTarefas();

            gereTiposTarefa.Show();

            this.Close();
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaTerminadas = new frmConsultarTarefasConcluidas();

            tarefaTerminadas.Show();

            this.Close();
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaEmCurso = new frmConsultaTarefasEmCurso();

            tarefaEmCurso.Show();

            this.Close();
        }




        // labels , To Do list , Doing List , Done List - abaixo
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lstTodo_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void lstDoing_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstDone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}