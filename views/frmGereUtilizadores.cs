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
        public frmGereUtilizadores()
        {
            InitializeComponent();

            cbDepartamento.Items.AddRange(Enum.GetNames(typeof(Departamento)));
            cbNivelProg.Items.AddRange(Enum.GetNames(typeof(NivelExperiencia)));
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

        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            string textoselecionado = cbDepartamento.SelectedItem.ToString();
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
        }

        private void btGravarProg_Click(object sender, EventArgs e)
        {
            string textoselecionado = cbNivelProg.SelectedItem.ToString();
            NivelExperiencia nivelExperiencia = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), textoselecionado);
            string programadornome = txtNomeProg.Text;
            string programadorusername = txtUsernameProg.Text;
            string gestorpass = txtPasswordProg.Text;
            Gestor gestorselecionado = (Gestor)cbGestorProg.SelectedItem;

            var Controllerutilizadores = new ControllerUtilizadores();

            Controllerutilizadores.ProgramadorAdicionar(programadornome, programadorusername, gestorpass, nivelExperiencia, gestorselecionado);
           
            ListaProgramadores = Controllerutilizadores.ListarProgramadores();
            lstListaProgramadores.DataSource = null;
            lstListaProgramadores.DataSource = ListaProgramadores;
            // botao para limpar
            // apagar registro
        }
    }
}
