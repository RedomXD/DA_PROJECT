using iTasks.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmGereUtilizadores : Form
    {
       
        List<Programador> ListaProgramadores = new List<Programador>();
        List<Gestor> ListaGestores = new List<Gestor>();

        private Utilizador utilizadorLogado;


        public frmGereUtilizadores(Utilizador utilizador)
        {
            // so entra se for Gestor senão mostra mensagem e fecha o formulário
            if (!(utilizador is Gestor))
            {
                MessageBox.Show("Apenas Gestores podem aceder a este formulário.");
                Close();
                return;
            }

            InitializeComponent();

            utilizadorLogado = utilizador;

            // Preenche os campos com os enums correspondentes
            cbDepartamento.Items.AddRange(Enum.GetNames(typeof(Departamento)));
            cbNivelProg.Items.AddRange(Enum.GetNames(typeof(NivelExperiencia)));

            AtualizarListas();
        }

        private void AtualizarListas()
        {
            // vai buscar os Gestores e Programadores da base de dados
            var Controllerutilizadores = new ControllerUtilizadores();

            ListaGestores = Controllerutilizadores.ListarGestores();
            ListaProgramadores = Controllerutilizadores.ListarProgramadores();

            // atualiza os dados nas ListBox
            cbGestorProg.DataSource = null;
            cbGestorProg.DataSource = ListaGestores;

            lstListaGestores.DataSource = null;
            lstListaGestores.DataSource = ListaGestores;

            lstListaProgramadores.DataSource = null;
            lstListaProgramadores.DataSource = ListaProgramadores;
        }

        // um bocado self explanatory
        private void LimparCamposGestor()
        {
            txtNomeGestor.Text = "";
            txtUsernameGestor.Text = "";
            txtPasswordGestor.Text = "";
            chkGereUtilizadores.Checked = false;
            cbDepartamento.SelectedIndex = -1;
        }

        private void LimparCamposProgramador()
        {
            txtNomeProg.Text = "";
            txtUsernameProg.Text = "";
            txtPasswordProg.Text = "";
            cbNivelProg.SelectedIndex = -1;
            cbGestorProg.SelectedIndex = -1;
        }


        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            // Validação de Campos Obrigatórios Gestor
            if(string.IsNullOrEmpty(txtNomeGestor.Text) || string.IsNullOrEmpty(txtUsernameGestor.Text) ||
               string.IsNullOrEmpty(txtPasswordGestor.Text) || cbDepartamento.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor preencha todos os campos do Gestor");
                return;
            }

            var Controllerutilizadores = new ControllerUtilizadores();

            // Verificação de Username único 
            if (Controllerutilizadores.UsernameExist(txtUsernameGestor.Text))
            {
                MessageBox.Show("O nome do Utilizador já está em uso.");
                return;
            }

            try
            {
                Departamento departamento = (Departamento)Enum.Parse(typeof(Departamento), cbDepartamento.SelectedItem.ToString());

                // Desta forma fica mais simplificado os Valores a atribuir ao Gestor para a base de dados
                // Chama-se GestorAdicionar do ControllerUtilizadores e atribui por ordem os respetivos valores
                Controllerutilizadores.GestorAdicionar(
                    txtNomeGestor.Text,
                    txtUsernameGestor.Text,
                    txtPasswordGestor.Text,
                    departamento,
                    chkGereUtilizadores.Checked);

                MessageBox.Show("Gestor Gravado com Sucesso!");

                AtualizarListas();
                LimparCamposGestor();

            }
            catch (Exception x)
            {
                MessageBox.Show( "Errro ao gravar Gestor: " + x.Message);
            }
        }

        private void btnUpdateGestores_Click(object sender, EventArgs e)
        {
            if (lstListaGestores.SelectedItem is Gestor gestorSelecionado)
            {
                if (string.IsNullOrWhiteSpace(txtNomeGestor.Text) || cbDepartamento.SelectedIndex == -1)
                {
                    MessageBox.Show("Preencha todos os campos obrigatórios para atualizar.");
                    return;
                }

                var Controllerutilizadores = new ControllerUtilizadores();

                try
                {
                    Departamento departamento = (Departamento)Enum.Parse(typeof(Departamento), cbDepartamento.SelectedItem.ToString());

                    Controllerutilizadores.AtualizarGestor(
                        gestorSelecionado.Id,
                        txtNomeGestor.Text,
                        txtUsernameGestor.Text,
                        txtPasswordGestor.Text,
                        departamento,
                        chkGereUtilizadores.Checked);


                    /*
                    var nome = txtNomeGestor.Text;
                    var username = txtUsernameGestor.Text;
                    var password = txtPasswordGestor.Text;
                    var departamento = (Departamento)Enum.Parse(typeof(Departamento), cbDepartamento.SelectedItem.ToString());
                    var gereUtilizadores = chkGereUtilizadores.Checked;

                    Controllerutilizadores.AtualizarGestor(gestorSelecionado.Id, nome, username, password, departamento, gereUtilizadores);
                    */

                    MessageBox.Show("Gestor atualizado com sucesso!");

                    AtualizarListas();
                    LimparCamposGestor();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao Atualizar o Gestor" +ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um Gestor.");
            }
        }

        private void btnApagarGestores_Click(object sender, EventArgs e)
        {
            // verifica se gester escolhido foi algum gestor da lista
            if (lstListaGestores.SelectedItem is Gestor gestorSelecionado)
            {
                // aparece uma caixa a perguntar se queres mesmo apagar
                var confirm = MessageBox.Show(
                    $"tem a certeza que quer apagar o gestor '{gestorSelecionado.Nome}'",
                    "confirmacao",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                // Se disser que sim entao apaga o gestor
                if (confirm == DialogResult.Yes)
                {
                    // instance do controller para tratar da base de dados
                    var Controllerutilizadores = new ControllerUtilizadores();

                    // chama o metodo para apagar o gestor pelo id
                    Controllerutilizadores.ApagarGestor(gestorSelecionado.Id);

                    MessageBox.Show("gestor apagado com sucesso");
                    AtualizarListas();
                    LimparCamposGestor();
                }
            }
            else
            {
                // se nao tiveres nenhum gestor selecionado mostra este aviso
                MessageBox.Show("seleciona um gestor da lista primeiro");
            }
        }



        private void btGravarProg_Click(object sender, EventArgs e)
        {
            // Validação de Campos Obrigatórios Programador
            if (string.IsNullOrEmpty(txtNomeProg.Text) || string.IsNullOrEmpty(txtUsernameProg.Text) ||
               string.IsNullOrEmpty(txtPasswordProg.Text) || cbNivelProg.SelectedIndex == -1 || cbGestorProg.SelectedItem == null)
            {
                MessageBox.Show("Por favor preencha todos os campos do Programador.");
                return;
            }


            var Controllerutilizadores = new ControllerUtilizadores();

            // Verificação de Username único 
            if (Controllerutilizadores.UsernameExist(txtUsernameProg.Text))
            {
                MessageBox.Show("O nome do utilizador já está em uso.");
                    return;
            }

            try
            {

                NivelExperiencia nivel = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), cbNivelProg.SelectedItem.ToString());
                Gestor gestor = (Gestor)cbGestorProg.SelectedItem;

                // desta forma fica mais simplificado os Valores a atribuir ao Programador para a base de dados
                // Chama GestorAdicionar do ControllerUtilizadores e atribui por ordem os respetivos valores
                Controllerutilizadores.ProgramadorAdicionar(
                    txtNomeProg.Text,
                    txtUsernameProg.Text,
                    txtPasswordProg.Text,
                    nivel,
                    gestor);

                MessageBox.Show("Programador Gravado com successo!");

                AtualizarListas();
                LimparCamposProgramador();
            }
            catch (Exception x)
            {
                MessageBox.Show("Errro ao gravar o Programador: " + x.Message);
            }
        }

        private void btnUpdateProg_Click(object sender, EventArgs e)
        {
            if (lstListaProgramadores.SelectedItem is Programador progSelecionado)
            {
                if (string.IsNullOrWhiteSpace(txtNomeProg.Text) || cbNivelProg.SelectedIndex == -1 || cbGestorProg.SelectedItem == null)
                {
                    MessageBox.Show("Preencha todos os campos obrigatórios para atualizar.");
                    return;
                }


                var Controllerutilizadores = new ControllerUtilizadores();

                try
                {

                    NivelExperiencia nivel = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), cbNivelProg.SelectedItem.ToString());
                    Gestor gestor = (Gestor)cbGestorProg.SelectedItem;

                    // Desta forma fica mais simplificado os Valores a atribuir ao Programador para a base de dados
                    // Chama-se AtualizarProgramador do ControllerUtilizadores e atribui por ordem os respetivos valores
                    Controllerutilizadores.AtualizarProgramador(
                        progSelecionado.Id,
                        txtNomeProg.Text,
                        txtUsernameProg.Text,
                        txtPasswordProg.Text,
                        nivel,
                        gestor);

                    MessageBox.Show("Programador atualizado com sucesso! Yippie!!");

                    AtualizarListas();
                    LimparCamposProgramador();
                }
                catch (Exception x)
                {
                    MessageBox.Show("Errro ao gravar o Programador: " + x.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um Programador.");
            }
        }

        private void btnApagarProg_Click(object sender, EventArgs e)
        {
            // verifica se o programador é um programador da lista
            if (lstListaProgramadores.SelectedItem is Programador progSelecionado)
            {
                // mostra uma caixa a perguntar se tens a certeza
                var confirm = MessageBox.Show(
                    $"tem a certeza que quer apagar o programador '{progSelecionado.Nome}'",
                    "confirmacao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // se disser que sim entao apaga o programador
                if (confirm == DialogResult.Yes)
                {
                    // instance do controller para tratar da base de dados
                    var Controllerutilizadores = new ControllerUtilizadores();

                    // chama o metodo para apagar o programador pelo id
                    Controllerutilizadores.ApagarProgramador(progSelecionado.Id);

                    MessageBox.Show("programador apagado com sucesso");
                    AtualizarListas();
                    LimparCamposProgramador();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um Programador.");
            }
        }


        private void cbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbNivelProg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbGestorProg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkGereUtilizadores_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

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

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaEmCurso = new frmConsultaTarefasEmCurso(utilizadorLogado);

            tarefaEmCurso.Show();

            this.Close();
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tarefaTerminadas = new frmConsultarTarefasConcluidas(utilizadorLogado);

            tarefaTerminadas.Show();

            this.Close();
        }
    }
}
