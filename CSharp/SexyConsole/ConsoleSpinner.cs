using System;

namespace ElGuerre.SexyConsole
{
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
