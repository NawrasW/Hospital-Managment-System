using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Domain
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
    }
}