namespace Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public ICollection<Guid>? ProductsIds { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
        public virtual Category? ParentCategory { get; set; }
    }
}
