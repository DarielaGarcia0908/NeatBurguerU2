namespace NeatBurguerU2.Models.ViewModels
{
    public class Menu1VM
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripción { get; set; } = "";
        public double Precio { get; set; }
        public int Anterior { get; set; }
        public int Siguiente { get; set; }
    }
}
