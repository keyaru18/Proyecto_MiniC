namespace MiniC
{
    partial class frmEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpcNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.OpcAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.OpcGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.OpcGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.OpcSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbEditor = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivosToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.compilarToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivosToolStripMenuItem
            // 
            this.archivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpcNuevo,
            this.OpcAbrir,
            this.OpcGuardar,
            this.OpcGuardarComo,
            this.toolStripSeparator1,
            this.OpcSalir});
            this.archivosToolStripMenuItem.Name = "archivosToolStripMenuItem";
            this.archivosToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.archivosToolStripMenuItem.Text = "Archivos";
            // 
            // OpcNuevo
            // 
            this.OpcNuevo.Name = "OpcNuevo";
            this.OpcNuevo.Size = new System.Drawing.Size(231, 34);
            this.OpcNuevo.Text = "Nuevo";
            this.OpcNuevo.Click += new System.EventHandler(this.OpcNuevo_Click);
            // 
            // OpcAbrir
            // 
            this.OpcAbrir.Name = "OpcAbrir";
            this.OpcAbrir.Size = new System.Drawing.Size(231, 34);
            this.OpcAbrir.Text = "Abrir";
            this.OpcAbrir.Click += new System.EventHandler(this.OpcAbrir_Click);
            // 
            // OpcGuardar
            // 
            this.OpcGuardar.Name = "OpcGuardar";
            this.OpcGuardar.Size = new System.Drawing.Size(231, 34);
            this.OpcGuardar.Text = "Guardar";
            this.OpcGuardar.Click += new System.EventHandler(this.OpcGuardar_Click);
            // 
            // OpcGuardarComo
            // 
            this.OpcGuardarComo.Name = "OpcGuardarComo";
            this.OpcGuardarComo.Size = new System.Drawing.Size(231, 34);
            this.OpcGuardarComo.Text = "Guardar Como";
            this.OpcGuardarComo.Click += new System.EventHandler(this.OpcGuardarComo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
            // 
            // OpcSalir
            // 
            this.OpcSalir.Name = "OpcSalir";
            this.OpcSalir.Size = new System.Drawing.Size(231, 34);
            this.OpcSalir.Text = "Salir";
            this.OpcSalir.Click += new System.EventHandler(this.OpcSalir_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(73, 29);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.compilarToolStripMenuItem.Text = "Compilar";
            this.compilarToolStripMenuItem.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(79, 29);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // rtbEditor
            // 
            this.rtbEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEditor.Location = new System.Drawing.Point(0, 33);
            this.rtbEditor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtbEditor.Name = "rtbEditor";
            this.rtbEditor.Size = new System.Drawing.Size(1200, 659);
            this.rtbEditor.TabIndex = 1;
            this.rtbEditor.Text = "";
            // 
            // frmEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.rtbEditor);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmEditor";
            this.Text = "MiniC";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpcNuevo;
        private System.Windows.Forms.ToolStripMenuItem OpcAbrir;
        private System.Windows.Forms.ToolStripMenuItem OpcGuardar;
        private System.Windows.Forms.ToolStripMenuItem OpcGuardarComo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem OpcSalir;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbEditor;
    }
}

