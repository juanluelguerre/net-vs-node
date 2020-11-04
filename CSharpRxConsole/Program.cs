using System.Reflection.Metadata;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace CSharpRxConsole
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var service = new Service(new HttpClient());
            var pagedUsers = await service.GetUsers();

            //Console.WriteLine($"{pagedUsers.Total} users found !");
            //foreach (var u in pagedUsers.Users)
            //{
            //    Console.WriteLine($"   - {u.FirstName} {u.LastName} - {u.Email}");
            //}

            var users = await service.GetObservableUsers();
            users
                .Where(u => u.LastName.StartsWith("F"))
                .Select(u => new User() { Id = u.Id, FirstName = u.FirstName, LastName =u.LastName, Email = u.Email.Replace("@reqres.in", "@rxjs.dev"), Avatar = u.Avatar })                
                .Subscribe(u =>
                {
                    Console.WriteLine($"   - {u.FirstName} {u.LastName} - {u.Email}");
                });
        }
    }
}
