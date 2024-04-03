public class Residente
{
    public int Id_Residente { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string NumeroCasa { get; set; } // Definir NumeroCasa como tipo string
    public string Tipo { get; set; }
    public DateTime FechaAlta { get; set; }
}
