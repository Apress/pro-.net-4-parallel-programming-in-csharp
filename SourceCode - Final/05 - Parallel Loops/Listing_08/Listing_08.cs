using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Listing_08 {
    class Listing_08 {
        static void Main(string[] args) {

            ParallelLoopResult res = Parallel.For(0, 100, 
                (int index, ParallelLoopState loopState) => {
                // calculate the square of the index
                double sqr = Math.Pow(index, 2);
                // if the square value is > 100 then break
                if (sqr > 100) {
                    Console.WriteLine("Breaking on index {0}", index);
                    loopState.Break();
                } else {
                    // write out the value
                    Console.WriteLine("Square value of {0} is {1}", index, sqr);
                }
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
