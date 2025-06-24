namespace iTasks
{
    partial class frmGereTiposTarefas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstLista = new System.Windows.Forms.ListBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btGravar = new System.Windows.Forms.Button();
            this.btnApagarTipoTarefa = new System.Windows.Forms.Button();
            this.btnUpdateTipoTarefa = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ficheiroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilizadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarefasTerminadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarefasEmCursoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verKanbanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstLista);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 385);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista de Tipos de Tarefas";
            // 
            // lstLista
            // 
            this.lstLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLista.FormattingEnabled = true;
            this.lstLista.Location = new System.Drawing.Point(3, 16);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(268, 366);
            this.lstLista.TabIndex = 0;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(359, 78);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(286, 20);
            this.txtDesc.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Descrição:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(359, 52);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(62, 20);
            this.txtId.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Id:";
            // 
            // btGravar
            // 
            this.btGravar.Location = new System.Drawing.Point(506, 117);
            this.btGravar.Name = "btGravar";
            this.btGravar.Size = new System.Drawing.Size(139, 38);
            this.btGravar.TabIndex = 31;
            this.btGravar.Text = "Gravar Dados";
            this.btGravar.UseVisualStyleBackColor = true;
            this.btGravar.Click += new System.EventHandler(this.btGravar_Click_1);
            // 
            // btnApagarTipoTarefa
            // 
            this.btnApagarTipoTarefa.Location = new System.Drawing.Point(506, 204);
            this.btnApagarTipoTarefa.Name = "btnApagarTipoTarefa";
            this.btnApagarTipoTarefa.Size = new System.Drawing.Size(139, 22);
            this.btnApagarTipoTarefa.TabIndex = 32;
            this.btnApagarTipoTarefa.Text = "Apagar Tipo Tarefa";
            this.btnApagarTipoTarefa.UseVisualStyleBackColor = true;
            this.btnApagarTipoTarefa.Click += new System.EventHandler(this.btnApagarTipoTarefa_Click);
            // 
            // btnUpdateTipoTarefa
            // 
            this.btnUpdateTipoTarefa.Location = new System.Drawing.Point(506, 160);
            this.btnUpdateTipoTarefa.Name = "btnUpdateTipoTarefa";
            this.btnUpdateTipoTarefa.Size = new System.Drawing.Size(139, 38);
            this.btnUpdateTipoTarefa.TabIndex = 33;
            this.btnUpdateTipoTarefa.Text = "Atualizar Tipo Tarefa";
            this.btnUpdateTipoTarefa.UseVisualStyleBackColor = true;
            this.btnUpdateTipoTarefa.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ficheiroToolStripMenuItem,
            this.utilizadoresToolStripMenuItem,
            this.listagensToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(674, 24);
            this.menuStrip1.TabIndex = 34;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ficheiroToolStripMenuItem
            // 
            this.ficheiroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem});
            this.ficheiroToolStripMenuItem.Name = "ficheiroToolStripMenuItem";
            this.ficheiroToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.ficheiroToolStripMenuItem.Text = "Ficheiro";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // utilizadoresToolStripMenuItem
            // 
            this.utilizadoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verKanbanToolStripMenuItem});
            this.utilizadoresToolStripMenuItem.Name = "utilizadoresToolStripMenuItem";
            this.utilizadoresToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.utilizadoresToolStripMenuItem.Text = "Gestão da Aplicação";
            // 
            // listagensToolStripMenuItem
            // 
            this.listagensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tarefasTerminadasToolStripMenuItem,
            this.tarefasEmCursoToolStripMenuItem});
            this.listagensToolStripMenuItem.Name = "listagensToolStripMenuItem";
            this.listagensToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.listagensToolStripMenuItem.Text = "Listagens";
            // 
            // tarefasTerminadasToolStripMenuItem
            // 
            this.tarefasTerminadasToolStripMenuItem.Name = "tarefasTerminadasToolStripMenuItem";
            this.tarefasTerminadasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tarefasTerminadasToolStripMenuItem.Text = "Tarefas Concluídas";
            this.tarefasTerminadasToolStripMenuItem.Click += new System.EventHandler(this.tarefasTerminadasToolStripMenuItem_Click);
            // 
            // tarefasEmCursoToolStripMenuItem
            // 
            this.tarefasEmCursoToolStripMenuItem.Name = "tarefasEmCursoToolStripMenuItem";
            this.tarefasEmCursoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tarefasEmCursoToolStripMenuItem.Text = "Tarefas em Curso";
            this.tarefasEmCursoToolStripMenuItem.Click += new System.EventHandler(this.tarefasEmCursoToolStripMenuItem_Click);
            // 
            // verKanbanToolStripMenuItem
            // 
            this.verKanbanToolStripMenuItem.Name = "verKanbanToolStripMenuItem";
            this.verKanbanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.verKanbanToolStripMenuItem.Text = "Ver Kanban";
            this.verKanbanToolStripMenuItem.Click += new System.EventHandler(this.verKanbanToolStripMenuItem_Click);
            // 
            // frmGereTiposTarefas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 433);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnUpdateTipoTarefa);
            this.Controls.Add(this.btnApagarTipoTarefa);
            this.Controls.Add(this.btGravar);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmGereTiposTarefas";
            this.Text = "frmGereTiposTarefas";
            this.Load += new System.EventHandler(this.frmGereTiposTarefas_Load);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstLista;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btGravar;
        private System.Windows.Forms.Button btnApagarTipoTarefa;
        private System.Windows.Forms.Button btnUpdateTipoTarefa;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ficheiroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilizadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listagensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarefasTerminadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarefasEmCursoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verKanbanToolStripMenuItem;
    }
}