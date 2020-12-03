using System;
using System.Threading.Tasks;
using ShellProgressBar;

namespace SexyConsole
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


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
}
