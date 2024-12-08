using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Domain
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        
    }
}