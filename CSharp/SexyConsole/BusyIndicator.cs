using System;
using System.Threading.Tasks;

namespace SexyConsole
{
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
}
