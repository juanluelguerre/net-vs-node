using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace CSharpRxConsole
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // Leson 1) Benchmack sample
            // BenchmarkRunner.Run<ObserableSamples>();

            // LESSON 2): Introduction to Observables.
            var ob = new ObserableSamples();
            await ob.GetUsers_ToObservable();


            // LESSON 3): Director "files" watcher using Observables !
            //var fm = new FileEventsManager(@"files");
            //fm.Created.Subscribe(e => Console.WriteLine("{0} was created.", e.FullPath));
            //fm.Renamed.Subscribe(e => Console.WriteLine("{0} was renamed to {1}.", e.OldFullPath, e.FullPath));
            //fm.Deteleted.Subscribe(e => Console.WriteLine("{0} was deleted.", e.FullPath));

            Console.WriteLine("Press ENTER to exit !");
            Console.ReadLine();

        }
    }    
}