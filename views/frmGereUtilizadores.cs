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

            if (!(utilizador is Gestor))
            {
                MessageBox.Show("Apenas Gestores podem aceder a este formulário.");
                Close(); 
                return;
            }

            InitializeComponent();

            utilizadorLogado = utilizador;


            cbDepartamento.Items.AddRange(Enum.GetNames(typeof(Departamento)));

            cbNivelProg.Items.AddRange(Enum.GetNames(typeof(NivelExperiencia)));
            AtualizarListas();

        }

        private void AtualizarListas()
        {

            var Controllerutilizadores = new ControllerUtilizadores();

            ListaGestores = Controllerutilizadores.ListarGestores();
            ListaProgramadores = Controllerutilizadores.ListarProgramadores();

            cbGestorProg.DataSource = null;
            cbGestorProg.DataSource = ListaGestores;

            lstListaGestores.DataSource = null;
            lstListaGestores.DataSource = ListaGestores;

            lstListaProgramadores.DataSource = null;
            lstListaProgramadores.DataSource = ListaProgramadores;

        }

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




            //VENTURA MODIFIQUEI PARA FICAR DE FORMA MAIS SIMPLIFICADA
            // NAO LEVAS A MAL SO COPIEI A MAIOR PARTE DO QUE TINHAS FEITO E METI DE FORMA MAIS LEGIVEL
            // VOU COMENTAR NA MSM SE QUISEREM VER, GERALMENTE NAO ANDO A DELETAR CENAS MAS A COMENTAR
            // MAS É CAPAZ QUE EU JÁ TENHO DELETADO E NAO NOTADO


            /*
             
            string textoselecionado = cbDepartamento.SelectedItem.ToString(); // isto aqui é para o drop down
            Departamento departamento = (Departamento)Enum.Parse(typeof(Departamento), textoselecionado);
            string gestornome = txtNomeGestor.Text;
            string gestorusername = txtUsernameGestor.Text;
            string gestorpass = txtPasswordGestor.Text;
            bool gereutilizadores = chkGereUtilizadores.Checked;

            var Controllerutilizadores = new ControllerUtilizadores();

            Controllerutilizadores.GestorAdicionar(gestornome, gestorusername, gestorpass, departamento, gereutilizadores);
            ListaGestores = Controllerutilizadores.ListarGestores();
            cbGestorProg.DataSource = null;
            cbGestorProg.DataSource = ListaGestores;

            lstListaGestores.DataSource = null;
            lstListaGestores.DataSource = ListaGestores;

            */

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

            if (lstListaGestores.SelectedItem is Gestor gestorSelecionado)
            {
                var confirm = MessageBox.Show($"Tem a certeza que quer apagar o Gestor '{gestorSelecionado.Nome}'?",
                    "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    var Controllerutilizadores = new ControllerUtilizadores();
                    Controllerutilizadores.ApagarGestor(gestorSelecionado.Id);

                    MessageBox.Show("Gestor apagado com sucesso!!");
                    AtualizarListas();
                    LimparCamposGestor();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um Gestor na lista.");
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

                // Desta forma fica mais simplificado os Valores a atribuir ao Programador para a base de dados
                // Chama-se GestorAdicionar do ControllerUtilizadores e atribui por ordem os respetivos valores
                Controllerutilizadores.ProgramadorAdicionar(
                    txtNomeProg.Text,
                    txtUsernameProg.Text,
                    txtPasswordProg.Text,
                    nivel,
                    gestor);

                MessageBox.Show("Programador Gravado com successo!! yippie");

                AtualizarListas();
                LimparCamposProgramador();
            }
            catch (Exception x)
            {
                MessageBox.Show("Errro ao gravar o Programador: " + x.Message);
            }


            /*
             
            string textoselecionado = cbNivelProg.SelectedItem.ToString();
            NivelExperiencia nivelExperiencia = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), textoselecionado);
            string programadornome = txtNomeProg.Text;
            string programadorusername = txtUsernameProg.Text;
            string gestorpass = txtPasswordProg.Text;
            Gestor gestorselecionado = (Gestor)cbGestorProg.SelectedItem;



            Controllerutilizadores.ProgramadorAdicionar(programadornome, programadorusername, gestorpass, nivelExperiencia, gestorselecionado);
           
            ListaProgramadores = Controllerutilizadores.ListarProgramadores();
            lstListaProgramadores.DataSource = null;
            lstListaProgramadores.DataSource = ListaProgramadores;
            // botao para limpar
            // apagar registro

            */

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


                    /*
                     
                        var Controllerutilizadores = new ControllerUtilizadores();
                        var nome = txtNomeProg.Text;
                        var username = txtUsernameProg.Text;
                        var password = txtPasswordProg.Text;
                        var nivel = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), cbNivelProg.SelectedItem.ToString());
                        var gestor = (Gestor)cbGestorProg.SelectedItem;

                        Controllerutilizadores.AtualizarProgramador(progSelecionado.Id, nome, username, password, nivel, gestor);

                        MessageBox.Show("Programador atualizado com sucesso!");
                        AtualizarListas();
                        LimparCamposProgramador();
                    */

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
            if (lstListaProgramadores.SelectedItem is Programador progSelecionado)
            {
                var confirm = MessageBox.Show($"Tem a certeza que quer apagar o Programador '{progSelecionado.Nome}'?",
                    "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    var Controllerutilizadores = new ControllerUtilizadores();
                    Controllerutilizadores.ApagarProgramador(progSelecionado.Id);

                    MessageBox.Show("Programador apagado com sucesso!");
                    AtualizarListas();
                    LimparCamposProgramador();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um Programador.");
            }
        }


        // xd 

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
