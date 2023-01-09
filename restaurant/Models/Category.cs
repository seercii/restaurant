using System.ComponentModel.DataAnnotations;

namespace restaurant.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]//isim kısmı boş geçilmemesi için attirube tanımladık

        public string Name { get; set; }
    }
}
