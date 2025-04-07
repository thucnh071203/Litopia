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

        // Phương thức chung để gọi OData
        public async Task<List<UserDTO>> GetUsersODataAsync(string odataQuery)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Users?{odataQuery}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<UserDTO>>(content);
                    return users ?? new List<UserDTO>();
                }
                throw new Exception($"Error retrieving users. Status code: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<UserListsDTO> GetAllUsers()
        {
            var result = new UserListsDTO();

            // Tất cả User Available (RoleId = 4, IsDeleted = false)
            result.AllUsersAvailable = await GetUsersODataAsync("$filter=RoleId eq 4 and IsDeleted eq false");

            // Tất cả User bị ban (RoleId = 4, IsDeleted = true)
            result.AllUsersBanned = await GetUsersODataAsync("$filter=RoleId eq 4 and IsDeleted eq true");

            // Tất cả Author Available (RoleId = 2, IsDeleted = false)
            result.AllAuthorsAvailable = await GetUsersODataAsync("$filter=RoleId eq 2 and IsDeleted eq false");

            // Tất cả Author bị ban (RoleId = 2, IsDeleted = true)
            result.AllAuthorsBanned = await GetUsersODataAsync("$filter=RoleId eq 2 and IsDeleted eq true");

            // Tất cả Staff Available (RoleId = 3, IsDeleted = false)
            result.AllStaffAvailable = await GetUsersODataAsync("$filter=RoleId eq 3 and IsDeleted eq false");

            // Tất cả Staff bị ban (RoleId = 3, IsDeleted = true)
            result.AllStaffBanned = await GetUsersODataAsync("$filter=RoleId eq 3 and IsDeleted eq true");

            // Tất cả User có UpToAuthor = true (không lọc RoleId)
            result.AllUsersUpToAuthor = await GetUsersODataAsync("$filter=UpToAuthor eq true");

            return result;
        }
    }
}
