using BLL;
using ENTITY;
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
    public partial class FrmPropietario: Form
    {
        private Form menuPrincipal;
        private readonly PropietarioService propietarioService;
        public FrmPropietario(Form menuPrincipal)
        {
            InitializeComponent();
            this.menuPrincipal = menuPrincipal;
            propietarioService = new PropietarioService();
            CargarLista();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarPropietario(txtId,txtNombre,txtApellido,txtCedula,txtTelefono);
        }
        private void GuardarPropietario(TextBox txtId, TextBox txtNombre, TextBox txtApellido, TextBox txtCedula, TextBox txtTelefono)
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id) || !int.TryParse(txtCedula.Text, out int cedula) || !int.TryParse(txtTelefono.Text, out int telefono))
            {
                MessageBox.Show("El ID, la Cedula y el Telefono deben ser numeros enteros");
                return;
            }
            Propietario propietario = new Propietario
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Cedula = int.Parse(txtCedula.Text),
                Telefono = int.Parse(txtTelefono.Text)
            };
            var resultado = propietarioService.Save(propietario);
            LimpiarCampos();
            CargarLista();
            MessageBox.Show(resultado.Mensaje);
            CargarLista();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarPropietario();
        }
        private void ActualizarPropietario()
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtCedula.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id) || !int.TryParse(txtCedula.Text, out int cedula) || !int.TryParse(txtTelefono.Text, out int telefono))
            {
                MessageBox.Show("El ID, la Cedula y el Telefono deben ser numeros enteros");
                return;
            }
            Propietario propietario = new Propietario
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Cedula = int.Parse(txtCedula.Text),
                Telefono = int.Parse(txtTelefono.Text)
            };
            var resultado = propietarioService.Update(propietario);
            if (resultado.Exito)
            {
                MessageBox.Show("Propietario actualizado correctamente.");
                CargarLista();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar el propietario.");
            }
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCedula.Clear();
            txtTelefono.Clear();
            txtBuscarId.Clear();
            txtId.Enabled = true;
            CargarLista();
            btnLimpiar.Enabled = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
        }
        private void CargarLista()
        {
            lstPropietarios.Items.Clear();
            List<Propietario> propietarios = propietarioService.GetAll();
            foreach (var propietario in propietarios)
            {
                lstPropietarios.Items.Add(propietario);
            }
        }
        private void CargarLista(Propietario propietario)
        {
            lstPropietarios.Items.Clear();
            lstPropietarios.Items.Add(propietario);
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
        private void Eliminar()
        {
            if (lstPropietarios.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione un veterinario para eliminar");
                return;
            }
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar a {txtNombre.Text} {txtApellido.Text}?, tambien se eliminaran las mascotas asociadas a este propietario.",
                                      "Confirmar eliminación",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                var resultado = propietarioService.Delete(int.Parse(txtId.Text));

                if (resultado.Exito)
                {
                    MessageBox.Show("Propietario eliminado exitosamente.");
                    CargarLista();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el propietario.");
                }
            }
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
            var propietario = propietarioService.GetById(id);
            if (propietario != null)
            {
                CargarLista(propietario);
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Veterinario no encontrado");
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
            menuPrincipal.Show();
        }

        private void lstPropietarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPropietarios.SelectedItem is Propietario prop)
            {
                txtId.Text = prop.Id.ToString();
                txtId.Enabled = false;
                txtNombre.Text = prop.Nombre;
                txtApellido.Text = prop.Apellido;
                txtCedula.Text = prop.Cedula.ToString();
                txtTelefono.Text = prop.Telefono.ToString();
                btnGuardar.Enabled = false;
                btnActualizar.Enabled = true;
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

        private void FrmPropietario_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir(e);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Form form = new FrmConsultaPropietario();
            form.ShowDialog();
        }
    }
}
