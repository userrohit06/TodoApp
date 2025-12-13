using backend.DTO.Auth;
using backend.DTO.Common;

namespace backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponse>> Register(RegisterRequest request);
    }
}
