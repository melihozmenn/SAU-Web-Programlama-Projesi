using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Salon
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Salon adı gereklidir")]
        public required string Adi { get; set; }
        [Required(ErrorMessage = "Adres gereklidir")]
        public required string Adres { get; set; }
        [Required(ErrorMessage = "Çalışma saatleri gereklidir")]
        public required string CalismaSaatleri { get; set; }

        public required ICollection<Calisan> Calisans { get; set; }
    }
}
