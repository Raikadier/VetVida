using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class FrmMain: Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void Salir(FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la aplicacion?", "Cerrar Aplicacion", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir(e);
        }

        private void btnVeterinario_Click(object sender, EventArgs e)
        {
            AbrirFrmVeterinario();
        }
        private void AbrirFrmVeterinario()
        {
            FrmVeterinario frmVeterinario = new FrmVeterinario(this);
            frmVeterinario.Show();
            this.Hide();
        }

        private void btnPropietario_Click(object sender, EventArgs e)
        {
            FrmPropietario frmPropietario = new FrmPropietario(this);
            frmPropietario.Show();
            this.Hide();
        }

        private void btnMascota_Click(object sender, EventArgs e)
        {
            FrmMascota frmMascota = new FrmMascota(this);
            frmMascota.Show();
            this.Hide();
        }

        private void btnRaza_Click(object sender, EventArgs e)
        {
            FrmRaza frmRaza = new FrmRaza(this);
            frmRaza.Show();
            this.Hide();
        }

        private void btnEspecies_Click(object sender, EventArgs e)
        {
            FrmEspecie frmEspecie = new FrmEspecie(this);
            frmEspecie.Show();
            this.Hide();
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            FrmConsultas frmConsultas = new FrmConsultas(this);
            frmConsultas.Show();
            this.Hide();
        }
    }
}
