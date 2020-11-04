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

        public async Task<IObservable<User>> GetObservableUsers()
        {
            var data = await this.GetUsers();

            //var observable = Observable.Create<User>(observer =>
            //{
            //    foreach (var u in data.Users)
            //    {
            //        observer.OnNext(u);
            //    }
            //    observer.OnCompleted();
            //    return () => { };
            //});
            // return observable;

            return data.Users.ToObservable();
        }
    }
}