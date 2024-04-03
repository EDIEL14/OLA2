using System.Windows;
using System.Windows.Controls;

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
            string nombre = txtNombre.Text;
            string especie = txtEspecie.Text;
            string raza = txtRaza.Text;
            int edad = int.Parse(txtEdad.Text);
            string propietario = txtPropietario.Text;
            db.InsertPet(nombre, especie, raza, edad, propietario);
            dgMascotas.ItemsSource = db.GetPets();
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
                db.UpdatePet(selectedPet.Id, nombre, especie, raza, edad, propietario);
                dgMascotas.ItemsSource = db.GetPets();
            }
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
    }
}
