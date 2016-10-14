using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_03 {
    class Listing_03 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[10];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // define a sequential linq query
            IEnumerable<double> results1 =
                sourceData.Select(item => Math.Pow(item, 2));

            // enumerate the results of the sequential query
            foreach (double d in results1) {
                Console.WriteLine("Sequential result: {0}", d);
            }

            // define a parallel linq query
            var results2 = sourceData.AsParallel()
                    .Select(item => Math.Pow(item, 2));

            // enumerate the results of the parallel query
            foreach (var d in results2) {
                Console.WriteLine("Parallel result: {0}", d);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
