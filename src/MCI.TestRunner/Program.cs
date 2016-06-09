//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Miharu Communications Inc.">
//     © 2016 Miharu Communications Inc.
// </copyright>
//-----------------------------------------------------------------------
namespace Miharu.TestRunner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Miharu.TestRunner.Async.Schedulers;

    public class Program
    {
        private static DisposableCollection collection;


        public static void Main(string[] args)
        {
            collection = new DisposableCollection();


            collection.Add(new PeriodicSchedulerTests());



            Console.WriteLine("Tests Start");
            Console.CancelKeyPress +=Console_CancelKeyPress;


            while (true)
            {
                Thread.Sleep(10 * 1000);
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Tests Finishing...");

            collection.Dispose();

            Console.WriteLine("Tests Finished");
        }
    }
}
