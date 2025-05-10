namespace GUI
{
    partial class FrmPrincipal
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
            this.gestionarEspeciesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarRazasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionarEspeciesToolStripMenuItem,
            this.gestionarRazasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gestionarEspeciesToolStripMenuItem
            // 
            this.gestionarEspeciesToolStripMenuItem.Image = global::GUI.Properties.Resources.especies;
            this.gestionarEspeciesToolStripMenuItem.Name = "gestionarEspeciesToolStripMenuItem";
            this.gestionarEspeciesToolStripMenuItem.Size = new System.Drawing.Size(198, 29);
            this.gestionarEspeciesToolStripMenuItem.Text = "Gestionar Especies";
            this.gestionarEspeciesToolStripMenuItem.Click += new System.EventHandler(this.gestionarEspeciesToolStripMenuItem_Click);
            // 
            // gestionarRazasToolStripMenuItem
            // 
            this.gestionarRazasToolStripMenuItem.Image = global::GUI.Properties.Resources.razas;
            this.gestionarRazasToolStripMenuItem.Name = "gestionarRazasToolStripMenuItem";
            this.gestionarRazasToolStripMenuItem.Size = new System.Drawing.Size(177, 29);
            this.gestionarRazasToolStripMenuItem.Text = "Gestionar Razas";
            this.gestionarRazasToolStripMenuItem.Click += new System.EventHandler(this.gestionarRazasToolStripMenuItem_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 417);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.Text = "FrmPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gestionarEspeciesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarRazasToolStripMenuItem;
    }
}