namespace ShopSneaker.Models;

public class ProductVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    
    public int Rating { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public string ThumbPath { get; set; }
    
    public int CategoryId { get; set; }

    public string Category { get; set; }

    public IFormFile Files { get; set; }
}