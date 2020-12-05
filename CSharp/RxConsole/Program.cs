using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using RxConsole;

namespace CSharpRxConsole
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{

			#region  LESSON 1) Basic samples

			var samples = new BasicSamples();
			await samples.DoSample1();

			samples.DoSample1().Subscribe(s => Console.WriteLine(s)).Dispose();
			samples.DoSample2().Subscribe(s => Console.WriteLine(s)).Dispose();
			samples.DoSample3().Subscribe(s => Console.WriteLine(s)).Dispose();
			samples.DoSample4().Subscribe(s => Console.WriteLine(s)).Dispose();
			samples.DoSample5(new[] { "Hello Juanlu !", "Hello Eva !", "Hello Bruno !" }).Subscribe(s => Console.WriteLine(s)).Dispose();

			samples.DoRange(1, 10).Subscribe(i => Console.WriteLine(i)).Dispose();
			samples.DoRangeUsingGenerate(1, 10).Subscribe(i => Console.WriteLine(i)).Dispose();

			samples.DoInterval(1000).Subscribe(l => Console.WriteLine(l)).Dispose();
			samples.DoTicks(1000).Subscribe(s => Console.WriteLine(s)).Dispose();

			samples.DoTask().Subscribe(s => Console.WriteLine(s)).Dispose();

			samples.DoEmpty().Subscribe(s => Console.WriteLine(s)).Dispose();
			samples.DoNever().Subscribe(s => Console.WriteLine(s)).Dispose();

			samples.DoException1();
			samples.DoException2();

			Console.WriteLine("Press ENTER to exit !");
			Console.ReadLine();

			#endregion
		
			#region LESSON 2): Director "files" watcher using Observables !

			//var fm = new FileEventsManager(@"files");
			//fm.Created.Subscribe(e => Console.WriteLine("{0} was created.", e.FullPath));
			//fm.Renamed.Subscribe(e => Console.WriteLine("{0} was renamed to {1}.", e.OldFullPath, e.FullPath));
			//fm.Deteleted.Subscribe(e => Console.WriteLine("{0} was deleted.", e.FullPath));

			//Console.WriteLine("Press ENTER to exit !");
			//Console.ReadLine();

			#endregion

			#region LESSON 3): Creates an observable sequence from a subscribe method implementation.
			// Based on: https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229114(v=vs.103)
			IObservable<Ticket> ticketObservable
				= Observable.Create((Func<IObserver<Ticket>, IDisposable>)TicketFactory.TicketSubscribe);

			// This is a sequence of tickets. Display each ticket in the console window.
			using (IDisposable handle
				= ticketObservable.Subscribe(ticket => Console.WriteLine(ticket.ToString())))
			{
				Console.WriteLine("\nPress ENTER to unsubscribe...\n");
				Console.ReadLine();
			}

			#endregion

			#region LESSON 4) Using Services (Same Javascript samples)

			var service = new Service(new HttpClient());
			service.GetObservableSample().Subscribe(s => Console.WriteLine(s));

			var users = await service.GetUsersToObservable();
			users.Subscribe(u => Console.WriteLine($"{u.FirstName} {u.LastName} - {u.Email}"));

			var otherUsers = await service.GetObservableUsers();
			otherUsers.Subscribe(u => Console.WriteLine($"{u.FirstName} {u.LastName} - {u.Email}"));

			// Review/show Benchmark !
			// BenchmarkRunner.Run<ObserableSamples>();

			Console.WriteLine("Press ENTER to exit !");
			Console.ReadLine();

			#endregion
		}

	}
}