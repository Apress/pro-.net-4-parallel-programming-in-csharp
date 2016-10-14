using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Listing_09 {
    class Listing_09 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[10];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            Console.WriteLine("Defining PLINQ query");
            // define the query
            IEnumerable<double> results =
                sourceData.AsParallel().Select(item => {
                    Console.WriteLine("Processing item {0}", item);
                    return Math.Pow(item, 2);
                });

            Console.WriteLine("Waiting...");
            Thread.Sleep(5000);

            // sum the results - this will trigger
            // execution of the query
            Console.WriteLine("Accessing results");
            double total = 0;
            foreach (double d in results) {
                total += d;
            }
            Console.WriteLine("Total {0}", total);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
