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
            if (CamposLlenos())
            {
                string nombre = txtNombre.Text;
                string apellidoPaterno = txtApellidoPaterno.Text;
                string apellidoMaterno = txtApellidoMaterno.Text;
                string telefono = txtTelefono.Text;
                string email = txtEmail.Text;
                string numeroCasa = txtNumeroCasa.Text;
                string tipo = (cmbTipo.SelectedItem as ComboBoxItem)?.Content.ToString();
                DateTime fechaAlta = DateTime.Now; // Fecha y hora actual

                db.InsertResidente(nombre, apellidoPaterno, apellidoMaterno, telefono, email, numeroCasa, tipo, fechaAlta);
                dgResidents.ItemsSource = db.GetResidentes();
            }
            else
            {
                MessageBox.Show("Es obligatorio llenar todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CamposLlenos()
        {
            return !string.IsNullOrEmpty(txtNombre.Text) &&
                   !string.IsNullOrEmpty(txtApellidoPaterno.Text) &&
                   !string.IsNullOrEmpty(txtApellidoMaterno.Text) &&
                   !string.IsNullOrEmpty(txtTelefono.Text) &&
                   !string.IsNullOrEmpty(txtEmail.Text) &&
                   !string.IsNullOrEmpty(txtNumeroCasa.Text) &&
                   cmbTipo.SelectedItem != null; 
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
                string tipo = (cmbTipo.SelectedItem as ComboBoxItem)?.Content.ToString();

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

        private void LimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtNumeroCasa.Text = "";
            cmbTipo.SelectedItem = null; 
        }
    }
}
