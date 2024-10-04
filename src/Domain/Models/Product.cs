namespace Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Article { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Guid> CategoriesIds { get; set; }  
        public Guid ImageId { get; set; }

        public virtual ICollection<Category> Categories { get; set; } 
        public virtual ProductImage Image { get; set; }
    }
}
