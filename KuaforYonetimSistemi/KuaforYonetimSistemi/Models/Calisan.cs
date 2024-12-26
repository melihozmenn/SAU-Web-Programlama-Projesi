using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Calisan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Çalışan adı gereklidir.")]
        public string Adi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Uzmanlık alanı gereklidir.")]
        public string UzmanlikAlanlari { get; set; } = string.Empty;

        [Required(ErrorMessage = "Uygunluk saatleri gereklidir.")]
        public string UygunlukSaatleri { get; set; } = string.Empty;

        // Çalışanın bağlı olduğu salon.
        public int SalonId { get; set; }
        public Salon? Salon { get; set; }
    }
}
