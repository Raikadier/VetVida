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
    public partial class FrmConsultaVeterinario: Form
    {
        private readonly VeterinarioService veterinarioService;
        public FrmConsultaVeterinario()
        {
            InitializeComponent();
            veterinarioService = new VeterinarioService();
            CrgarTabla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void CrgarTabla()
        {
            var lista = veterinarioService.GetAll();
            if (lista.Count == 0)
            {
                MessageBox.Show("No hay veterinarios registrados");
                return;
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (var vet in lista)
                {
                    dataGridView1.Rows.Add(vet.Id, vet.Nombre, vet.Apellido, vet.Telefono, vet.Especialidad);
                }
            }
        }
    }
}
