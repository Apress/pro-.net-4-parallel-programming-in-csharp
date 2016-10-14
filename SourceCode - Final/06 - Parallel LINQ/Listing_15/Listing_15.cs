using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_15 {
    class Listing_15 {
        static void Main(string[] args) {

            // create a cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();

            // create some source data
            int[] sourceData = new int[1000000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // define a query that supports cancellation
            IEnumerable<double> results = sourceData
                .AsParallel()
                .WithCancellation(tokenSource.Token)
                .Select(item => {
                    // return the result value
                    return Math.Pow(item, 2);
                });

            // create a task that will wait for 5 seconds
            // and then cancel the token
            Task.Factory.StartNew(() => {
                Thread.Sleep(5000);
                tokenSource.Cancel();
                Console.WriteLine("Token source cancelled");
            });

            try {
                // enumerate the query results
                foreach (double d in results) {
                    Console.WriteLine("Result: {0}", d);
                }
            } catch (OperationCanceledException) {
                Console.WriteLine("Caught cancellation exception");
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
