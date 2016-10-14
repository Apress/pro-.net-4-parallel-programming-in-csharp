using System;
using System.Linq;

namespace Parallel_Map {

    class Use_Parallel_Map {

        static void Main(string[] args) {

            // create the source data
            int[] sourceData = Enumerable.Range(0, 100).ToArray();

            // define the mapping function
            Func<int, double> mapFunction = value => Math.Pow(value, 2);

            // map the source data
            double[] resultData = Parallel_Map.ParallelMap<int, double>(mapFunction, sourceData);

            // run through the results
            for (int i = 0; i < sourceData.Length; i++) {
                Console.WriteLine("Value {0} mapped to {1}",
                    sourceData[i],
                    resultData[i]);
            }
        }
    }
}
