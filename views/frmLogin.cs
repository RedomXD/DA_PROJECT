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

            // backdoor hardcoded para testes
            if (username == "adm" && password == "1234")
            {
                // criar um utilizador falso só para passar no login
                utilizador = new Gestor
                {
                    Username = "adm",
                    Password = "1234",
                    Nome = "Administrador Backdoor"
                };

                return true;
            }

            try
            {
                using (var db = new Basededados())
                {
                    // procura na base de dados um utilizador o username e password
                    utilizador = db.Utilizadors
                        .OfType<Utilizador>() 
                        .SingleOrDefault(u => u.Username == username && u.Password == password);

                    if (utilizador == null)
                    {
                        //login falhou
                        MessageBox.Show("Utilizador ou password inválidos.");
                        return false;
                    }

                    //login com sucesso
                    return true;
                }
            }
            catch (Exception ex)
            {
                // erro ao aceder a base de dados (pode ser por ficheiro em falta, falha de ligacao...)
                MessageBox.Show("Erro ao tentar fazer login: " + ex.Message);
                return false;
            }

        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            //validaaco dos campos username e password na textBox
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            // validate para o Login do utilizador
            if (VerifyLogin(username, password, out Utilizador utilizador))
            {
                
                MessageBox.Show("Login efetuado com sucesso!!");

                //é necessário diferenciar entre gestor e programador
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
