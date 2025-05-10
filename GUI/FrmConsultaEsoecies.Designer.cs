namespace GUI
{
    partial class FrmConsultaEsoecies
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvEspecies = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecies)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(132, 389);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(338, 26);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dgvEspecies
            // 
            this.dgvEspecies.AllowUserToAddRows = false;
            this.dgvEspecies.AllowUserToDeleteRows = false;
            this.dgvEspecies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEspecies.Location = new System.Drawing.Point(36, 25);
            this.dgvEspecies.Name = "dgvEspecies";
            this.dgvEspecies.ReadOnly = true;
            this.dgvEspecies.RowHeadersWidth = 62;
            this.dgvEspecies.RowTemplate.Height = 24;
            this.dgvEspecies.Size = new System.Drawing.Size(448, 354);
            this.dgvEspecies.TabIndex = 2;
            // 
            // FrmConsultaEsoecies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 419);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.dgvEspecies);
            this.Name = "FrmConsultaEsoecies";
            this.Text = "FrmConsultaEsoecies";
            this.Load += new System.EventHandler(this.FrmConsultaEsoecies_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEspecies)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dgvEspecies;
    }
}