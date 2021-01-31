using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using photo_compare.ConsoleIO;
using photo_compare.FileIO;
using photo_compare.ImageIO;
using photo_compare.Models;

namespace photo_compare
{
    class Program
    {
        private static IRunMe _runMe;

        static void Main(string[] args)
        {
            _runMe = new RunMe();
            _runMe.Start();
        }
    }
}
