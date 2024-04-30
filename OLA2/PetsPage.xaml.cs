using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRUDApp
{
    public partial class PetsPage : Page
    {
        MySQLDatabase db = new MySQLDatabase();

        public PetsPage()
        {
            InitializeComponent();
            dgMascotas.ItemsSource = db.GetPets();
        }

        private void AgregarMascota_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtEspecie.Text) ||
                string.IsNullOrWhiteSpace(txtRaza.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text) ||
                string.IsNullOrWhiteSpace(txtPropietario.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCasa.Text))
            {
                MessageBox.Show("Es obligatorio llenar todos los campos", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string nombre = txtNombre.Text;
                string especie = txtEspecie.Text;
                string raza = txtRaza.Text;
                int edad = int.Parse(txtEdad.Text);
                string propietario = txtPropietario.Text;
                int numeroCasa = int.Parse(txtNumeroCasa.Text); // Convertir a entero
                db.InsertPet(nombre, especie, raza, edad, propietario, numeroCasa.ToString()); // Convertir a string para la base de datos
                dgMascotas.ItemsSource = db.GetPets();
            }
        }

        private void ActualizarMascota_Click(object sender, RoutedEventArgs e)
        {
            Pet selectedPet = dgMascotas.SelectedItem as Pet;
            if (selectedPet != null)
            {
                string nombre = txtNombre.Text;
                string especie = txtEspecie.Text;
                string raza = txtRaza.Text;
                int edad = int.Parse(txtEdad.Text);
                string propietario = txtPropietario.Text;
                int numeroCasa = int.Parse(txtNumeroCasa.Text); // Convertir a entero
                db.UpdatePet(selectedPet.Id, nombre, especie, raza, edad, propietario, numeroCasa.ToString()); // Convertir a string para la base de datos
                dgMascotas.ItemsSource = db.GetPets();
            }
        }

        private void LimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void EliminarMascota_Click(object sender, RoutedEventArgs e)
        {
            Pet selectedPet = dgMascotas.SelectedItem as Pet;
            if (selectedPet != null)
            {
                db.DeletePet(selectedPet.Id);
                dgMascotas.ItemsSource = db.GetPets();
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtEspecie.Text = "";
            txtRaza.Text = "";
            txtEdad.Text = "";
            txtPropietario.Text = "";
            txtNumeroCasa.Text = "";
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Verifica si el texto ingresado no es un número
            if (!int.TryParse(e.Text, out _))
            {
                // Cancela el evento de entrada
                e.Handled = true;
                // Muestra un mensaje de alerta
                MessageBox.Show("Por favor, ingrese solo números.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
