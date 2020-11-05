using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace CSharpRxConsole
{
    public class NumbersGenerator
    {
        public NumbersGenerator(int max)
        {
            Max = max;
        }

        public int Max { get; private set; }

        public async Task<IObservable<int>> GetNumbers(bool enableRandomDelay = false)
        {            
            var rand = new Random();

            var observable = Observable.Create<int>(async observer =>
            {
                for (int i = 1; i <= Max; i++)
                {
                    observer.OnNext(i);
                    if (enableRandomDelay)
                        await Task.Delay(rand.Next(1, 11) * 100); //  Secs between 1 and 5 (in milliseconds).
                    else
                        await Task.Delay(200);
                }
                observer.OnCompleted();
                return () => { };
            });

            await Task.CompletedTask;
            return observable;
        }
    }
}
