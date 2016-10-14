using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_04 {
    class Listing_04 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[100000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // define a filtering query using keywords
            IEnumerable<double> results1
                = from item in sourceData.AsParallel()
                  where item % 2 == 0
                  select Math.Pow(item, 2);

            // enumerate the results
            foreach (var d in results1) {
                Console.WriteLine("Result: {0}", d);
            }

            // define a filtering query using extension methods
            IEnumerable<double> results2
                = sourceData.AsParallel()
                .Where(item => item % 2 == 0)
                .Select(item => Math.Pow(item, 2));

            // enumerate the results
            foreach (var d in results2) {
                Console.WriteLine("Result: {0}", d);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
