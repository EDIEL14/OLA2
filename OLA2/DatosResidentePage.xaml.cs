using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CRUDApp
{
    public partial class DatosResidentePage : Page
    {
        MySQLDatabase db = new MySQLDatabase();

        public DatosResidentePage()
        {
            InitializeComponent();
        }

        private void BuscarResidente_Click(object sender, RoutedEventArgs e)
        {
            int numeroCasa;
            if (int.TryParse(txtNumeroCasa.Text, out numeroCasa))
            {
                MostrarDatosResidente(db.GetResidentePorNumeroCasa(numeroCasa));
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número de casa válido.");
            }
        }

        private void MostrarDatosResidente(Residente residente)
        {
            if (residente != null)
            {
                string resultado = $"Datos del Residente:\n\n" +
                                   $"Nombre: {residente.Nombre} {residente.ApellidoPaterno} {residente.ApellidoMaterno}\n" +
                                   $"Teléfono: {residente.Telefono}\n" +
                                   $"Email: {residente.Email}\n" +
                                   $"Número de Casa: {residente.NumeroCasa}\n" +
                                   $"Tipo: {residente.Tipo}\n" +
                                   $"Fecha de Alta: {residente.FechaAlta}\n\n";

                txtResultados.Text = resultado;
            }
            else
            {
                MessageBox.Show("No se encontró ningún residente con el número de casa proporcionado.");
            }
        }

        private void BuscarVehiculoMascota_Click(object sender, RoutedEventArgs e)
        {
            int numeroCasa;
            if (int.TryParse(txtNumeroCasa.Text, out numeroCasa))
            {
                var residente = db.GetResidentePorNumeroCasa(numeroCasa);
                if (residente != null)
                {
                    string propietario = residente.Nombre; 
                    MostrarDatosVehiculo(db.GetVehiclesByOwner(propietario));
                    MostrarDatosMascota(db.GetPetsByOwner(propietario));
                }
                else
                {
                    MessageBox.Show("No se encontró ningún residente con el número de casa proporcionado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número de casa válido.");
            }
        }

        private void MostrarDatosVehiculo(List<Vehicle> vehiculos)
        {
            if (vehiculos.Any())
            {
                string resultado = "Datos del Vehículo:\n\n";
                foreach (var vehiculo in vehiculos)
                {
                    resultado += $"Marca: {vehiculo.Marca}\n" +
                                  $"Modelo: {vehiculo.Modelo}\n" +
                                  $"Año: {vehiculo.Anio}\n" +
                                  $"Color: {vehiculo.Color}\n" +
                                  $"Placa: {vehiculo.Placa}\n\n";
                }
                txtResultados.Text += resultado;
            }
            else
            {
                txtResultados.Text += "No se encontraron vehículos para este propietario.\n";
            }
        }

        private void MostrarDatosMascota(List<Pet> mascotas)
        {
            if (mascotas.Any())
            {
                string resultado = "Datos de la Mascota:\n";
                foreach (var mascota in mascotas)
                {
                    resultado += $"Nombre: {mascota.Nombre}\n" +
                                  $"Especie: {mascota.Especie}\n" +
                                  $"Raza: {mascota.Raza}\n" +
                                  $"Edad: {mascota.Edad}\n";
                }
                txtResultados.Text += resultado;
            }
            else
            {
                txtResultados.Text += "No se encontraron mascotas para este propietario.\n";
            }
        }

        private void LimpiarDatos()
        {
            // Limpiar campo de entrada para número de casa
            txtNumeroCasa.Clear();

            // Restaurar resultados
            txtResultados.Text = "";
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarDatos();
        }
    }
}
