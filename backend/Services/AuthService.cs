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
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            this._userRepo = userRepository;
            this._jwtService = jwtService;
        }

        public async Task<ApiResponse<AuthResponse>> Login(LoginRequest request)
        {
            var response = new ApiResponse<AuthResponse>();

            try
            {
                if (request == null)
                {
                    response.Status = 400;
                    response.Message = "Request cannot be null";
                    return response;
                }

                if (string.IsNullOrWhiteSpace(request.Email) ||
                   string.IsNullOrWhiteSpace(request.Password))
                {
                    response.Status = 400;
                    response.Message = "Email and password are required";
                    return response;
                }

                var user = await this._userRepo.GetUserByEmailAsync(request.Email);
                if(user == null)
                {
                    response.Status = 401;
                    response.Message = "Invalid email or password";
                    return response;
                }

                var isPasswordMatch = BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword);
                if (!isPasswordMatch)
                {
                    response.Status = 401;
                    response.Message = "Invalid email or password";
                    return response;
                }

                var token = this._jwtService.GenerateToken(user);

                var authResponse = new AuthResponse
                {
                    UserId = user.UserId,
                    Name = user.FullName,
                    Email = user.Email,
                    CreatedOn = user.CreatedOn,
                    Token = token,
                    IsActive = user.IsActive
                };

                response.Status = 200;
                response.Message = "User login successful";
                response.Data = authResponse;
                return response;
            }
            catch (Exception)
            {
                response.Status = 500;
                response.Message = "Unexpected error occured while user login";
                return response;
            }
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
                    response.Message = "Name, email and password are required";
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
                    CreatedOn = DateTime.Now,
                    IsActive = true
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
