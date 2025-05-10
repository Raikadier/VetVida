using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using ENTITY;

namespace GUI
{
    public partial class FrmConsultaEsoecies: Form
    {
        IService<Especie> serviceEspecie;
        public FrmConsultaEsoecies()
        {
            InitializeComponent();
            serviceEspecie = new EspecieService();
        }

        private void CargarEspecies()
        {
            var lista = serviceEspecie.Consultar();

            dgvEspecies.DataSource = lista;
            //foreach (var item in lista)
            //{
            //    dgvEspecies.Rows.Add(item.Id, item.Nombre);
            //}
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmConsultaEsoecies_Load(object sender, EventArgs e)
        {
            CargarEspecies();
        }
    }
}
