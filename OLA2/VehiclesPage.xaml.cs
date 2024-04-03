using System.Windows;
using System.Windows.Controls;

namespace CRUDApp
{
    public partial class VehiclesPage : Page
    {
        MySQLDatabase db = new MySQLDatabase();

        public VehiclesPage()
        {
            InitializeComponent();
            dgVehiculos.ItemsSource = db.GetVehicles();
        }

        private void AgregarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            int anio = int.Parse(txtAnio.Text);
            string color = txtColor.Text;
            string placa = txtPlaca.Text;
            string propietario = txtPropietario.Text; // Agregamos la obtención del propietario desde el TextBox
            db.InsertVehicle(marca, modelo, anio, color, placa, propietario);
            dgVehiculos.ItemsSource = db.GetVehicles();
        }

        private void ActualizarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            Vehicle selectedVehicle = dgVehiculos.SelectedItem as Vehicle;
            if (selectedVehicle != null)
            {
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                int anio = int.Parse(txtAnio.Text);
                string color = txtColor.Text;
                string placa = txtPlaca.Text;
                string propietario = txtPropietario.Text; // Agregamos la obtención del propietario desde el TextBox
                db.UpdateVehicle(selectedVehicle.Id, marca, modelo, anio, color, placa, propietario);
                dgVehiculos.ItemsSource = db.GetVehicles();
            }
        }

        private void EliminarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            Vehicle selectedVehicle = dgVehiculos.SelectedItem as Vehicle;
            if (selectedVehicle != null)
            {
                db.DeleteVehicle(selectedVehicle.Id);
                dgVehiculos.ItemsSource = db.GetVehicles();
            }
        }
    }
}
