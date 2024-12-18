﻿namespace Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public IList<Guid>? ProductsIds { get; set; } 
        public IList<Guid>? ChildrenCategoriesIds { get; set; }

        public virtual IList<Product>? Products { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual IList<Category>? ChildrenCategories { get; set; }
    }
}
