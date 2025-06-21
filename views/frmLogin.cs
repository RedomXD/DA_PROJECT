using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace iTasks
{
    public partial class frmLogin : Form
    {

        private const int SALTSIZE = 8;
        private const int NUMBER_OF_ITERATIONS = 1000;

        public frmLogin()
        {
            InitializeComponent();
        }

        private bool VerifyLogin(string username, string password, out Utilizador utilizador)
        {

            utilizador = null;

            try
            {
                using (var db = new Basededados())
                {
                    // Procurar utilizador com username e password exatos
                    utilizador = db.Utilizadors
                        .OfType<Utilizador>()
                        .SingleOrDefault(u => u.Username == username && u.Password == password);

                    if (utilizador == null)
                    {
                        MessageBox.Show("Utilizador ou password inválidos.");
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar fazer login: " + ex.Message);
                return false;
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            //Validação dos Campos Username e Password na TextBox
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            // Validações para o Login do Utilizador
            if (VerifyLogin(username, password, out Utilizador utilizador))
            {
                //Login efetuado com sucesso
                MessageBox.Show("Login efetuado com sucesso!!");

                //Agora é necessário Diferenciar entre Gestor e Programador
                if (utilizador is Gestor gestor)
                {
                    frmKanban kanban = new frmKanban(gestor); // Inicia o kanban para o Gestor
                    kanban.Show();
                }
                else if (utilizador is Programador programador)
                {
                    frmKanban kanban = new frmKanban(programador); // Inicia o kanban para Programador
                    kanban.Show();
                }
                else
                {
                    MessageBox.Show("Tipo de utilizador não reconhecido.");
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Hide();  // esconder em vez de fechar
            }
            else
            {
                MessageBox.Show("Autenticação falhou.");
            }
        }



        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
