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
    public partial class FrmConsultas : Form
    {
        private Form menuPrincipal;
        private readonly ConsultaVeterinariaService consultasService;
        private readonly VeterinarioService veterinarioService;
        private readonly MascotaService mascotaService;
        public FrmConsultas(Form menu)
        {
            InitializeComponent();
            this.menuPrincipal = menu;
            consultasService = new ConsultaVeterinariaService();
            veterinarioService = new VeterinarioService();
            mascotaService = new MascotaService();
            CargarCombos();
            CargarLista();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnLimpiar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarConsulta(txtId, dateFecha, txtDiagnostico, txtTratamiento);
        }
        private void GuardarConsulta(TextBox txtId, DateTimePicker dateFecha, TextBox txtDiagnostico, TextBox txtTratamiento)
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtDiagnostico.Text) || string.IsNullOrEmpty(txtTratamiento.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Los IDs debe ser un numero entero");
                return;
            }
            ConsultaVeterinaria consulta = new ConsultaVeterinaria
            {
                Id = int.Parse(txtId.Text),
                Fecha = dateFecha.Value,
                Veterinario = veterinarioService.GetById((int)cbVeterinario.SelectedValue),
                Mascota = mascotaService.GetById((int)cbMascota.SelectedValue),
                Diagnostico = txtDiagnostico.Text,
                Tratamiento = txtTratamiento.Text
            };
            var resultado = consultasService.Save(consulta);
            LimpiarCampos();
            CargarLista();
            MessageBox.Show(resultado.Mensaje);
        }
        private void CargarLista()
        {
            lstConsultas.Items.Clear();
            foreach (var consulta in consultasService.GetAll())
            {
                lstConsultas.Items.Add(consulta);
            }
        }
        private void CargarLista(ConsultaVeterinaria consulta)
        {
            lstConsultas.Items.Clear();
            lstConsultas.Items.Add(consulta);
        }
        private void LimpiarCampos()
        {
            txtId.Clear();
            dateFecha.Value = DateTime.Now;
            txtTratamiento.Clear();
            txtDiagnostico.Clear();
            txtBuscarId.Clear();
            txtBuscarMascota.Clear();
            txtId.Enabled = true;
            CargarLista();
            btnLimpiar.Enabled = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void btnBuscarId_Click(object sender, EventArgs e)
        {
            BuscarConsulta(txtBuscarId);
        }
        private void BuscarConsulta(TextBox txtBuscarId)
        {
            if (string.IsNullOrEmpty(txtBuscarId.Text))
            {
                MessageBox.Show("Por favor ingrese el ID de la consulta a buscar");
                return;
            }
            if (!int.TryParse(txtBuscarId.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var consulta = consultasService.GetById(id);
            if (consulta != null)
            {
                CargarLista(consulta);
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Consulta no encontrada");
            }
        }

        private void lstConsultas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstConsultas.SelectedItem is ConsultaVeterinaria consulta)
            {
                txtId.Text = consulta.Id.ToString();
                txtId.Enabled = false;
                dateFecha.Value = consulta.Fecha;
                cbVeterinario.Text = consulta.Veterinario.Nombre;
                cbMascota.Text = consulta.Mascota.Nombre;
                txtDiagnostico.Text = consulta.Diagnostico;
                txtTratamiento.Text = consulta.Tratamiento;
                btnGuardar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
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

        private void FrmConsultas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir(e);
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
            if (lstConsultas.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione una consulta para eliminar");
                return;
            }
            var confirmacion = MessageBox.Show($"¿Está seguro que desea eliminar la consulta {txtId.Text} realizada el {dateFecha.Value.Date.ToString()}?",
                                      "Confirmar eliminación",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                var resultado = consultasService.Delete(int.Parse(txtId.Text));

                if (resultado.Exito)
                {
                    MessageBox.Show("Consulta eliminado exitosamente.");
                    CargarLista();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar la consulta.");
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarConsulta();
        }
        private void ModificarConsulta()
        {
            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtDiagnostico.Text) || string.IsNullOrEmpty(txtTratamiento.Text))
            {
                MessageBox.Show("Por favor complete todos los campos");
                return;
            }
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Los IDs debe ser un numero entero");
                return;
            }
            ConsultaVeterinaria consulta = new ConsultaVeterinaria
            {
                Id = int.Parse(txtId.Text),
                Fecha = dateFecha.Value,
                Veterinario = veterinarioService.GetById((int)cbVeterinario.SelectedValue),
                Mascota = mascotaService.GetById((int)cbMascota.SelectedValue),
                Diagnostico = txtDiagnostico.Text,
                Tratamiento = txtTratamiento.Text
            };
            var resultado = consultasService.Update(consulta);
            if (resultado.Exito)
            {
                MessageBox.Show("Consulta actualizada correctamente.");
                CargarLista();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al actualizar la consulta.");
            }
        }

        private void btnBuscarMascota_Click(object sender, EventArgs e)
        {
            BuscarPorMascota(txtBuscarMascota);
        }
        private void BuscarPorMascota(TextBox txtBuscarMascota)
        {
            if (string.IsNullOrEmpty(txtBuscarMascota.Text))
            {
                MessageBox.Show("Por favor ingrese el ID de la mascota a buscar");
                return;
            }
            if (!int.TryParse(txtBuscarMascota.Text, out int id))
            {
                MessageBox.Show("El ID debe ser un numero entero");
                return;
            }
            var consultas = consultasService.SearchForEntity(1, id);
            if (consultas != null)
            {
                lstConsultas.Items.Clear();
                foreach (var consulta in consultas)
                {
                    lstConsultas.Items.Add(consulta);
                }
                btnLimpiar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Consulta no encontrada");
            }
        }

        private void CargarCombos()
        {
            var listaVeterinarios = veterinarioService.GetAll();
            var listaMascotas = mascotaService.GetAll();
            if (listaVeterinarios.Count == 0|| listaMascotas.Count == 0)
            {
                return;
            }
            else
            {
                cbMascota.DataSource = listaMascotas;
                cbMascota.DisplayMember = "Nombre";
                cbMascota.ValueMember = "Id";
                cbVeterinario.DataSource = listaVeterinarios;
                cbVeterinario.DisplayMember = "Nombre";
                cbVeterinario.ValueMember = "Id";
            }

        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Form form = new FrmConsultaConsultas();
            form.ShowDialog();
        }
    }
}
