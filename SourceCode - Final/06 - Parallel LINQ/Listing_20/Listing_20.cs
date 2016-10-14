using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_20 {
    class Listing_20 {
        static void Main(string[] args) {

            // use PLINQ to process a parallel range
            IEnumerable<double> result1 =
                from e in ParallelEnumerable.Range(0, 10)
                where e % 2 == 0
                select Math.Pow(e, 2);
                

            // use PLINQ to process a repeating sequence
            IEnumerable<double> result2 =
                ParallelEnumerable.Repeat(10, 100)
                .Select(item => Math.Pow(item, 2));

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
