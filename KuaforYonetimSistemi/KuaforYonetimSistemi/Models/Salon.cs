using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Salon
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Salon adı gereklidir")]
        public string Adi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres gereklidir")]
        public string Adres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Çalışma saatleri gereklidir")]
        public string CalismaSaatleri { get; set; } = string.Empty;

        public ICollection<Calisan> Calisans { get; set; } = new List<Calisan>();
    }
}