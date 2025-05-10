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
    public partial class FrmMascota : Form
    {
        private Form menuPrincipal;
        private readonly MascotaService mascotaService;
        private readonly PropietarioService propietarioService;
        private readonly RazaService razaService;
        public FrmMascota(Form menu)
        {
            InitializeComponent();
            this.menuPrincipal = menu;
            mascotaService = new MascotaService();
            propietarioService = new PropietarioService();
            razaService = new RazaService();
            CargarCombos();
            CargarLista();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarMascota(txtId, txtNombre, txtEdad);
        }
        private void GuardarMascota(TextBox txtId, TextBox txtNombre, TextBox txtEdad)
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEdad.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id) || !int.TryParse(txtEdad.Text, out int edad))
            {
                MessageBox.Show("Los IDs y la Edad deben ser numeros enteros");
                return;
            }

            Mascota mascota = new Mascota
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Raza = razaService.GetById((int)cbRaza.SelectedValue),
                Edad = int.Parse(txtEdad.Text),
                Propietario = propietarioService.GetById((int)cbPropietario.SelectedValue)
            };
            var resultado = mascotaService.Save(mascota);
            LimpiarCampos();
            CargarLista();
            MessageBox.Show(resultado.Mensaje);
        }
        private void CargarLista()
        {
            lstMascota.Items.Clear();
            foreach (var vet in mascotaService.GetAll())
            {
                lstMascota.Items.Add(vet);
            }
        }
        private void CargarLista(Mascota mascota)
        {
            lstMascota.Items.Clear();
            lstMascota.Items.Add(mascota);
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtEdad.Clear();
            txtBuscarId.Clear();
            txtId.Enabled = true;
            CargarLista();
            btnLimpiar.Enabled = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarMascota(txtBuscarId);
        }
        private void BuscarMascota(TextBox txtBuscarId)
        {
            if (string.IsNullOrEmpty(txtBuscarId.Text))
            {
                MessageBox.Show("Por favor ingrese el ID de la mascota a buscar");
                return;
            }
            if (!int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var mascota = mascotaService.GetById(id);
            if (mascota != null)
            {
                CargarLista(mascota);
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Veterinario no encontrado");
            }
        }

        private void lstMascota_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMascota.SelectedItem is Mascota masc)
            {
                txtId.Text = masc.Id.ToString();
                txtId.Enabled = false;
                txtNombre.Text = masc.Nombre;
                txtEdad.Text = masc.Edad.ToString();
                cbRaza.Text = masc.Raza.Nombre;
                cbPropietario.Text = masc.Propietario.Nombre;
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

        private void FrmMascota_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir(e);
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
        private void Eliminar()
        {
            if (lstMascota.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione una mascota para eliminar");
                return;
            }
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar a {txtNombre.Text}?",
                                      "Confirmar eliminación",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                var resultado = mascotaService.Delete(int.Parse(txtId.Text));

                if (resultado.Exito)
                {
                    MessageBox.Show("Mascota eliminado exitosamente.");
                    CargarLista();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la mascota.");
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
            menuPrincipal.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarMascota(txtId, txtNombre, txtEdad);
        }
        private void ModificarMascota(TextBox txtId, TextBox txtNombre, TextBox txtEdad)
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtEdad.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id) || !int.TryParse(txtEdad.Text, out int edad))
            {
                MessageBox.Show("Los IDs y la edad deben ser numeros enteros");
                return;
            }
            Mascota mascota = new Mascota
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Raza = razaService.GetById((int)cbRaza.SelectedValue),
                Edad = int.Parse(txtEdad.Text),
                Propietario = propietarioService.GetById((int)cbPropietario.SelectedValue)
            };
            var resultado = mascotaService.Update(mascota);
            if (resultado.Exito)
            {
                MessageBox.Show("Mascota actualizada correctamente.");
                CargarLista();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar la mascota.");
            }
        }

        private void btnBuscarProp_Click(object sender, EventArgs e)
        {
            BuscarPorPropietario(txtPropietarioBuscar);
        }
        private void BuscarPorPropietario(TextBox txtPropietarioBuscar)
        {
            if (string.IsNullOrEmpty(txtPropietarioBuscar.Text))
            {
                MessageBox.Show("Por favor ingrese el ID del propietario a buscar");
                return;
            }
            if (!int.TryParse(txtPropietarioBuscar.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var mascotas = mascotaService.SearchForEntity(1, int.Parse(txtPropietarioBuscar.Text));
            if (mascotas != null)
            {
                lstMascota.Items.Clear();
                foreach (var mascota in mascotas)
                {
                    lstMascota.Items.Add(mascota);
                }
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("No se encontraron mascotas para este propietario");
            }
        }

        private void btnBuscarRaza_Click(object sender, EventArgs e)
        {
            BuscarPorRaza(txtBuscarRaza);
        }
        private void BuscarPorRaza(TextBox txtRazaBuscar)
        {
            if (string.IsNullOrEmpty(txtRazaBuscar.Text))
            {
                MessageBox.Show("Por favor ingrese el ID de la raza a buscar");
                return;
            }
            if (!int.TryParse(txtRazaBuscar.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var mascotas = mascotaService.SearchForEntity(2, int.Parse(txtRazaBuscar.Text));
            if (mascotas != null)
            {
                lstMascota.Items.Clear();
                foreach (var mascota in mascotas)
                {
                    lstMascota.Items.Add(mascota);
                }
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("No se encontraron mascotas para esta raza");
            }
        }
        private void CargarCombos()
        {
            var razas = razaService.GetAll();
            var propietarios = propietarioService.GetAll();
            if (razas.Count == 0 || propietarios.Count == 0)
            {

            }
            else
            {
                cbRaza.DataSource = razas;
                cbRaza.DisplayMember = "Nombre";
                cbRaza.ValueMember = "Id";
                cbPropietario.DataSource = propietarios;
                cbPropietario.DisplayMember = "Nombre";
                cbPropietario.ValueMember = "Id";
            }
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Form form = new FrmConsultaMascota();
            form.ShowDialog();
        }
    }
}
