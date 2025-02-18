namespace EcomCore.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }
        public string? ImageUrl { get; set; }
    }
}
