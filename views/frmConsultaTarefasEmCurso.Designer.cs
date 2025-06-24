namespace iTasks
{
    partial class frmConsultaTarefasEmCurso
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
            this.gvTarefasEmCurso = new System.Windows.Forms.DataGridView();
            this.btFechar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ficheiroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarParaCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilizadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verKanbanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarefasTerminadasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvTarefasEmCurso)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvTarefasEmCurso
            // 
            this.gvTarefasEmCurso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTarefasEmCurso.Location = new System.Drawing.Point(12, 27);
            this.gvTarefasEmCurso.Name = "gvTarefasEmCurso";
            this.gvTarefasEmCurso.Size = new System.Drawing.Size(1026, 395);
            this.gvTarefasEmCurso.TabIndex = 0;
            this.gvTarefasEmCurso.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTarefasEmCurso_CellContentClick);
            // 
            // btFechar
            // 
            this.btFechar.Location = new System.Drawing.Point(934, 430);
            this.btFechar.Name = "btFechar";
            this.btFechar.Size = new System.Drawing.Size(104, 23);
            this.btFechar.TabIndex = 30;
            this.btFechar.Text = "Fechar";
            this.btFechar.UseVisualStyleBackColor = true;
            this.btFechar.Click += new System.EventHandler(this.btFechar_Click);
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
            this.menuStrip1.Size = new System.Drawing.Size(1050, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ficheiroToolStripMenuItem
            // 
            this.ficheiroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem,
            this.exportarParaCSVToolStripMenuItem});
            this.ficheiroToolStripMenuItem.Name = "ficheiroToolStripMenuItem";
            this.ficheiroToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.ficheiroToolStripMenuItem.Text = "Ficheiro";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // exportarParaCSVToolStripMenuItem
            // 
            this.exportarParaCSVToolStripMenuItem.Name = "exportarParaCSVToolStripMenuItem";
            this.exportarParaCSVToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.exportarParaCSVToolStripMenuItem.Text = "Exportar Tarefas Concluídas para CSV";
            this.exportarParaCSVToolStripMenuItem.Click += new System.EventHandler(this.exportarParaCSVToolStripMenuItem_Click);
            // 
            // utilizadoresToolStripMenuItem
            // 
            this.utilizadoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verKanbanToolStripMenuItem});
            this.utilizadoresToolStripMenuItem.Name = "utilizadoresToolStripMenuItem";
            this.utilizadoresToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.utilizadoresToolStripMenuItem.Text = "Gestão da Aplicação";
            // 
            // verKanbanToolStripMenuItem
            // 
            this.verKanbanToolStripMenuItem.Name = "verKanbanToolStripMenuItem";
            this.verKanbanToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.verKanbanToolStripMenuItem.Text = "Ver Kanban";
            this.verKanbanToolStripMenuItem.Click += new System.EventHandler(this.verKanbanToolStripMenuItem_Click);
            // 
            // listagensToolStripMenuItem
            // 
            this.listagensToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tarefasTerminadasToolStripMenuItem});
            this.listagensToolStripMenuItem.Name = "listagensToolStripMenuItem";
            this.listagensToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.listagensToolStripMenuItem.Text = "Listagens";
            // 
            // tarefasTerminadasToolStripMenuItem
            // 
            this.tarefasTerminadasToolStripMenuItem.Name = "tarefasTerminadasToolStripMenuItem";
            this.tarefasTerminadasToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.tarefasTerminadasToolStripMenuItem.Text = "Tarefas Concluídas";
            this.tarefasTerminadasToolStripMenuItem.Click += new System.EventHandler(this.tarefasTerminadasToolStripMenuItem_Click);
            // 
            // frmConsultaTarefasEmCurso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 464);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btFechar);
            this.Controls.Add(this.gvTarefasEmCurso);
            this.Name = "frmConsultaTarefasEmCurso";
            this.Text = "frmConsultaTarefasEmCurso";
            this.Load += new System.EventHandler(this.frmConsultaTarefasEmCurso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvTarefasEmCurso)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvTarefasEmCurso;
        private System.Windows.Forms.Button btFechar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ficheiroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarParaCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilizadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listagensToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tarefasTerminadasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verKanbanToolStripMenuItem;
    }
}