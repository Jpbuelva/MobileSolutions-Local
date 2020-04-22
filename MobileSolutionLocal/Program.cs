using MobileSolutionLocal.Application;
using System;
using System.IO;

namespace MobileSolutionLocal
{
    class Program
    {
        static void Main(string[] args)
        {
            Listener listener = new Listener();
            listener.GetFile();
            Console.ReadLine();

        }

    }
}
