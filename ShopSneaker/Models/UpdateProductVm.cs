using System.ComponentModel.DataAnnotations;

namespace ShopSneaker.Models
{
    public class UpdateProductVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is null")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is null")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is null")]
        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}
