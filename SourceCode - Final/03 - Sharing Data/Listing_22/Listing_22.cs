using System;
using System.Collections;
using System.Threading.Tasks;

namespace Listing_22 {

    class Listing_22 {

        static void Main(string[] args) {

            // create a collection 
            Queue sharedQueue = Queue.Synchronized(new Queue());

            // create tasks to process the list
            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++) {
                // create the new task
                tasks[i] = new Task(() => {

                    for (int j = 0; j < 100; j++) {
                        sharedQueue.Enqueue(j);
                    }

                });
                // start the new task
                tasks[i].Start();
            }

            // wait for the tasks to complete
            Task.WaitAll(tasks);

            // report on the number of items enqueued
            Console.WriteLine("Items enqueued: {0}", sharedQueue.Count);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
