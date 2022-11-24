using il_mio_primo_blog.Validator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo nome è obbligatorio")]
        [StringLength(50, ErrorMessage ="Il nome può essere massimo 50 caratteri")]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Il campo descrizione è obbligatorio")]
        [StringLength(300, ErrorMessage = "La descrizione può essere massimo 300 caratteri")]
        [MinFiveWords]
        public string Description { get; set; }
        [Required(ErrorMessage = "Il campo immagine è obbligatorio")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Il campo prezzo è obbligatorio")]
        [Range(0.0, 1000.0, ErrorMessage = "Il prezzo non può essere negativo")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public Pizza()
        {
        }
        public Pizza(string name, string description, string image, double price)
        {
            Name = name;
            Description = description;
            Image = image;
            Price = price;
        }
    }
}