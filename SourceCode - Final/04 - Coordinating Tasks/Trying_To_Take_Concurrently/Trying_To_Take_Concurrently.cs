using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Trying_To_Take_Concurrently {
    class Trying_To_Take_Concurrently {
        static void Main(string[] args) {

            // create a blocking collection
            BlockingCollection<int> blockingCollection
                = new BlockingCollection<int>();

            // create and start a producer
            Task.Factory.StartNew(() => {
                // put items into the collectioon
                for (int i = 0; i < 1000; i++) {
                    blockingCollection.Add(i);
                }
                // mark the collection as complete
                blockingCollection.CompleteAdding();
            });

            // create and start a producer
            Task.Factory.StartNew(() => {
                while (!blockingCollection.IsCompleted) {
                    // take an item from the collection
                    int item = blockingCollection.Take();
                    // print out the item
                    Console.WriteLine("Item {0}", item);
                }
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
