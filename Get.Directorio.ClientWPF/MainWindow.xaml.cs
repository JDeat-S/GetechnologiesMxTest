using Get.Directorio.ClientWPF.Models;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{

    public partial class MainWindow : Window
    {
        private readonly ApiClient _api;

        private List<UsuarioFacturasDto> _personas = new();
        public MainWindow()
        {
            _api = new ApiClient();
            InitializeComponent();
            CargarPersonasAsync();
        }
      

        private async Task CargarPersonasAsync()
        {
            try
            {
                _personas = await _api.GetAsync<List<UsuarioFacturasDto>>("/api/personas");

                // Tabla 1 → Usuarios
                dgUsuarios.ItemsSource = _personas.Select(p => new
                {
                    id = p.PersonaId,
                    nombre = p.Nombre,
                    apellidoPaterno = p.ApellidoPaterno,
                    identificacion = p.Identificacion
                }).ToList();

                // Tabla 2 → Usuarios + Facturas
                dgUsuariosFacturas.ItemsSource = _personas.Select(p => new
                {
                    id = p.PersonaId,
                    nombre = p.Nombre,
                    facturasDescripcion = string.Join(", ", p.Facturas.Select(f => f.Concepto))
                }).ToList();

                // ComboBox para registrar facturas
                cbPersonas.ItemsSource = _personas.Select(p => new
                {
                    personaId = p.PersonaId,
                    nombreCompleto = $"{p.Nombre} {p.ApellidoPaterno}"
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblErrorNombre.Text = "El nombre es obligatorio.";
                esValido = false;
            }
            else lblErrorNombre.Text = "";

            if (string.IsNullOrWhiteSpace(txtApellidoPaterno.Text))
            {
                lblErrorApellidoPaterno.Text = "El apellido paterno es obligatorio.";
                esValido = false;
            }
            else lblErrorApellidoPaterno.Text = "";

            if (string.IsNullOrWhiteSpace(txtIdentificacion.Text))
            {
                lblErrorIdentificacion.Text = "La identificación es obligatoria.";
                esValido = false;
            }
            else lblErrorIdentificacion.Text = "";

            return esValido;
        }

        private void Campo_TextChanged(object sender, RoutedEventArgs e)
        {
            ValidarCampos();
        }
        private async void RegistrarFactura_Click(object sender, RoutedEventArgs e)
        {
            if (cbPersonas.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un usuario.", "Advertencia");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcionFactura.Text))
            {
                MessageBox.Show("Ingrese una descripción de factura.", "Advertencia");
                return;
            }

            if (!decimal.TryParse(txtMontoFactura.Text, out decimal monto))
            {
                MessageBox.Show("Ingrese un monto válido.", "Error");
                return;
            }

            var factura = new FacturaCreateDto
            {
                PersonaId = (int)cbPersonas.SelectedValue,
                Concepto = txtDescripcionFactura.Text,
                Monto = monto
            };

            await _api.PostAsync<FacturaCreateDto, object>("/api/facturas", factura);

            MessageBox.Show("Factura registrada correctamente.", "Éxito");

            // Limpiar campos
            txtDescripcionFactura.Text = "";
            txtMontoFactura.Text = "";
        }
        private void txtBuscarUsuarios_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = txtBuscarUsuarios.Text.ToLower();
            dgUsuarios.ItemsSource = _personas
                .Where(p => p.Nombre.ToLower().Contains(text))
                .Select(p => new
                {
                    personaId = p.PersonaId,
                    nombre = p.Nombre,
                    apellidoPaterno = p.ApellidoPaterno,
                    identificacion = p.Identificacion
                }).ToList();
        }

        private void txtBuscarFacturas_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = txtBuscarFacturas.Text.ToLower();
            dgUsuariosFacturas.ItemsSource = _personas
                .Where(p => p.Nombre.ToLower().Contains(text))
                .Select(p => new
                {
                    personaId = p.PersonaId,
                    nombre = p.Nombre,
                    facturasDescripcion = string.Join(", ", p.Facturas.Select(f => f.Concepto))
                }).ToList();
        }

        private async void Registrar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Por favor corrige los errores.");
                return;
            }

            var dto = new PersonaDto
            {
                Nombre = txtNombre.Text,
                ApellidoPaterno = txtApellidoPaterno.Text,
                ApellidoMaterno = txtApellidoMaterno.Text,
                Identificacion = txtIdentificacion.Text
            };

            try
            {
                await _api.PostAsync<PersonaDto, object>("/api/personas", dto);
                MessageBox.Show("Usuario registrado correctamente.");

                txtNombre.Text = "";
                txtApellidoPaterno.Text = "";
                txtApellidoMaterno.Text = "";
                txtIdentificacion.Text = "";

                lblErrorNombre.Text = "";
                lblErrorApellidoPaterno.Text = "";
                lblErrorIdentificacion.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
