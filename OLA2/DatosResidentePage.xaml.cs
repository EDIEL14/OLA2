using System;
using System.Collections.Generic;
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
            if (cbNombreCompleto.IsChecked == true)
            {
                string nombreCompleto = txtNombreCompleto.Text;
                MostrarDatosResidente(db.GetResidentePorNombreCompleto(nombreCompleto));
            }
            else if (cbNumeroCasa.IsChecked == true)
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
            else if (cbNumeroTelefono.IsChecked == true)
            {
                string numeroTelefono = txtNumeroTelefono.Text;
                MostrarDatosResidente(db.GetResidentesPorNumeroTelefono(numeroTelefono));
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una opción de búsqueda.");
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
                MessageBox.Show("No se encontró ningún residente con los criterios de búsqueda proporcionados.");
            }
        }

        private void BuscarVehiculoMascota_Click(object sender, RoutedEventArgs e)
        {
            string propietario = txtPropietario.Text;
            MostrarDatosVehiculo(db.GetVehiclesByOwner(propietario));
            MostrarDatosMascota(db.GetPetsByOwner(propietario));
        }

        private void MostrarDatosVehiculo(List<Vehicle> vehiculos)
        {
            if (vehiculos.Any())
            {
                string resultado = "Datos del Vehículo:\n";
                foreach (var vehiculo in vehiculos)
                {
                    resultado += $"Marca: {vehiculo.Marca}, " +
                                  $"Modelo: {vehiculo.Modelo}, " +
                                  $"Año: {vehiculo.Anio}, " +
                                  $"Color: {vehiculo.Color}, " +
                                  $"Placa: {vehiculo.Placa}\n";
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
                    resultado += $"Nombre: {mascota.Nombre}, " +
                                  $"Especie: {mascota.Especie}, " +
                                  $"Raza: {mascota.Raza}, " +
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
            // Limpiar campos de entrada para residente
            txtNombreCompleto.Clear();
            txtNumeroCasa.Clear();
            txtNumeroTelefono.Clear();

            // Limpiar campos de entrada para vehículo y mascota
            txtPropietario.Clear();

            // Restaurar resultados para todos los datos
            txtResultados.Text = "";
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarDatos();
        }
    }
}
