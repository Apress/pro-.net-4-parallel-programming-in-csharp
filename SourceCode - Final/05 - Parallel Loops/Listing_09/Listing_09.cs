using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_09 {
    class Listing_09 {
        static void Main(string[] args) {

            // run a parallel loop in which one of
            // the iterations calls Stop()
            ParallelLoopResult loopResult =
                Parallel.For(0, 10, (int index, ParallelLoopState loopState) => {
                    if (index == 2) {
                        loopState.Stop();
                    }
                });

            // get the details from the loop result
            Console.WriteLine("Loop Result");
            Console.WriteLine("IsCompleted: {0}", loopResult.IsCompleted);
            Console.WriteLine("BreakValue: {0}", loopResult.LowestBreakIteration.HasValue);
 
            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
