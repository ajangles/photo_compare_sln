using System;
using System.Collections.Generic;
using System.Text;
using photo_compare.Models;

namespace photo_compare.ConsoleIO
{
    public interface IConsolePrinter
    {
        public void PrintWelcomeMessage();
        public void PrintSimilarImagesDetails(ImageFile toPrint);
        public void PrintMessage(string message);
        public void PrintError(string message, Exception e);
        public string GetEntryFromUser(string message);
    }
}
