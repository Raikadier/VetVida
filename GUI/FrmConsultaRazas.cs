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
    public partial class FrmConsultaRazas: Form
    {
        RazaService serviceRaza;
        IService<Especie> serviceEspecie;
        public FrmConsultaRazas()
        {
            InitializeComponent();
            serviceEspecie = new EspecieService();
            serviceRaza = new RazaService();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CargarGrillaRazas()
        {
            var listaRazas = serviceRaza.Consultar();
            //grilla3.DataSource = listaRazas;
            dgvRazas.DataSource = listaRazas;
            //dgvRazas.Rows.Clear();
            //foreach (var item in listaRazas)
            //{
            //    dgvRazas.Rows.Add(item.Id, item.Nombre, item.Especie.Nombre);
            //}
            
        }

        private void CargarGrillaRazas2()
        {
            var listaRazas = serviceRaza.ConsultarDTO();
            grillaPrueba.DataSource = listaRazas;
        }

        private void FrmConsultaRazas_Load(object sender, EventArgs e)
        {
            CargarGrillaRazas();
            CargarGrillaRazas2();
        }
    }
}
