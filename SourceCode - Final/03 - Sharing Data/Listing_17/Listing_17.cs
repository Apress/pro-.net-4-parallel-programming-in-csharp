using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_17 {

    class Listing_17 {

        static void Main(string[] args) {

            // create a shared collection 
            Queue<int> sharedQueue = new Queue<int>();
            // populate the collection with items to process
            for (int i = 0; i < 1000; i++) {
                sharedQueue.Enqueue(i);
            }

            // define a counter for the number of processed items
            int itemCount = 0;

            // create tasks to process the list
            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++) {
                // create the new task
                tasks[i] = new Task(() => {

                    while (sharedQueue.Count > 0) {
                        // take an item from the queue
                        int item = sharedQueue.Dequeue();
                        // increment the count of items processed
                        Interlocked.Increment(ref itemCount);
                    }

                });
                // start the new task
                tasks[i].Start();
            }

            // wait for the tasks to complete
            Task.WaitAll(tasks);

            // report on the number of items processed
            Console.WriteLine("Items processed: {0}", itemCount);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
