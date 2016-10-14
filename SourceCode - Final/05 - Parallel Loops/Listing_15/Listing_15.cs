using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Listing_15 {

    class Listing_15 {

        static void Main(string[] args) {

            // create the results array
            double[] resultData = new double[10000000];

            // created a partioner that will chunk the data
            OrderablePartitioner<Tuple<int, int>> chunkPart = Partitioner.Create(0, resultData.Length, 10000);

            // perform the loop in chunks
            Parallel.ForEach(chunkPart, chunkRange => {
                // iterate through all of the values in the chunk range
                for (int i = chunkRange.Item1; i < chunkRange.Item2; i++) {
                    resultData[i] = Math.Pow(i, 2);
                }
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
