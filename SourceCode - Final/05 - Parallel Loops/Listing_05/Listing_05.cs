using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Listing_05 {
    class Listing_05 {

        static IEnumerable<int> SteppedIterator(int startIndex,
            int endEndex, int stepSize) {
            for (int i = startIndex; i < endEndex; i += stepSize) {
                yield return i;
            }
        }

        static void Main(string[] args) {

            Parallel.ForEach(SteppedIterator(0, 10, 2), index => {
                Console.WriteLine("Index value: {0}", index);
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();  
        }
    }
}
