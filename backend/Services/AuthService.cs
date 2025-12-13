using backend.DTO.Auth;
using backend.DTO.Common;
using backend.Models;
using backend.Repository.Interface;
using backend.Services.Interfaces;

namespace backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepository)
        {
            this._userRepo = userRepository;
        }

        public async Task<ApiResponse<AuthResponse>> Register(RegisterRequest request)
        {
            var response = new ApiResponse<AuthResponse>();

            try
            {
                if(request == null)
                {
                    response.Status = 400;
                    response.Message = "Request cannot be null";
                    return response;
                }

                if(string.IsNullOrWhiteSpace(request.Name) ||
                   string.IsNullOrWhiteSpace(request.Email) ||
                   string.IsNullOrWhiteSpace(request.Password))
                {
                    response.Status = 400;
                    response.Message = "Name, email are password are required";
                    return response;
                }

                // check if user exists
                var user = await this._userRepo.GetUserByEmailAsync(request.Email);
                if(user != null)
                {
                    response.Status = 409;
                    response.Message = "User already exists";
                    return response;
                }

                // hash password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

                // create new user
                var newUser = new User
                {
                    FullName = request.Name,
                    Email = request.Email,
                    HashedPassword = hashedPassword,
                    CreatedOn = DateTime.Now
                };

                await this._userRepo.AddUserAsync(newUser);
                await this._userRepo.SaveChangesAsync();

                // convert user to AuthResponse
                var authRespone = new AuthResponse
                {
                    UserId = newUser.UserId,
                    Name = newUser.FullName,
                    Email = newUser.Email,
                    Token = ""
                };

                response.Status = 201;
                response.Message = "User registered successfully";
                response.Data = authRespone;
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Unexpected error occured registering user";
                return response;
            }
        }
    }
}
