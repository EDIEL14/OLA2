using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CRUDApp
{
    public class MySQLDatabase
    {
        private readonly string connectionString = "Server=localhost;Database=residencia;Uid=root;Pwd=;";

        public List<Residente> GetResidentes()
        {
            List<Residente> residentes = new List<Residente>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Residentes";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Residente residente = new Residente
                        {
                            Id_Residente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            ApellidoPaterno = reader.GetString(2),
                            ApellidoMaterno = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            NumeroCasa = reader.GetString(6), // Asegúrate de que NumeroCasa es de tipo string en tu clase Residente
                            Tipo = reader.GetString(7),
                            FechaAlta = reader.GetDateTime(8) // Obtener la fecha de alta del residente
                        };
                        residentes.Add(residente);
                    }
                }
            }
            return residentes;
        }

        public void InsertResidente(string nombre, string apellidoPaterno, string apellidoMaterno, string telefono, string email, string numeroCasa, string tipo, DateTime fechaAlta)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Residentes (Nombre, ApellidoPaterno, ApellidoMaterno, Telefono, Email, NumeroCasa, Tipo, FechaAlta) VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Telefono, @Email, @NumeroCasa, @Tipo, @FechaAlta)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@ApellidoPaterno", apellidoPaterno);
                command.Parameters.AddWithValue("@ApellidoMaterno", apellidoMaterno);
                command.Parameters.AddWithValue("@Telefono", telefono);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                command.Parameters.AddWithValue("@Tipo", tipo);
                command.Parameters.AddWithValue("@FechaAlta", fechaAlta);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateResidente(int idResidente, string nombre, string apellidoPaterno, string apellidoMaterno, string telefono, string email, string numeroCasa, string tipo)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Residentes SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, Telefono = @Telefono, Email = @Email, NumeroCasa = @NumeroCasa, Tipo = @Tipo WHERE Id_Residente = @Id_Residente";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@ApellidoPaterno", apellidoPaterno);
                command.Parameters.AddWithValue("@ApellidoMaterno", apellidoMaterno);
                command.Parameters.AddWithValue("@Telefono", telefono);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                command.Parameters.AddWithValue("@Tipo", tipo);
                command.Parameters.AddWithValue("@Id_Residente", idResidente);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteResidente(int idResidente)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Residentes WHERE Id_Residente = @Id_Residente";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id_Residente", idResidente);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT Id_Vehiculo, Marca, Modelo, Año, Color, Placa, NumeroCasa, Propietario FROM Vehiculos";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle
                        {
                            Id = reader.GetInt32(0),
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Anio = reader.GetInt32(3),
                            Color = reader.GetString(4),
                            Placa = reader.GetString(5),
                            NumeroCasa = reader.IsDBNull(6) ? "0" : reader.GetInt32(6).ToString(), // Convertir entero a cadena
                            Propietario = reader.GetString(7)
                        };
                        vehicles.Add(vehicle);
                    }
                }
            }
            return vehicles;
        }

        public void InsertVehicle(string marca, string modelo, int anio, string color, string placa, string propietario, string numeroCasa)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Vehiculos (Marca, Modelo, Año, Color, Placa, Propietario, NumeroCasa) VALUES (@Marca, @Modelo, @Año, @Color, @Placa, @Propietario, @NumeroCasa)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Marca", marca);
                command.Parameters.AddWithValue("@Modelo", modelo);
                command.Parameters.AddWithValue("@Año", anio);
                command.Parameters.AddWithValue("@Color", color);
                command.Parameters.AddWithValue("@Placa", placa);
                command.Parameters.AddWithValue("@Propietario", propietario);
                command.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateVehicle(int idVehiculo, string marca, string modelo, int anio, string color, string placa, string propietario, string numeroCasa)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Vehiculos SET Marca = @Marca, Modelo = @Modelo, Año = @Anio, Color = @Color, Placa = @Placa, Propietario = @Propietario, NumeroCasa = @NumeroCasa WHERE Id_Vehiculo = @IdVehiculo";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Marca", marca);
                command.Parameters.AddWithValue("@Modelo", modelo);
                command.Parameters.AddWithValue("@Anio", anio);
                command.Parameters.AddWithValue("@Color", color);
                command.Parameters.AddWithValue("@Placa", placa);
                command.Parameters.AddWithValue("@Propietario", propietario);
                command.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                command.Parameters.AddWithValue("@IdVehiculo", idVehiculo);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteVehicle(int idVehiculo)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Vehiculos WHERE Id_Vehiculo = @IdVehiculo"; // Usar el nombre de columna correcto
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdVehiculo", idVehiculo);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Pet> GetPets()
        {
            List<Pet> pets = new List<Pet>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Mascotas";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Especie = reader.GetString(2),
                            Raza = reader.GetString(3),
                            Edad = reader.GetInt32(4),
                            NumeroCasa = reader.GetInt32(5).ToString(), // Convertir el entero a cadena si es necesario
                            Propietario = reader.GetString(6), // Se asume que Propietario es una cadena en la base de datos
                        };
                        pets.Add(pet);
                    }
                }
            }
            return pets;
        }

        public void InsertPet(string nombre, string especie, string raza, int edad, string propietario, string numeroCasa)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO Mascotas (Nombre, Especie, Raza, Edad, Propietario, NumeroCasa) VALUES (@Nombre, @Especie, @Raza, @Edad, @Propietario, @NumeroCasa)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Especie", especie);
                command.Parameters.AddWithValue("@Raza", raza);
                command.Parameters.AddWithValue("@Edad", edad);
                command.Parameters.AddWithValue("@Propietario", propietario);
                command.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePet(int idMascota, string nombre, string especie, string raza, int edad, string propietario, string numeroCasa)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE Mascotas SET Nombre = @Nombre, Especie = @Especie, Raza = @Raza, Edad = @Edad, Propietario = @Propietario, NumeroCasa = @NumeroCasa WHERE Id_Mascota = @IdMascota";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Especie", especie);
                command.Parameters.AddWithValue("@Raza", raza);
                command.Parameters.AddWithValue("@Edad", edad);
                command.Parameters.AddWithValue("@Propietario", propietario);
                command.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                command.Parameters.AddWithValue("@IdMascota", idMascota);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeletePet(int idMascota)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Mascotas WHERE Id_Mascota = @IdMascota";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdMascota", idMascota);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Pet> GetPetsByOwnerId(int ownerId)
        {
            List<Pet> pets = new List<Pet>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Mascotas WHERE Propietario = @OwnerId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OwnerId", ownerId);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Especie = reader.GetString(2),
                            Raza = reader.GetString(3),
                            Edad = reader.GetInt32(4),
                            Propietario = reader.GetInt32(5).ToString(), // Convertir explícitamente el valor int a string
                        };
                        pets.Add(pet);
                    }
                }
            }
            return pets;
        }

        public List<Vehicle> GetVehiclesByOwnerId(int ownerId)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Vehiculos WHERE Propietario = @OwnerId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@OwnerId", ownerId);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle
                        {
                            Id = reader.GetInt32(0),
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Anio = reader.GetInt32(3),
                            Color = reader.GetString(4),
                            Placa = reader.GetString(5),
                            Propietario = reader.GetInt32(6).ToString() // Convertir el ID del propietario a string
                        };
                        vehicles.Add(vehicle);
                    }
                }
            }
            return vehicles;
        }

        public Residente GetResidentePorNombreCompleto(string nombreCompleto)
        {
            Residente residente = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Residentes WHERE CONCAT(Nombre, ' ', ApellidoPaterno, ' ', ApellidoMaterno) = @NombreCompleto";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        residente = new Residente
                        {
                            Id_Residente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            ApellidoPaterno = reader.GetString(2),
                            ApellidoMaterno = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            NumeroCasa = reader.GetString(6),
                            Tipo = reader.GetString(7),
                            FechaAlta = reader.GetDateTime(8)
                        };
                    }
                }
            }
            return residente;
        }

        public Residente GetResidentePorNumeroCasa(int numeroCasa)
        {
            Residente residente = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Residentes WHERE NumeroCasa = @NumeroCasa";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        residente = new Residente
                        {
                            Id_Residente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            ApellidoPaterno = reader.GetString(2),
                            ApellidoMaterno = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            NumeroCasa = reader.GetString(6),
                            Tipo = reader.GetString(7),
                            FechaAlta = reader.GetDateTime(8)
                        };
                    }
                }
            }
            return residente;
        }

        public Residente GetResidentesPorNumeroTelefono(string numeroTelefono)
        {
            Residente residente = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Residentes WHERE Telefono = @NumeroTelefono";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroTelefono", numeroTelefono);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        residente = new Residente
                        {
                            Id_Residente = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            ApellidoPaterno = reader.GetString(2),
                            ApellidoMaterno = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            NumeroCasa = reader.GetString(6),
                            Tipo = reader.GetString(7),
                            FechaAlta = reader.GetDateTime(8)
                        };
                    }
                }
            }
            return residente;
        }
        public List<Vehicle> GetVehiclesByOwner(string owner)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Vehiculos WHERE Propietario = @Owner";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Owner", owner);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Vehicle vehicle = new Vehicle
                        {
                            Id = reader.GetInt32(0),
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Anio = reader.GetInt32(3),
                            Color = reader.GetString(4),
                            Placa = reader.GetString(5),
                            NumeroCasa = reader.GetInt32(6).ToString(), 
                            Propietario = reader.GetString(7) 
                        };
                        vehicles.Add(vehicle);
                    }
                }
            }
            return vehicles;
        }

        public List<Pet> GetPetsByOwner(string owner)
        {
            List<Pet> pets = new List<Pet>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Mascotas WHERE Propietario = @Owner";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Owner", owner);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Especie = reader.GetString(2),
                            Raza = reader.GetString(3),
                            Edad = reader.GetInt32(4),
                            NumeroCasa = reader.GetInt32(5).ToString(), // Convertir a cadena
                            Propietario = reader.GetString(6) // Ajustar si es necesario según tu implementación real
                        };
                        pets.Add(pet);
                    }
                }
            }
            return pets;
        }
    }
}
