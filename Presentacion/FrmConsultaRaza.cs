using BLL;
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
    public partial class FrmConsultaRaza: Form
    {
        private readonly RazaService razaService;
        public FrmConsultaRaza()
        {
            InitializeComponent();
            razaService = new RazaService();
            CargarTabla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void CargarTabla()
        {
            var lista = razaService.GetAll();

            if (lista.Count == 0)
            {
                MessageBox.Show("No hay especies registradas");
                return;
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (var raza in lista)
                {
                    dataGridView1.Rows.Add(raza.Id, raza.Nombre,raza.especie.Nombre);
                }
            }
        }
    }
}
