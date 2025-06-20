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

        public frmGereUtilizadores()
        {
            InitializeComponent();

            cbDepartamento.Items.AddRange(Enum.GetNames(typeof(Departamento)));

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
        }
    }
}
