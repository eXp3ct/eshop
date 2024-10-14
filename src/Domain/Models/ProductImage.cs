namespace Domain.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Extension { get; set; }
        public virtual Product Product { get; set; }
    }
}
