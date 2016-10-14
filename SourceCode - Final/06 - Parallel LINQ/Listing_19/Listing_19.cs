using System;
using System.Linq;

namespace Listing_19 {
    class Listing_19 {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[10000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // perform a custom aggregation
            double aggregateResult = sourceData.AsParallel().Aggregate(
                // 1st function - initialize the result
                0.0,
                // 2nd function - process each item and the per-Task subtotal
                (subtotal, item) => subtotal += Math.Pow(item, 2),
                // 3rd function - process the overall total and the per-Task total
                (total, subtotal) => total + subtotal,
                // 4th function - perform final processing
                total => total / 2);

            // write out the result
            Console.WriteLine("Total: {0}", aggregateResult);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
