using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_06 {

    class Listing_06 {

        static void Main(string[] args) {

            // create a ParallelOptions instance
            // and set the max concurrency to 1
            ParallelOptions options 
                = new ParallelOptions() { MaxDegreeOfParallelism = 1 };

            // perform a parallel for loop
            Parallel.For(0, 10, options, index => {
                Console.WriteLine("For Index {0} started", index);
                Thread.Sleep(500);
                Console.WriteLine("For Index {0} finished", index);
            });

            // create an array of ints to process
            int[] dataElements = new int[] { 0, 2, 4, 6, 8 };

            // perform a parallel foreach loop
            Parallel.ForEach(dataElements, options, index => {
                Console.WriteLine("ForEach Index {0} started", index);
                Thread.Sleep(500);
                Console.WriteLine("ForEach Index {0} finished", index);
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();  
        }
    }
}
