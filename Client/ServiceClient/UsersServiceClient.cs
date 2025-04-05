using Newtonsoft.Json;
using Shared.DTOs;

namespace Client.ServiceClient
{
    public class UsersServiceClient
    {
        private readonly HttpClient _httpClient;

        public UsersServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserService");
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/users/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    // Lấy chuỗi JSON từ response
                    var content = await response.Content.ReadAsStringAsync();
                    // Deserialize thành List<UserDTO>
                    var users = JsonConvert.DeserializeObject<List<UserDTO>>(content);
                    return users ?? new List<UserDTO>();
                }
                else
                {
                    // Nếu không thành công, ném ra thông báo lỗi thích hợp
                    throw new Exception($"Error retrieving users. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu cần thiết)
                Console.Error.WriteLine(ex.Message);
                throw new Exception("An error occurred while retrieving users. Please try again later.");
            }
        }

    }
}
