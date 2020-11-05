using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using System.Reactive.Linq;

//
// https://reqres.in/
//
namespace CSharpRxConsole
{

    public class Service
    {
        private const string URL = "https://reqres.in/api/users";
        private HttpClient _http;        

        public Service(HttpClient http)
        {
            _http = http;
        }

        private async Task<PagedUsers> GetUsersAsync()
        {
            var json = await this._http.GetStringAsync($"{URL}?page=1&per_page=50");
            var users = JsonConvert.DeserializeObject<PagedUsers>(json);

            return users;
        }

        public async Task<IObservable<User>> GetToObservable()
        {
            var data = await GetUsersAsync();

            return data.Users.ToObservable();
        }


        public async Task<IObservable<User>> GetObservable()
        {
            var data = await GetUsersAsync();

            var observable = Observable.Create<User>(observer =>
            {
                foreach (var u in data.Users)
                {
                    observer.OnNext(u);
                }
                observer.OnCompleted();
                return () => { };
            });

            return observable;
        }
    }        
}