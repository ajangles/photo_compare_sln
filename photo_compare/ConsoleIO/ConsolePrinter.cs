using System;
using System.Collections.Generic;
using System.Text;
using photo_compare.Models;

namespace photo_compare.ConsoleIO
{
    public class ConsolePrinter : IConsolePrinter
    {
        public void PrintWelcomeMessage()
        {
            Console.WriteLine("***************************************************************************************************\r\n");
            Console.WriteLine("\t\t\t\t" + "Welcome to the Image Comparer\r\n");

            Console.WriteLine("***************************************************************************************************\r\n");

            Console.WriteLine("\t\tThis program will help you identify any duplicates in your image library.\r\n\n\n");
        }

        public void PrintSimilarImagesDetails(ImageFile toPrint)
        {
            //TODO Pagination ?

            Console.WriteLine("File \"" +
                              toPrint.Name +
                              "\" located at: \r\n\t\"" +
                              toPrint.FullPath +
                              "\"\n\t has these similar files: \r\t"
                              
                              );

            foreach (var item in toPrint.SimilarImages)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\t\t" + item.FullPath + "\\" + item.Name);
                
            }

            Console.ResetColor();

            Console.WriteLine();
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintError(string message, Exception e)
        {
            Console.WriteLine(message, e.Message);
        }

        public string GetEntryFromUser(string message)
        {
            Console.WriteLine(message);

            Console.Write(":");
            return Console.ReadLine();
        }
    }
}
