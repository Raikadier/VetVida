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
    public partial class FrmEspecie: Form
    {
        private Form menuPrincipal;
        private readonly EspecieService especieService;

        public FrmEspecie(Form menu)
        {
            InitializeComponent();
            especieService = new EspecieService();
            this.menuPrincipal = menu;
            CargarLista();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEspecie.SelectedItem is Especie especie)
            {
                txtId.Text = especie.Id.ToString();
                txtId.Enabled = false;
                txtNombre.Text = especie.Nombre;
                btnGuardar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar(txtId , txtNombre);
        }
        private void Guardar(TextBox txtId, TextBox txtNombre)
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
            Especie especie = new Especie
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text
            };
            var resultado = especieService.Save(especie);
            LimpiarCampos();
            CargarLista();
            MessageBox.Show(resultado.Mensaje);
            CargarLista();
        }
        private void CargarLista()
        {
            lstEspecie.Items.Clear();
            foreach (var especie in especieService.GetAll())
            {
                lstEspecie.Items.Add(especie);
            }
        }
        private void CargarLista(Especie especie)
        {
            lstEspecie.Items.Clear();
            lstEspecie.Items.Add(especie);
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
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
            BuscarEspecie(txtBuscarId);
        }

        private void BuscarEspecie (TextBox txtBuscarId)
        {
            if (string.IsNullOrEmpty(txtBuscarId.Text))
            {
                MessageBox.Show("Por favor ingrese el ID de la especie a buscar");
                return;
            }
            if (!int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var especie = especieService.GetById(id);
            if (especie != null)
            {
                CargarLista(especie);
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Especie no encontrado");
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
            menuPrincipal.Show();
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
            if (lstEspecie.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione una especie para eliminar");
                return;
            }
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar a {txtNombre.Text}?",
                                      "Confirmar eliminación",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                var resultado = especieService.Delete(int.Parse(txtId.Text));

                if (resultado.Exito)
                {
                    MessageBox.Show("Especie eliminada exitosamente.");
                    CargarLista();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la especie.");
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarEspecie();
        }
        private void ModificarEspecie()
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("El ID debe ser numeros enteros");
                return;
            }
            Especie especie = new Especie
            {
                Id = int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
            };
            var resultado = especieService.Update(especie);
            if (resultado.Exito)
            {
                MessageBox.Show("Especie actualizada correctamente.");
                CargarLista();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar la especie.");
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }
        private void Consultar()
        {
            Form form = new FrmConsultaEspecie();
            form.ShowDialog();
        }
    }
}
