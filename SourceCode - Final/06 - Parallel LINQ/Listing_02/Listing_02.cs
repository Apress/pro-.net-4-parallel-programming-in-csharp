using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_02 {
    class Listing_02 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[10];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // define a sequential linq query
            IEnumerable<double> results1 =
                from item in sourceData
                select Math.Pow(item, 2);

            // enumerate the results of the sequential query
            foreach (double d in results1) {
                Console.WriteLine("Sequential result: {0}", d);
            }

            // define a parallel linq query
            IEnumerable<double> results2 =
                from item in sourceData.AsParallel()
                select Math.Pow(item, 2);

            // enumerate the results of the parallel query
            foreach (double d in results2) {
                Console.WriteLine("Parallel result: {0}", d);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
