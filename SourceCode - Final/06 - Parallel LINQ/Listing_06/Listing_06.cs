using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_06 {
    class Listing_06 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[5];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // preserve order with the AsOrdered() method
            IEnumerable<double> results1 =
                from item in sourceData.AsParallel().AsOrdered()
                select Math.Pow(item, 2);

            // create an index into the source array
            int index = 0;

            // enumerate the results
            foreach (double d in results1) {
                Console.WriteLine("Bad result {0} from item {1}", d, index++);
            }

            // perform the query without ordering the results
            var results2 =
                from item in sourceData.AsParallel()
                select new {
                    sourceValue = item,
                    resultValue = Math.Pow(item, 2)
                };

            // enumerate the results
            foreach (var v in results2) {
                Console.WriteLine("Better result {0} from item {1}",
                    v.resultValue, v.sourceValue);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
