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
        private List<TipoTarefa> listaTipoTarefa = new List<TipoTarefa>();
        private ControllerTipoTarefa controllerTipoTarefa = new ControllerTipoTarefa();
        public frmGereTiposTarefas()
        {
            InitializeComponent();

            /*
            var ControllerTipoTarefa = new ControllerTipoTarefa();
            listaTipoTarefa = ControllerTipoTarefa.ListaTipoTarefa();

            lstLista.DataSource = null;
            lstLista.DataSource = listaTipoTarefa;
            */
        }

        private void frmGereTiposTarefas_Load(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            listaTipoTarefa = controllerTipoTarefa.ListaTipoTarefa();

            lstLista.DataSource = null;
            lstLista.DataSource = listaTipoTarefa;
        }

        private void btGravar_Click_1(object sender, EventArgs e)
        {
            // Trim na descricao para retirar white Spaces no Inicio e Fim
            string descricao = txtDesc.Text.Trim();

            if (string.IsNullOrEmpty(descricao) )
            {
                MessageBox.Show("Por favor, Insira uma descrição para o tipo de Tarefa.");
                return;
            }

            bool adicionado = controllerTipoTarefa.TipoTarefaAdicionar(descricao);

            if (adicionado)
            {
                MessageBox.Show("Tipo de Tarefa adicionado com Sucesso !");
            }
            else
            {
                MessageBox.Show("Este tipo de Tarefa já existe.");
            }


            txtDesc.Clear();
            AtualizarLista();
            
            /*
            var ControllerTipoTarefa = new ControllerTipoTarefa();
            ControllerTipoTarefa.TipoTarefaAdicionar(descricao);
            listaTipotarefa = ControllerTipoTarefa.ListaTipoTarefa();

            lstLista.DataSource = null;
            lstLista.DataSource = listaTipotarefa;
            */
        }

        //button para atualizar
        private void button1_Click(object sender, EventArgs e)
        {
            if (!(lstLista.SelectedItem is TipoTarefa tipoSelecionado))
            {
                MessageBox.Show("Por favor, selecione um tipo de tarefa para atualizar.");
                return;
            }

            string novaDescricao = txtDesc.Text.Trim();

            if (string.IsNullOrWhiteSpace(novaDescricao))
            {
                MessageBox.Show("A descrição não pode estar vazia.");
                return;
            }

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

        private void btnApagarTipoTarefa_Click(object sender, EventArgs e)
        {
            if (!(lstLista.SelectedItem is TipoTarefa tipoSelecionado))
            {
                MessageBox.Show("Por favor, selecione um tipo de tarefa para apagar.");
                return;
            }

            DialogResult result = MessageBox.Show("Tem a certeza que deseja apagar este tipo de tarefa?", "Confirmação", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
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
    }
}
