using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ShellProgressBar;

namespace ElGuerre.SexyConsole
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // LESSON 1)  ConsoleSpinner
            await WorkingWithSpinner(0);
            // await WorkingWithSpinner(4);
            // await WorkingWithSpinner(5);

            // LESSON 2): Utilities like progress bar !
            await WorkingWithProgressBar();

            // Console.WriteLine("Press ENTER to exit !");
            // Console.ReadLine();
        }

        /// <summary>
        /// Manage Keys to exit correctly. Press Ctrl + C to exit correctly.
        /// </summary>
        /// <returns></returns>
        private static Task ConfigureKeys()
        {
            // When press Ctrl + C
            Console.CancelKeyPress += (sender, e) =>
            {                
                Debug.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            // Create a new task to control ESC key press when progress end.
            var taskKeys = new Task(() =>
            {
                var key = new ConsoleKeyInfo();

                while (!Console.KeyAvailable && key.Key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey(true);

                    // TODO: Do more thing when one key is pressed !
                }
            });
            return taskKeys;
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
