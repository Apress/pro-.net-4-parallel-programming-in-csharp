using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Listing_02 {
    class Listing_02 {
        static void Main(string[] args) {

            // create some source data
            Random rnd = new Random();
            int[] sourceData = new int[100000000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = rnd.Next(0, int.MaxValue);
            }

            // define the measurement variables
            int numberOfIterations = 10;
            int maxDegreeOfConcurrency = 16;

            // define the lock object for updating the shared result
            object lockObj = new object();

            // outer loop is degree of concurrency
            for (int concurrency = 1; 
                concurrency <= maxDegreeOfConcurrency; 
                concurrency++) {

                // reset the stopwatch for this concurrency degree
                Stopwatch stopWatch = Stopwatch.StartNew();

                // create the loop options for this degree
                ParallelOptions options = new ParallelOptions() 
                    { MaxDegreeOfParallelism = concurrency };

                // inner loop is repeated iterations with same concurrency
                for (int iteration = 0; 
                    iteration < numberOfIterations; 
                    iteration++) {

                    // define the (shared) result
                    double result = 0;

                    // perform the work
                    Parallel.ForEach(
                        sourceData,
                        options,
                        () => 0.0,
                        (int value, 
                            ParallelLoopState loopState, 
                            long index, 
                            double localTotal) => {
                                return localTotal + Math.Pow(value, 2);
                            },
                        localTotal => {
                            lock (lockObj) {
                                result += localTotal;
                            }
                        });
                }
                // stop the stopwatch
                stopWatch.Stop();

                // write out the per-iteration time for this degree of concurrency
                Console.WriteLine("Concurrency {0}: Per-iteration time is {1} ms", 
                    concurrency, 
                    stopWatch.ElapsedMilliseconds/numberOfIterations);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
