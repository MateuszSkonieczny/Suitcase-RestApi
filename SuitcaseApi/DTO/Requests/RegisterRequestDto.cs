using System.ComponentModel.DataAnnotations;

namespace SuitcaseApi.DTO.Requests
{
    public class RegisterRequestDto
    {
        [Required]
        [MaxLength(200, ErrorMessage = "Max length is 200")]
        public string Login { get; set; }
        
        [Required]
        [MaxLength(200, ErrorMessage = "Max length is 200")]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}