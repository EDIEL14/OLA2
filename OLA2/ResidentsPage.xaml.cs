using System;
using System.Windows;
using System.Windows.Controls;

namespace CRUDApp
{
    public partial class ResidentsPage : Page
    {
        MySQLDatabase db = new MySQLDatabase();

        public ResidentsPage()
        {
            InitializeComponent();
            dgResidents.ItemsSource = db.GetResidentes();
        }

        private void AgregarResidente_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellidoPaterno = txtApellidoPaterno.Text;
            string apellidoMaterno = txtApellidoMaterno.Text;
            string telefono = txtTelefono.Text;
            string email = txtEmail.Text;
            string numeroCasa = txtNumeroCasa.Text;
            string tipo = txtTipo.Text;
            DateTime fechaAlta = DateTime.Now; // Fecha y hora actual

            db.InsertResidente(nombre, apellidoPaterno, apellidoMaterno, telefono, email, numeroCasa, tipo, fechaAlta);
            dgResidents.ItemsSource = db.GetResidentes();
        }

        private void ActualizarResidente_Click(object sender, RoutedEventArgs e)
        {
            Residente selectedResidente = dgResidents.SelectedItem as Residente;
            if (selectedResidente != null)
            {
                string nombre = txtNombre.Text;
                string apellidoPaterno = txtApellidoPaterno.Text;
                string apellidoMaterno = txtApellidoMaterno.Text;
                string telefono = txtTelefono.Text;
                string email = txtEmail.Text;
                string numeroCasa = txtNumeroCasa.Text;
                string tipo = txtTipo.Text;

                db.UpdateResidente(selectedResidente.Id_Residente, nombre, apellidoPaterno, apellidoMaterno, telefono, email, numeroCasa, tipo);
                dgResidents.ItemsSource = db.GetResidentes();
            }
        }

        private void EliminarResidente_Click(object sender, RoutedEventArgs e)
        {
            Residente selectedResidente = dgResidents.SelectedItem as Residente;
            if (selectedResidente != null)
            {
                db.DeleteResidente(selectedResidente.Id_Residente);
                dgResidents.ItemsSource = db.GetResidentes();
            }
        }
    }
}
