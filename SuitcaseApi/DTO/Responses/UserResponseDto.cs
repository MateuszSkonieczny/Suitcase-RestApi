using SuitcaseApi.Models;

namespace SuitcaseApi.DTO.Responses
{
    public class UserResponseDto
    {
        public User User { get; set; }
        public string RefreshToken { get; set; }
    }
}