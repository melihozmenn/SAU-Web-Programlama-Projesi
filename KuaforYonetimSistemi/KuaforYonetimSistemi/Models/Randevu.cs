namespace KuaforYonetimSistemi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }

        public int IslemId { get; set; }
        public required Islem Islem { get; set; }

        public int CalisanId { get; set; }
        public required Calisan Calisan { get; set; }

    }
}
