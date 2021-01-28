using System;
using System.Collections.Generic;
using System.Text;

namespace photo_compare.ConsoleIO
{
    public interface IConsolePrinter
    {
        public void PrintWelcomeMessage();
        public void PrintStringList(string message, IList<string> toPrint);
    }
}
