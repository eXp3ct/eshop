using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Price { get; set; }
        public IList<Guid> CategoriesIds { get; set; }
        public Guid ImageId { get; set; }

        public virtual IList<Category> Categories { get; set; }
        public virtual ProductImage Image { get; set; }
    }
}
