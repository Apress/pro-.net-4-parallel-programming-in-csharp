using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Listing_07 {
    class Program {
        static void Main(string[] args) {

            List<string> dataItems
                = new List<string>() { "an", "apple", "a", "day", 
                    "keeps", "the", "doctor", "away" };

            Parallel.ForEach(dataItems, (string item, ParallelLoopState state) => {
                if (item.Contains("k")) {
                    Console.WriteLine("Hit: {0}", item);
                    state.Stop();
                } else {
                    Console.WriteLine("Miss: {0}", item);
                }
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
