namespace KuaforYonetimSistemi.Models
{
    public class Calisan
    {
        public int Id { get; set; }
        public required string Adi { get; set; }
        public required string UzmanlikAlanlari { get; set; }
        public required string UygunlukSaatleri { get; set; }

        public int SalonId { get; set; }
        public required Salon Salon { get; set; }
    }
}
