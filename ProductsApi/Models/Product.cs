namespace ProductsAPI.Models
{
    public class Product
    {
        public Guid Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public float Price {  get; set; }
        
        public DateTime CreatedAt { get; private set; }

        public Product()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

    }
}
