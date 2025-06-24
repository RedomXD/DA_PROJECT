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
    public partial class frmDetalhesTarefa : Form
    {
        // id da tarefa a editar/ver
        private int _tarefaId;

        public frmDetalhesTarefa(int tarefaId)
        {
            InitializeComponent();
            _tarefaId = tarefaId;
        }

        // quando o form carrega
        private void frmDetalhesTarefa_Load(object sender, EventArgs e)
        {
            using (var db = new Basededados())
            {
                // vai buscar a tarefa pelo id
                var tarefa = db.Tarefas.Find(_tarefaId);
                if (tarefa == null)
                {
                    MessageBox.Show("Tarefa nao encontrada.");
                    this.Close();
                    return;
                }

                // mete dados nos textboxes
                txtId.Text = tarefa.Id.ToString();

                // datas reais podem ser null, entao mete "" se for o caso
                txtDataRealini.Text = tarefa.DataRealInicio?.ToString("yyyy-MM-dd") ?? "";
                txtdataRealFim.Text = tarefa.DataRealFim?.ToString("yyyy-MM-dd") ?? "";

                txtEstado.Text = tarefa.EstadoAtual.ToString();
                txtDataCriacao.Text = tarefa.DataCreation.ToString("yyyy-MM-dd");
                txtDesc.Text = tarefa.Descricao;
                txtOrdem.Text = tarefa.Ordem.ToString();
                txtStoryPoints.Text = tarefa.StoryPoints.ToString();

                // mete datas previstas nos calendarios
                dtInicio.Value = tarefa.DataPrevistaInicio;
                dtFim.Value = tarefa.DataPrevistaFim;

                // dropdown do tipo de tarefa
                cbTipoTarefa.DataSource = db.Set<TipoTarefa>().ToList();
                cbTipoTarefa.DisplayMember = "TipoTarefaName";
                cbTipoTarefa.ValueMember = "TipoTarefaId";
                cbTipoTarefa.SelectedValue = tarefa.TipoTarefaId;

                // dropdown do programador
                cbProgramador.DataSource = db.Programadors
                    .Select(p => new { p.Id, p.Nome })
                    .ToList();
                cbProgramador.DisplayMember = "Nome";
                cbProgramador.ValueMember = "Id";
                cbProgramador.SelectedValue = tarefa.ProgramadorID;
            }
        }

        // botao gravar - guarda as alteracoes feitas no form
        private void btGravar_Click_1(object sender, EventArgs e)
        {
            using (var db = new Basededados())
            {
                // volta a buscar a tarefa pelo id
                var tarefa = db.Tarefas.Find(_tarefaId);
                if (tarefa == null)
                {
                    MessageBox.Show("Tarefa nao encontrada.");
                    return;
                }

                // atualiza os campos com os valores do form
                tarefa.DataRealInicio = string.IsNullOrWhiteSpace(txtDataRealini.Text)
                    ? (DateTime?)null
                    : DateTime.Parse(txtDataRealini.Text);

                tarefa.DataRealFim = string.IsNullOrWhiteSpace(txtdataRealFim.Text)
                    ? (DateTime?)null
                    : DateTime.Parse(txtdataRealFim.Text);

                tarefa.Descricao = txtDesc.Text;

                // converte o texto do estado pra enum
                tarefa.EstadoAtual = (Tarefa.estadoatual)Enum.Parse(typeof(Tarefa.estadoatual), txtEstado.Text);

                tarefa.DataPrevistaInicio = dtInicio.Value;
                tarefa.DataPrevistaFim = dtFim.Value;
                tarefa.Ordem = int.Parse(txtOrdem.Text);
                tarefa.StoryPoints = int.Parse(txtStoryPoints.Text);
                tarefa.TipoTarefaId = (int)cbTipoTarefa.SelectedValue;
                tarefa.ProgramadorID = (int)cbProgramador.SelectedValue;

                // guarda no db
                db.SaveChanges();

                MessageBox.Show("Alteracoes salvas com sucesso!");
                this.Close();
            }
        }

        // sair
        private void btFechar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
