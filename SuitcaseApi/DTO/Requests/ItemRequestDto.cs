using System.ComponentModel.DataAnnotations;

namespace SuitcaseApi.DTO.Requests
{
    public class ItemRequestDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Max 200 symbols")]
        public string Name { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public bool IsPacked { get; set; }
    }
}