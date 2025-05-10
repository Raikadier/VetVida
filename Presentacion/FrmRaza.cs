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
namespace Presentacion
{
    public partial class FrmRaza: Form
    {
        private Form menuPrincipal;
        private readonly RazaService razaService;
        private readonly EspecieService especieService;
        private readonly MascotaService mascotaService;
        public FrmRaza(Form menu)
        {
            InitializeComponent();
            razaService = new RazaService();
            especieService = new EspecieService();
            mascotaService = new MascotaService();
            CargarComboEspecie();
            menuPrincipal = menu;
            CargarLista();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRaza(txtId, txtNOmbre);
        }
        private void GuardarRaza(TextBox txtId, TextBox txtNombre)
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("El ID deben ser numeros enteros");
                return;
            }
            Raza raza = new Raza
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                especie = especieService.GetById((int)cbEspecie.SelectedValue)
            };
            var resultado = razaService.Save(raza);
            LimpiarCampos();
            CargarLista();
            MessageBox.Show(resultado.Mensaje);
        }
        private void CargarLista()
        {
            lstRaza.Items.Clear();
            foreach (var vet in razaService.GetAll())
            {
                lstRaza.Items.Add(vet);
            }
        }
        private void CargarLista(Raza raza)
        {
            lstRaza.Items.Clear();
            lstRaza.Items.Add(raza);
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNOmbre.Clear();
            txtBuscarId.Clear();
            txtEspecieBuscar.Clear();
            txtId.Enabled = true;
            CargarLista();
            btnLimpiar.Enabled = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnBuscarId_Click(object sender, EventArgs e)
        {
            BuscarRaza(txtBuscarId);
        }
        private void BuscarRaza(TextBox txtBuscarId)
        {
            if (string.IsNullOrEmpty(txtBuscarId.Text))
            {
                MessageBox.Show("Por favor ingrese el ID del veterinario a buscar");
                return;
            }
            if (!int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var raza = razaService.GetById(id);
            if (raza != null)
            {
                CargarLista(raza);
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Raza no encontrada");
            }
        }

        private void lstRaza_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRaza.SelectedItem is Raza raza)
            {
                txtId.Text = raza.Id.ToString();
                txtId.Enabled = false;
                txtNOmbre.Text = raza.Nombre;
                cbEspecie.Text = raza.especie.Id.ToString();
                btnGuardar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
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

        private void FrmRaza_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir(e);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
            menuPrincipal.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
        private void Eliminar()
        {
            if (lstRaza.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione una raza para eliminar");
                return;
            }
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar a {txtNOmbre.Text}?",
                                      "Confirmar eliminación",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                mascotaService.DeleteByRaza(int.Parse(txtId.Text));
                var resultado = razaService.Delete(int.Parse(txtId.Text));

                if (resultado.Exito)
                {
                    MessageBox.Show("Raza eliminada exitosamente.");
                    CargarLista();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la raza.");
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarRaza();
        }
        private void ModificarRaza()
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNOmbre.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Los IDs deben ser numeros enteros");
                return;
            }
            Raza raza = new Raza
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNOmbre.Text,
                especie = especieService.GetById((int)cbEspecie.SelectedValue)
            };
            var resultado = razaService.Update(raza);
            if (resultado.Exito)
            {
                MessageBox.Show("Raza actualizada correctamente.");
                CargarLista();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar la raza.");
            }
        }

        private void btnEspecieBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorEspecie();
        }
        private void BuscarPorEspecie()
        {
            if (string.IsNullOrEmpty(txtEspecieBuscar.Text))
            {
                MessageBox.Show("Por favor ingrese el ID de la especie a buscar");
                return;
            }
            if (!int.TryParse(txtEspecieBuscar.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var razas = razaService.SearchForEntity(1, id);
            if (razas != null)
            {
                lstRaza.Items.Clear();
                foreach (var raza in razas)
                {
                    lstRaza.Items.Add(raza);
                }
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("No se encontraron razas para esta especie");
            }
        }
        private void CargarComboEspecie()
        {
            var lista = especieService.GetAll();
            if (lista.Count == 0)
            {
                cbEspecie.DataSource = null;

            }
            else
            {
                cbEspecie.DataSource = lista;
                cbEspecie.DisplayMember = "Nombre";
                cbEspecie.ValueMember = "Id";
            }
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Form form = new FrmConsultaRaza();
            form.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
