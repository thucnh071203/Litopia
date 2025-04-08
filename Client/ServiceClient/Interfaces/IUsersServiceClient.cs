using Shared.DTOs;

namespace Client.ServiceClient.Interfaces
{
    public interface IUsersServiceClient
    {
        Task<List<UserDTO>> GetUsersODataAsync(string odataQuery);
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto); // Thêm phương thức Login
        Task<bool> RegisterAsync(RegisterDTO registerDto);
    }
}
