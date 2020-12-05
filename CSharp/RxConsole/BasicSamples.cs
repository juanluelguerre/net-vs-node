//
// http://introtorx.com/Content/v1.0.10621.0/04_CreatingObservableSequences.html
//
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RxConsole
{
	public class BasicSamples
	{
		// Return an string Observable using ReplaySubject
		public IObservable<string> DoSample1()
		{
			var subject = new ReplaySubject<string>();
			subject.OnNext("Value");
			subject.OnCompleted();

			return subject;
		}

		// Return an string Observable using "Return". So return a secuence with just a simple item.
		public IObservable<string> DoSample2()
		{
			return Observable.Return("Hello Juanlu !");
		}

		// Return an string Observable using ReplaySubject
		public IObservable<string> DoSample3()
		{
			var subject = new ReplaySubject<string>();
			subject.OnNext("Hello Juanlu !");
			Thread.Sleep(500);
			subject.OnNext("Hello Eva !");
			Thread.Sleep(1000);
			subject.OnNext("Hello Mars !");
			subject.OnCompleted();

			return subject;
		}

		public IObservable<string> DoSample4()
		{
			return Observable.Create((IObserver<string> observer) =>
			{
				observer.OnNext("Hello Juanlu !");
				observer.OnNext("Hello Eva !");
				observer.OnNext("Hello Mars !");
				observer.OnCompleted();
				Thread.Sleep(1000);
				return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
				//or can return an Action like 
				//return () => Console.WriteLine("Observer has unsubscribed"); 
			});
		}

		public IObservable<string> DoSample5(string[] messages)
		{
			 return Observable.Generate(
				string.Empty,
				value => messages != null,
				value => string.Join(", ", messages),
				value => value
			);
		}
		
		public IObservable<int> DoRange(int start, int count)
		{
			return Observable.Range(start, count);
		}

		public IObservable<int> DoRangeUsingGenerate(int start, int count)
		{
			var max = start + count;
			return Observable.Generate(
				start,
				value => value < max,
				value => value + 1,
				value => value
			);
		}

		// Generate a tick every second (1000 ms).
		public IObservable<string> DoTicks(int milliseconds)
		{
			return Observable.Create<string>(
				observer =>
				{
					var timer = new System.Timers.Timer();
					timer.Interval = milliseconds;
					timer.Elapsed += (s, e) => observer.OnNext("tick");
					// timer.Elapsed += (sender, e) => { Console.WriteLine(e.SignalTime); };
					timer.Start();
					return Disposable.Empty;
				});
		}

		public IObservable<long> DoInterval(int milliseconds)
		{
			return Observable.Interval(TimeSpan.FromMilliseconds(milliseconds));
		}

		public IObservable<string> DoTask()
		{
			var t = Task.Factory.StartNew(() => "Test");
			return t.ToObservable();
		}

		// Returns an empty IObservable<T>.
		public IObservable<string> DoEmpty()
		{
			return Observable.Empty<string>();
		}

		// Return infinite sequence without any notification.
		public IObservable<string> DoNever()
		{
			return Observable.Never<string>();
		}


		public void DoException1()
		{
			var subject = new ReplaySubject<string>();
			subject.OnError(new Exception("Just a sample Exception !"));
		}

		public void DoException2()
		{
			Observable.Throw<string>(new Exception("Just a sample Exception !"));
		}

		//public void DoFromEvents()
		//{

		//	//Activated delegate is EventHandler
		//	var appActivated = Observable.FromEventPattern(
		//	h => Application.Current.Activated += h,
		//	h => Application.Current.Activated -= h);
		//	//PropertyChanged is PropertyChangedEventHandler
		//	var propChanged = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
		//		handler => handler.Invoke,
		//		h => this.PropertyChanged += h,
		//		h => this.PropertyChanged -= h
		//	);
		//	//FirstChanceException is EventHandler<FirstChanceExceptionEventArgs>
		//	var firstChanceException = Observable.FromEventPattern<FirstChanceExceptionEventArgs>(
		//	h => AppDomain.CurrentDomain.FirstChanceException += h,
		//	h => AppDomain.CurrentDomain.FirstChanceException -= h);
		//}
	}
}
