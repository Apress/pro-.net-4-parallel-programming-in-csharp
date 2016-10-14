using System;
using System.Linq;

namespace Parallel_Reduce {

    class Use_Parallel_Reduce {

        static void Main(string[] args) {

            // create some source data
            int[] sourceData = Enumerable.Range(0, 10).ToArray();

            // create the aggregation function
            Func<int, int, int> reduceFunction = (value1, value2) => value1 + value2;

            // perform the reduction
            int result = Parallel_Reduce.Reduce<int>(sourceData, 0, reduceFunction);

            // write out the result
            Console.WriteLine("Result: {0}", result);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
