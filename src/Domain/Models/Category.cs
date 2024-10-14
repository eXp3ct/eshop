namespace Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public IList<Guid>? ProductsIds { get; set; } 

        public virtual IReadOnlyCollection<Product>? Products { get; set; }
        public virtual Category? ParentCategory { get; set; }
    }
}
