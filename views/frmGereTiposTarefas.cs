using iTasks.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmGereTiposTarefas : Form
    {
        // lista local dos tipos de tarefa
        private List<TipoTarefa> listaTipoTarefa = new List<TipoTarefa>();

        // controller que faz a logica
        private ControllerTipoTarefa controllerTipoTarefa = new ControllerTipoTarefa();

        // utilizador que está com sessao iniciada
        private Utilizador utilizadorLogado;

        public frmGereTiposTarefas(Utilizador utilizador)
        {
            InitializeComponent();

            utilizadorLogado = utilizador;

            // este bloco abaixo estava comentado e nao é usado, mas deixei aqui
            /*
            var ControllerTipoTarefa = new ControllerTipoTarefa();
            listaTipoTarefa = ControllerTipoTarefa.ListaTipoTarefa();

            lstLista.DataSource = null;
            lstLista.DataSource = listaTipoTarefa;
            */
        }

        private void frmGereTiposTarefas_Load(object sender, EventArgs e)
        {
            // quando abre o form, carrega a lista
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            // busca os tipos de tarefa ao controller
            listaTipoTarefa = controllerTipoTarefa.ListaTipoTarefa();

            // atualiza o listbox
            lstLista.DataSource = null;
            lstLista.DataSource = listaTipoTarefa;
        }

        private void btGravar_Click_1(object sender, EventArgs e)
        {
            // retira espaços brancos do input
            string descricao = txtDesc.Text.Trim();

            if (string.IsNullOrEmpty(descricao))
            {
                MessageBox.Show("Por favor, Insira uma descrição para o tipo de Tarefa.");
                return;
            }

            // tenta adicionar via controller
            bool adicionado = controllerTipoTarefa.TipoTarefaAdicionar(descricao);

            if (adicionado)
            {
                MessageBox.Show("Tipo de Tarefa adicionado com Sucesso !");
            }
            else
            {
                MessageBox.Show("Este tipo de Tarefa já existe.");
            }

            // limpa input e recarrega lista
            txtDesc.Clear();
            AtualizarLista();

            // este bloco era codigo antigo, deixem aqui pod e ser util
            /*
            var ControllerTipoTarefa = new ControllerTipoTarefa();
            ControllerTipoTarefa.TipoTarefaAdicionar(descricao);
            listaTipotarefa = ControllerTipoTarefa.ListaTipoTarefa();

            lstLista.DataSource = null;
            lstLista.DataSource = listaTipotarefa;
            */
        }

        // botao para atualizar tipo de tarefa selecionado
        private void button1_Click(object sender, EventArgs e)
        {
            // verifica se foi selecionado um tipo de tarefa
            if (!(lstLista.SelectedItem is TipoTarefa tipoSelecionado))
            {
                MessageBox.Show("Por favor, selecione um tipo de tarefa para atualizar.");
                return;
            }

            // limpa espaços e valida input
            string novaDescricao = txtDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(novaDescricao))
            {
                MessageBox.Show("A descrição não pode estar vazia.");
                return;
            }

            // tenta atualizar via controller
            bool atualizado = controllerTipoTarefa.AtualizarTipoTarefa(tipoSelecionado.TipoTarefaId, novaDescricao);

            if (atualizado)
            {
                MessageBox.Show("Tipo de tarefa atualizado com sucesso.");
                AtualizarLista();
                txtDesc.Clear();
            }
            else
            {
                MessageBox.Show("Erro ao atualizar o tipo de tarefa.");
            }
        }

        // botao para apagar tipo de tarefa
        private void btnApagarTipoTarefa_Click(object sender, EventArgs e)
        {
            // valida selecao
            if (!(lstLista.SelectedItem is TipoTarefa tipoSelecionado))
            {
                MessageBox.Show("Por favor, selecione um tipo de tarefa para apagar.");
                return;
            }

            // confirmacao
            DialogResult result = MessageBox.Show("Tem a certeza que deseja apagar este tipo de tarefa?", "Confirmação", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // tenta apagar via controller
                bool apagado = controllerTipoTarefa.ApagarTipoTarefa(tipoSelecionado.TipoTarefaId);

                if (apagado)
                {
                    MessageBox.Show("Tipo de tarefa apagado com sucesso.");
                    AtualizarLista();
                    txtDesc.Clear();
                }
                else
                {
                    MessageBox.Show("Erro ao apagar o tipo de tarefa.");
                }
            }
        }

        // ver tarefas terminadas
        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaTerminadas = new frmConsultarTarefasConcluidas(utilizadorLogado);
            tarefaTerminadas.Show();
            this.Close();
        }

        // ver tarefas em curso
        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaEmCurso = new frmConsultaTarefasEmCurso(utilizadorLogado);
            tarefaEmCurso.Show();
            this.Close();
        }

        // ver kanban
        private void verKanbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form kanban;

            if (utilizadorLogado is Gestor gestor)
            {
                kanban = new frmKanban(gestor);
            }
            else if (utilizadorLogado is Programador programador)
            {
                kanban = new frmKanban(programador);
            }
            else
            {
                MessageBox.Show("Tipo de utilizador desconhecido.");
                return;
            }

            kanban.Show();
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
