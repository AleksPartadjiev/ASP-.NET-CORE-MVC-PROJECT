namespace WebApp.Models.Entities
{
    public class Products
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Sizes { get; set; }
    }
}
