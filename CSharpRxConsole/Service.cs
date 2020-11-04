using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
//
// https://reqres.in/
//
namespace CSharpRxConsole
{

    public class Service
    {
        private const string URL="https://reqres.in/api/users";
        private HttpClient _http;
        private User[] Users;

        public Service(HttpClient http)
        {
            this._http = http;
        }

        public async Task<PagedUsers> GetUsers()
        {
            var json = await this._http.GetStringAsync($"{URL}?page=1&per_page=50");            
            var users = JsonConvert.DeserializeObject<PagedUsers>(json);

            return users;
        }
    }
}