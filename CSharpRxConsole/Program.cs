using System.Reflection.Metadata;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSharpRxConsole
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var service = new Service(new HttpClient());
            var pagedUsers = await service.GetUsers();

            Console.WriteLine($"{pagedUsers.Total} users found !");
            foreach (var u in pagedUsers.Users)
            {
                Console.WriteLine($"   - {u.FirstName} {u.LastName} - {u.Email}");
            }
        }
    }
}
