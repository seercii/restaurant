using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class İletisimim
    {
        [Key]
        public int id { get; set; }
        public string EMail { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }

    }
}
