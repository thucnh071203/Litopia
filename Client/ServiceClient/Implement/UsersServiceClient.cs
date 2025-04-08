using Client.ServiceClient.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Shared.DTOs;
using System.Net;
using System.Text;

namespace Client.ServiceClient.Implement
{
    public class UsersServiceClient : IUsersServiceClient
    {
        private readonly HttpClient _httpClient;

        public UsersServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserService");
        }

        public async Task<List<UserDTO>> GetUsersODataAsync(string odataQuery)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Users?{odataQuery}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserDTO>>(content) ?? new List<UserDTO>();
                }

                throw new Exception(response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable
                    ? "Server đang bảo trì"
                    : "Vui lòng thử lại sau");
            }
            catch (Exception ex)    
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Authen/Login", loginDto); // Đơn giản hóa POST
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Response: {response.StatusCode} - {content}");

                var result = JsonConvert.DeserializeObject<LoginResponseDTO>(content)
                    ?? new LoginResponseDTO { Success = false, ErrorMessage = "Invalid response" };

                if (response.IsSuccessStatusCode && result.Success)
                    return result;

                if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    throw new Exception("Server is under maintenance");

                return result; // Trả về DTO với Success = false nếu đăng nhập sai
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Authen/Register", registerDto); // Đơn giản hóa POST
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Requesting: {_httpClient.BaseAddress}api/Authen/Register");
                Console.WriteLine($"Request body: {JsonConvert.SerializeObject(registerDto)}");
                Console.WriteLine($"Response: {response.StatusCode} - {content}");

                if (response.IsSuccessStatusCode)
                {
                    return true; // Đăng ký thành công
                }

                throw new Exception(response.StatusCode switch
                {
                    HttpStatusCode.Conflict => "Email or Username already exists",
                    HttpStatusCode.BadRequest => "Invalid registration information",
                    HttpStatusCode.ServiceUnavailable => "Server is under maintenance",
                    HttpStatusCode.NotFound => "Registration endpoint not found",
                    _ => $"Unknown error: {response.StatusCode}"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Ném lại exception để Controller xử lý
            }
        }
    }
}
