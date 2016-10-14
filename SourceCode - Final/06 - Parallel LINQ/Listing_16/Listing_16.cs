using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_16 {
    class Listing_16 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[5];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // define a fully buffered query
            IEnumerable<double> results =
                sourceData.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Select(item => {
                    double resultItem = Math.Pow(item, 2);
                    Console.WriteLine("Produced result {0}", resultItem);
                    return resultItem;
                });

            // enumerate the query results
            foreach (double d in results) {
                Console.WriteLine("Enumeration got result {0}", d);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
