using System.Threading.Tasks;
using SuitcaseApi.DTO.Requests;
using SuitcaseApi.DTO.Responses;

namespace SuitcaseApi.Repositories.Interfaces
{
    public interface IUserDbRepository
    {
        Task<UserResponseDto> GetUserFromDb(LoginRequestDto loginRequestDto);
        Task<UserResponseDto> RefreshToken(string refreshToken);
        Task<bool> RegisterUser(RegisterRequestDto registerRequestDto);
    }
}