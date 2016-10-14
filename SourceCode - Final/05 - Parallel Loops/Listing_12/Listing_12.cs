using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_12 {
    class Listing_12 {
        static void Main(string[] args) {

            // create a running total of matched words
            int matchedWords = 0;
            // create a lock object
            object lockObj = new object();

            // define the source data
            string[] dataItems 
                = new string[] { "an", "apple", "a", "day", 
                    "keeps", "the", "doctor", "away" };

            // perform a parallel foreach loop with TLS
            Parallel.ForEach(
                dataItems,
                () => 0,
                (string item, ParallelLoopState loopState, int tlsValue) => {
                    // increment the tls value if the item
                    // contains the letter 'a'
                    if (item.Contains("a")) {
                        tlsValue++;
                    }
                    return tlsValue;
                },
                tlsValue => {
                    lock (lockObj) {
                        matchedWords += tlsValue;
                    }
                });

            Console.WriteLine("Matches: {0}", matchedWords);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
