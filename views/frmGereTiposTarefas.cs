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
        List<TipoTarefa> Listatipotarefa = new List<TipoTarefa>();
        public frmGereTiposTarefas()
        {
            InitializeComponent();
            var ControllerTipoTarefa = new ControllerTipoTarefa();
            Listatipotarefa = ControllerTipoTarefa.ListaTipoTarefa();

            lstLista.DataSource = null;
            lstLista.DataSource = Listatipotarefa;
        }



        private void btGravar_Click_1(object sender, EventArgs e)
        {
            string descricao = txtDesc.Text;
            var ControllerTipoTarefa = new ControllerTipoTarefa();
            ControllerTipoTarefa.TipoTarefaAdicionar(descricao);
            Listatipotarefa = ControllerTipoTarefa.ListaTipoTarefa();

            lstLista.DataSource = null;
            lstLista.DataSource = Listatipotarefa;
        }
    }
}
