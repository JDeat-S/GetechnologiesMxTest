using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly ApiClient _api;

        public MainWindow()
        {
            InitializeComponent();
            _api = new ApiClient();
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

        private async void Registrar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Por favor corrige los errores.");
                return;
            }

            var dto = new PersonaCreateDto
            {
                Nombre = txtNombre.Text,
                ApellidoPaterno = txtApellidoPaterno.Text,
                ApellidoMaterno = txtApellidoMaterno.Text,
                Identificacion = txtIdentificacion.Text
            };

            try
            {
                await _api.PostAsync<PersonaCreateDto, object>("/api/personas", dto);
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
}
