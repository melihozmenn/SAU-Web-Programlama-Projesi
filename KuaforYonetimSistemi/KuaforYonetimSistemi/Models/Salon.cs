namespace KuaforYonetimSistemi.Models
{
    public class Salon
    {
        public int Id { get; set; }
        public required string Adi { get; set; }
        public required string Adres { get; set; }
        public required string CalismaSaatleri { get; set; }

        public required ICollection<Calisan> Calisans { get; set; }
    }
}
