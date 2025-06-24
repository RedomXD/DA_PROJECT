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

                // Esconder botão de apagar tarefas
                btApagarTarefa.Visible = false;

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
                    // DataRealInicio e DataRealFim só vão ser definidos na mudança do Estadi no ControllerTarefa.cs
                    DataCreation = DateTime.Now,
                    Ordem = 1,
                    ProgramadorID = programador.Id,
                    GestorID = gestor.Id,
                    TipoTarefaId = tipoTarefa.TipoTarefaId
                };

                // Tentar guardar a tarefa na base de dados
                db.Tarefas.Add(novaTarefa);

                try
                {
                    db.SaveChanges();

                    //Obter o ID após salvar e abrir o formulário de detalhes
                    var frmDetalhes = new frmDetalhesTarefa(novaTarefa.Id);
                    frmDetalhes.ShowDialog();

                    MessageBox.Show("Tarefa adicionada com sucesso!");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    MessageBox.Show("Erro ao gravar tarefa: " + ex.InnerException?.InnerException?.Message);
                }
            }

            //Atualiza a vista depois de adicionar tarefa ao form
            LoadTarefas();
        }


        private void btApagarTarefa_Click(object sender, EventArgs e)
        {
            Tarefa tarefa = null;

            // verifica se alguma tarefa foi selecionada em algum dos 3 estados
            if (lstTodo.SelectedItem is Tarefa t1)
                tarefa = t1;
            else if (lstDoing.SelectedItem is Tarefa t2)
                tarefa = t2;
            else if (lstDone.SelectedItem is Tarefa t3)
                tarefa = t3;

            // se encontrou uma tarefa selecionada entao continua
            if (tarefa != null)
            {
                // mostra caixa de confirmacao
                var confirm = MessageBox.Show($"deseja apagar a tarefa '{tarefa.Descricao}'", "confirmacao", MessageBoxButtons.YesNo);

                // se confirmar que quer apagar
                if (confirm == DialogResult.Yes)
                {
                    // abre ligacao com a base de dados
                    using (var db = new Basededados())
                    {
                        // procura a tarefa na base de dados
                        var tarefaDb = db.Tarefas.Find(tarefa.Id);

                        // se encontrou entao remove e guarda
                        if (tarefaDb != null)
                        {
                            db.Tarefas.Remove(tarefaDb);
                            db.SaveChanges();
                        }
                    }
                    LoadTarefas();
                }
            }
            else
            {
                MessageBox.Show("seleciona uma tarefa para apagar");
            }
        }
        private void btSetDoing_Click_1(object sender, EventArgs e)
        {
            // verifica se ha uma tarefa selecionada em todo
            if (lstTodo.SelectedItem is Tarefa tarefa)
            {
                // verifica se o user atual e um programador
                if (utilizadorLogado is Programador programador)
                {
                    // cria o controller para tratar das tarefas
                    var controller = new Controllers.ControllerTarefa();

                    // tenta mover a tarefa para doing
                    bool sucesso = controller.MoverTarefa(tarefa.Id, programador.Id, Tarefa.estadoatual.doing);

                    if (!sucesso)
                        MessageBox.Show("nao podes mover para doing verifica se ja tens 2 tarefas ou se ha tarefas pendentes antes desta");
                    
                    LoadTarefas();
                }
            }
        }

        private void btSetTodo_Click_1(object sender, EventArgs e)
        {
            // verifica se ha uma tarefa selecionada em doing
            if (lstDoing.SelectedItem is Tarefa tarefa)
            {
                // verifica se o user e um programador
                if (utilizadorLogado is Programador programador)
                {
                    // cria o controller das tarefas
                    var controller = new Controllers.ControllerTarefa();

                    // tenta mover para o estado todo
                    bool sucesso = controller.MoverTarefa(tarefa.Id, programador.Id, Tarefa.estadoatual.todo);

                    if (!sucesso)
                        MessageBox.Show("nao foi possivel mover para todo");

                    LoadTarefas();
                }
            }
        }

        private void btSetDone_Click_1(object sender, EventArgs e)
        {
            // verifica se ha tarefa selecionada em doing
            if (lstDoing.SelectedItem is Tarefa tarefa)
            {
                // verifica se quem esta logado e um programador
                if (utilizadorLogado is Programador programador)
                {
                    // cria o controller para tratar disso
                    var controller = new Controllers.ControllerTarefa();

                    // tenta mover para done
                    bool sucesso = controller.MoverTarefa(tarefa.Id, programador.Id, Tarefa.estadoatual.done);

                    // se falhar mostra mensagem
                    if (!sucesso)
                        MessageBox.Show("nao foi possivel concluir a tarefa verifica se ela esta mesmo em doing");

                    LoadTarefas();
                }
            }
        }




        //MenuToolStrip, o DropDown Menu e suas funcionalidades - abaixo

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(utilizadorLogado is Gestor gestor))
            {
                MessageBox.Show("Apenas o Gestor pode exportar tarefas.");
                return;
            }

            using (SaveFileDialog savefiledialog = new SaveFileDialog())
            {
                savefiledialog.Filter = "CSV Files (*.csv)|*.csv";
                savefiledialog.FileName = "tarefas_concluidas.csv";

                if (savefiledialog.ShowDialog() == DialogResult.OK)
                {
                    var controllerTarefa = new Controllers.ControllerTarefa();
                    controllerTarefa.ExportarTarefasConcluidasParaCSV(gestor.Id, savefiledialog.FileName);
                    MessageBox.Show("Exportação concluída em sucesso!!", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Toolstrip redirect
        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gereUtilizadores = new frmGereUtilizadores(utilizadorLogado);

            gereUtilizadores.Show();

            this.Close();
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gereTiposTarefa = new frmGereTiposTarefas(utilizadorLogado);

            gereTiposTarefa.Show();

            this.Close();
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaTerminadas = new frmConsultarTarefasConcluidas(utilizadorLogado);

            tarefaTerminadas.Show();

            this.Close();
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaEmCurso = new frmConsultaTarefasEmCurso(utilizadorLogado);

            tarefaEmCurso.Show();

            this.Close();
        }



        //Botao de Previsão para a Estimativa de Conclusão com base nos StoryPoints
        private void btPrevisao_Click(object sender, EventArgs e)
        {
            // POR FAZER

            var controllerTarefa = new Controllers.ControllerTarefa();
            bool ProgramadorLogado = utilizadorLogado is Programador;

            DateTime? previsao = controllerTarefa.PreverConclusaoTarefas(utilizadorLogado.Id, ProgramadorLogado);

            if (previsao == null)
            {
                MessageBox.Show("Dados insuficientes para calcular a previsão.");
            }
            else
            {
                MessageBox.Show($"Previsão de conclusão das tarefas pendentes: {previsao.Value:dd/MM/yyyy}",
                                "Estimativa de Conclusão",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

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

        private void frmKanban_Load(object sender, EventArgs e)
        {

        }

    }
}