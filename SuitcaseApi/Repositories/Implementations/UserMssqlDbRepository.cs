using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuitcaseApi.DTO.Requests;
using SuitcaseApi.DTO.Responses;
using SuitcaseApi.Models;
using SuitcaseApi.Repositories.Interfaces;

namespace SuitcaseApi.Repositories.Implementations
{
    public class UserMssqlDbRepository: IUserDbRepository
    {
        private readonly SuitcaseContext _context;

        public UserMssqlDbRepository(SuitcaseContext context)
        {
            _context = context;
        }
        
        public async Task<UserResponseDto> GetUserFromDb(LoginRequestDto loginRequestDto)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(e => e.Login == loginRequestDto.Login);
            if (user is null)
                return null;

            var samePass = new PasswordHasher<LoginRequestDto>().VerifyHashedPassword(loginRequestDto,
                user.HashedPassword, loginRequestDto.Password);
            if (samePass == PasswordVerificationResult.Failed)
                return null;

            var guid = Guid.NewGuid();
            user.RefreshToken = guid;
            user.TokenExpire = DateTime.Now.AddMinutes(30);
            await _context.SaveChangesAsync();

            return new UserResponseDto {User = user, RefreshToken = guid.ToString()};
        }

        public async Task<UserResponseDto> RefreshToken(string refreshToken)
        {
            var user = await _context.User
                .SingleOrDefaultAsync(e => e.RefreshToken.ToString() == refreshToken);
            if (user is null)
                return null;
            
            if (user.TokenExpire < DateTime.Now)
                return new UserResponseDto
                {
                    RefreshToken = null
                };
                
            
            
            var guid = Guid.NewGuid();
            user.RefreshToken = guid;
            user.TokenExpire = DateTime.Now.AddMinutes(30);
            await _context.SaveChangesAsync();

            return new UserResponseDto {User = user, RefreshToken = guid.ToString()};
        }

        public async Task<bool> RegisterUser(RegisterRequestDto registerRequestDto)
        {
            var sameData = await _context.User
                .SingleOrDefaultAsync(e => e.Login == registerRequestDto.Login && e.Email == registerRequestDto.Email);
            if (sameData is not null)
                return false;

            var user = new User();
            user.Login = registerRequestDto.Login;
            user.Email = registerRequestDto.Email;
            user.HashedPassword = new PasswordHasher<User>().HashPassword(user, registerRequestDto.Password);

            await _context.AddAsync(user);
            return true;
        }
    }
}