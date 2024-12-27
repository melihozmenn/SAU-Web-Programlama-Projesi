using System.ComponentModel.DataAnnotations;

namespace KuaforYonetimSistemi.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tarih gereklidir")]
        public DateTime Tarih { get; set; }

        [Required(ErrorMessage = "İşlem seçilmelidir")]
        public int IslemId { get; set; }
        public Islem? Islem { get; set; }

        public int CalisanId { get; set; }
        public Calisan? Calisan { get; set; }

        [Required(ErrorMessage = "Müşteri adı gereklidir")]
        public string MusteriAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Müşteri telefonu gereklidir")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string MusteriTelefonu { get; set; } = string.Empty;
       
        public bool Onayli { get; set; } = false;  // Başlangıçta onaysız
    }
}