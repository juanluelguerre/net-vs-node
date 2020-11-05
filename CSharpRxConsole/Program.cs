using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ShellProgressBar;

namespace CSharpRxConsole
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            // Leson 1) Benchmack sample
            // BenchmarkRunner.Run<ObserableSamples>();

            // LESSON 2): Introduction to Observables.
            //var ob = new ObserableSamples();
            //await ob.GetUsers_ToObservable();

            // Console.WriteLine("Press ENTER to exit !");
            // Console.ReadLine();


            // LESSON 3): Director "files" watcher using Observables !
            //var fm = new FileEventsManager(@"files");
            //fm.Created.Subscribe(e => Console.WriteLine("{0} was created.", e.FullPath));
            //fm.Renamed.Subscribe(e => Console.WriteLine("{0} was renamed to {1}.", e.OldFullPath, e.FullPath));
            //fm.Deteleted.Subscribe(e => Console.WriteLine("{0} was deleted.", e.FullPath));

            // Console.WriteLine("Press ENTER to exit !");
            // Console.ReadLine();

            // LESSON 5)  ConsoleSpinner
            await WorkingWithSpinner(0);
            // await WorkingWithSpinner(4);
            // await WorkingWithSpinner(5);

            // LESSON 4): Utilities like progress bar !
            await WorkingWithProgressBar();

        }

        private static async Task WorkingWithSpinner(int sequenceCode)
        {
            var taskKeys = ConfigureKeys();

            var spinner = new ConsoleSpinner
            {
                Delay = 300,
                SequenceCode = sequenceCode
            };

            int maxNumbersToProcess = 20;
            var gen = new NumbersGenerator(maxNumbersToProcess);
            var numbers = await gen.GetNumbers();
            numbers
                // .Subscribe(n => Console.WriteLine($"{n} de {max} completed !"));
                .Subscribe(
                    n => spinner.Turn(displayMsg: "Working"),
                    () =>
                    {
                        taskKeys.Start();
                        Console.WriteLine("Press ESC to Exit");
                    }
                );

            // Console.ReadLine(); 
            var tasks = new[] { taskKeys };
            Task.WaitAll(tasks);
        }

        private static Task ConfigureKeys()
        {
            // When press Ctrl + C
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            // Create a new task to control ESC key press when progress end.
            var taskKeys = new Task(() =>
            {
                ConsoleKeyInfo key = new ConsoleKeyInfo();

                while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey(true);

                    // TODO: Do more thing when one key is pressed !
                }
            });
            return taskKeys;
        }

        /// <summary>
        /// Avoid While(true), using progress-bar and Observables woking with Console. 
        /// </summary>
        /// <returns></returns>
        private static async Task WorkingWithProgressBar()
        {
            var taskKeys = ConfigureKeys();

            int maxNumbersToProcess = 10;
            var pbar = new ProgressBar(
                maxNumbersToProcess,
                "Loading...",
                new ProgressBarOptions()
                {
                    ProgressCharacter = '\u2593',
                    ProgressBarOnBottom = true // Progress bar under the message
                });

            var gen = new NumbersGenerator(maxNumbersToProcess);
            var numbers = await gen.GetNumbers(enableRandomDelay: true);
            numbers
                // .Subscribe(n => Console.WriteLine($"{n} de {max} completed !"));
                .Subscribe(
                    n => pbar.Tick(),
                    () =>
                    {
                        pbar.Dispose();
                        taskKeys.Start();
                        Console.WriteLine("Press ESC to Exit");
                    }
                );

            // Console.ReadLine();
            var tasks = new[] { taskKeys };
            Task.WaitAll(tasks);

        }
    }

    internal class BusyIndicator
    {
        int _currentBusySymbol;

        public char[] BusySymbols { get; set; }

        public BusyIndicator()
        {
            BusySymbols = new[] { '|', '/', '-', '\\' };
        }

        public void UpdateProgress()
        {
            while (true)
            {
                Task.Delay(100);
                var originalX = Console.CursorLeft;
                var originalY = Console.CursorTop;

                Console.Write(BusySymbols[_currentBusySymbol]);

                _currentBusySymbol++;

                if (_currentBusySymbol == BusySymbols.Length)
                {
                    _currentBusySymbol = 0;
                }

                Console.SetCursorPosition(originalX, originalY);
            }
        }
    }

    /// <summary>
    /// <seealso cref="https://stackoverflow.com/questions/1923323/console-animations"/>
    /// </summary>
    internal class ConsoleSpinner
    {
        static string[,] sequence = null;

        public int Delay { get; set; } = 200;
        /// 0 | 1 | 2 |3 | 4 | 5 
        public int SequenceCode { get; set; } = 0;

        readonly int totalSequences = 0;
        int counter;

        public ConsoleSpinner()
        {
            counter = 0;
            sequence = new string[,] {
                { "/", "-", "\\", "|" },
                { ".", "o", "0", "o" },
                { "+", "x","+","x" },
                { "V", "<", "^", ">" },
                { ".   ", "..  ", "... ", "...." },
                { "=>   ", "==>  ", "===> ", "====>" },
               // ADD YOUR OWN CREATIVE SEQUENCE HERE IF YOU LIKE
            };

            totalSequences = sequence.GetLength(0);

            SequenceCode = 0;
        }

        /// <summary>
        /// Write next iteration from the sequence
        /// </summary>        
        public void Turn(string displayMsg = "")
        {
            counter++;

            // Task.Delay(Delay);

            SequenceCode = SequenceCode > totalSequences - 1 ? 0 : SequenceCode;

            int counterValue = counter % 4;

            displayMsg += string.IsNullOrWhiteSpace(displayMsg) ? "" : " ";
            string fullMessage = displayMsg + sequence[SequenceCode, counterValue];
            int msglength = fullMessage.Length;

            Console.Write(fullMessage);

            Console.SetCursorPosition(Console.CursorLeft - msglength, Console.CursorTop);
        }
    }
}