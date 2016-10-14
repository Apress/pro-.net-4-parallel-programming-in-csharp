using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_05 {
    class Listing_05 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[10];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // preserve order with the AsOrdered() method
            IEnumerable<double> results =
                from item in sourceData.AsParallel().AsOrdered()
                select Math.Pow(item, 2);

            // enumerate the results of the parallel query
            foreach (double d in results) {
                Console.WriteLine("Parallel result: {0}", d);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
