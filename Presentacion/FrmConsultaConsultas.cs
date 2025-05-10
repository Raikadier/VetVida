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
    public partial class FrmConsultaConsultas: Form
    {
        private readonly ConsultaVeterinariaService consultaVeterinariaService;
        public FrmConsultaConsultas()
        {
            InitializeComponent();
            consultaVeterinariaService = new ConsultaVeterinariaService();
            CrgarTabla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void CrgarTabla()
        {
            var lista = consultaVeterinariaService.GetAll();
            if (lista.Count == 0)
            {
                MessageBox.Show("No hay consultas registrados");
                return;
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (var consulta in lista)
                {
                    dataGridView1.Rows.Add(consulta.Id,consulta.Fecha.ToShortDateString(),consulta.Mascota.Nombre,consulta.Diagnostico,consulta.Tratamiento);
                }
            }
        }
    }
}
