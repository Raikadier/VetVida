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
    public partial class FrmVeterinario : Form
    {
        private Form menuPrincipal;
        private readonly VeterinarioService veterinarioService;
        public FrmVeterinario(Form menu)
        {
            InitializeComponent();
            this.menuPrincipal = menu;
            veterinarioService = new VeterinarioService();
            CargarLista();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarVeterinario(txtId, txtNombre, txtApellido, txtCedula, txtTelefono, txtEspecialidad);
        }
        private void GuardarVeterinario(TextBox txtId, TextBox txtNombre, TextBox txtApellido, TextBox txtCedula, TextBox txtTelefono, TextBox txtEspecialidad)
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtEspecialidad.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id) || !int.TryParse(txtCedula.Text, out int cedula) || !int.TryParse(txtTelefono.Text, out int telefono))
            {
                MessageBox.Show("El ID, la Cedula y el Telefono deben ser numeros enteros");
                return;
            }
            Veterinario veterinario = new Veterinario
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Cedula = int.Parse(txtCedula.Text),
                Telefono = int.Parse(txtTelefono.Text),
                Especialidad = txtEspecialidad.Text
            };
            var resultado = veterinarioService.Save(veterinario);
            LimpiarCampos();
            CargarLista();
            MessageBox.Show(resultado.Mensaje);
            CargarLista();
        }
        private void CargarLista()
        {
            lstVeterinario.Items.Clear();
            foreach (var vet in veterinarioService.GetAll())
            {
                lstVeterinario.Items.Add(vet);
            }
        }
        private void CargarLista(Veterinario veterinario)
        {
            lstVeterinario.Items.Clear();
            lstVeterinario.Items.Add(veterinario);
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtEspecialidad.Clear();
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
            BuscarVeterinario(txtBuscarId);
        }
        private void BuscarVeterinario(TextBox txtBuscarId)
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
            var veterinario = veterinarioService.GetById(id);
            if (veterinario != null)
            {
                CargarLista(veterinario);
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Veterinario no encontrado");
            }
        }

        private void lstVeterinario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVeterinario.SelectedItem is Veterinario vet)
            {
                txtId.Text = vet.Id.ToString();
                txtId.Enabled = false;
                txtNombre.Text = vet.Nombre;
                txtApellido.Text = vet.Apellido;
                txtCedula.Text = vet.Cedula.ToString();
                txtTelefono.Text = vet.Telefono.ToString();
                txtEspecialidad.Text = vet.Especialidad;
                btnGuardar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
        }

        private void FrmVeterinario_Load(object sender, EventArgs e)
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

        private void FrmVeterinario_FormClosing(object sender, FormClosingEventArgs e)
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
            if (lstVeterinario.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione un veterinario para eliminar");
                return;
            }
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar a {txtNombre.Text} {txtApellido.Text}?",
                                      "Confirmar eliminación",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                var resultado = veterinarioService.Delete(int.Parse(txtId.Text));

                if (resultado.Exito)
                {
                    MessageBox.Show("Veterinario eliminado exitosamente.");
                    CargarLista();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el veterinario.");
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarVeterinario();
        }
        private void ModificarVeterinario()
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtTelefono.Text) || string.IsNullOrEmpty(txtEspecialidad.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id) || !int.TryParse(txtCedula.Text, out int cedula) || !int.TryParse(txtTelefono.Text, out int telefono))
            {
                MessageBox.Show("El ID, la Cedula y el Telefono deben ser numeros enteros");
                return;
            }
            Veterinario veterinario = new Veterinario
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Cedula = int.Parse(txtCedula.Text),
                Telefono = int.Parse(txtTelefono.Text),
                Especialidad = txtEspecialidad.Text
            };
            var resultado = veterinarioService.Update(veterinario);
            if (resultado.Exito)
            {
                MessageBox.Show("Veterinario actualizado correctamente.");
                CargarLista();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar el veterinario.");
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Form form = new FrmConsultaVeterinario();
            form.ShowDialog();
        }
    }
}
