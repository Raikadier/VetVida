using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTITY;
using BLL;
namespace Presentacion
{
    public partial class FrmConsultaEspecie: Form
    {
        private readonly EspecieService especieService;
        public FrmConsultaEspecie()
        {
            InitializeComponent();
            especieService = new EspecieService();
            CargarLista();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void CargarLista()
        {
            var lista= especieService.GetAll();
            if (lista.Count == 0)
            {
                MessageBox.Show("No hay especies registradas");
                return;
            }
            else
            {
                dataGridView1.Rows.Clear();
                foreach (var especie in lista)
                {
                    dataGridView1.Rows.Add(especie.Id, especie.Nombre);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
