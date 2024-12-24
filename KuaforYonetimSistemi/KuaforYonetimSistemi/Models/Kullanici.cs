namespace KuaforYonetimSistemi.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        public required string KullaniciAdi { get; set; }
        public required string Sifre { get; set; }
        public required string Rol { get; set; }
    }
}
