using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Reactive.Disposables;

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

        // Observable sample
        public IObservable<string> GetObservableSample()
        {
            // Using Javascript !
            //var observable = new Observable((observer) => {
            //    observer.next("Hello Juanlu!");
            //    observer.next("Hello Eva!");
            //    observer.complete();
            //    // observer.next('Bye'); // Undefined. After complete(). Nothing to show !
            //});

            var observable =  Observable.Create((IObserver<string> observer) =>
            {
                observer.OnNext("Hello Juanlu !");
                observer.OnNext("Hello Eva !");                
                observer.OnCompleted();
                // return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
                // or can return an Action like 
                // return () => Console.WriteLine("Observer has unsubscribed");
                return () => { };
            });           

            return observable;
        }

        private async Task<PagedUsers> GetUsersAsync()
        {
            var json = await this._http.GetStringAsync($"{URL}?page=1&per_page=50");
            var users = JsonConvert.DeserializeObject<PagedUsers>(json);

            return users;
        }

        public async Task<IObservable<User>> GetUsersToObservable()
        {
            var data = await GetUsersAsync();

            return data.Users.ToObservable();
        }

        public async Task<IObservable<User>> GetObservableUsers()
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