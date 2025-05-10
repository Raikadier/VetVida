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
    public partial class FrmConsultaMascota: Form
    {
        private readonly MascotaService mascotaService;
        public FrmConsultaMascota()
        {
            InitializeComponent();
            mascotaService = new MascotaService();
            CrgarTabla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void CrgarTabla()
        {
            var lista = mascotaService.GetAll();
            if (lista.Count == 0)
            {
                MessageBox.Show("No hay mascotas registradas");
                return;
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (var masc in lista)
                {
                    dataGridView1.Rows.Add(masc.Id, masc.Nombre, masc.Edad, masc.Propietario.Nombre, masc.Raza.Nombre);
                }
            }
        }
    }
}
