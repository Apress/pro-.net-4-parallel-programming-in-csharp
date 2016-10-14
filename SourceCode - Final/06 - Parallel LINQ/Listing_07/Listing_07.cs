using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listing_07 {
    class Listing_07 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[10000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // define a query that has an ordered subquery
            var result =
                sourceData.AsParallel().AsOrdered()
                .Take(10).AsUnordered()
                .Select(item => new {
                    sourceValue = item,
                    resultValue = Math.Pow(item, 2)
                });

            foreach (var v in result) {
                Console.WriteLine("Source {0}, Result {1}",
                    v.sourceValue, v.resultValue);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
