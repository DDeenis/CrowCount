using System;
using System.Threading;
using CrowCount.Models;

namespace CrowCount
{
    class Program
    {
        static void Main(string[] args)
        {
            // foreach (var day in Day.CreateRandomDays(10))
            // {
            //     Console.WriteLine(day);
            //     Thread.Sleep(2000);
            // }

            CrowCounter crowCounter = new CrowCounter();
            crowCounter.StartCount();
        }
    }
}
