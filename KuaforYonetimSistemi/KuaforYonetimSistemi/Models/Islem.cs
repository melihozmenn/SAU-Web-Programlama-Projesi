    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KuaforYonetimSistemi.Models
{
    public class Islem
    {
        public int Id { get; set; }
        public required string Adi { get; set; }
        public int Sure { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Ucret { get; set; }

    }
}
