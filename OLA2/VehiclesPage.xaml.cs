using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            if (string.IsNullOrWhiteSpace(txtMarca.Text) ||
                string.IsNullOrWhiteSpace(txtModelo.Text) ||
                string.IsNullOrWhiteSpace(txtAnio.Text) ||
                string.IsNullOrWhiteSpace(txtColor.Text) ||
                string.IsNullOrWhiteSpace(txtPlaca.Text) ||
                string.IsNullOrWhiteSpace(txtPropietario.Text) ||
                string.IsNullOrWhiteSpace(txtNumeroCasa.Text))
            {
                MessageBox.Show("Es obligatorio llenar todos los campos", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                int anio;
                if (int.TryParse(txtAnio.Text, out anio))
                {
                    string color = txtColor.Text;
                    string placa = txtPlaca.Text;
                    string propietario = txtPropietario.Text;
                    int numeroCasa;
                    if (int.TryParse(txtNumeroCasa.Text, out numeroCasa))
                    {
                        db.InsertVehicle(marca, modelo, anio, color, placa, propietario, numeroCasa.ToString());
                        dgVehiculos.ItemsSource = db.GetVehicles();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un número de casa válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un año válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ActualizarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            Vehicle selectedVehicle = dgVehiculos.SelectedItem as Vehicle;
            if (selectedVehicle != null)
            {
                string marca = txtMarca.Text;
                string modelo = txtModelo.Text;
                int anio;
                if (int.TryParse(txtAnio.Text, out anio))
                {
                    string color = txtColor.Text;
                    string placa = txtPlaca.Text;
                    string propietario = txtPropietario.Text;
                    int numeroCasa;
                    if (int.TryParse(txtNumeroCasa.Text, out numeroCasa))
                    {
                        db.UpdateVehicle(selectedVehicle.Id, marca, modelo, anio, color, placa, propietario, numeroCasa.ToString());
                        dgVehiculos.ItemsSource = db.GetVehicles();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un número de casa válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un año válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
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

        private void LimpiarCampos()
        {
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtAnio.Text = "";
            txtColor.Text = "";
            txtPlaca.Text = "";
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
