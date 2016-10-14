using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_11 {

    class Listing_11 {

        static void Main(string[] args) {

            // create the parent task
            Task parentTask = new Task(() => {

                // create the first child task
                Task childTask = new Task(() => {
                    // write out a message and wait
                    Console.WriteLine("Child 1 running");
                    Thread.Sleep(1000);
                    Console.WriteLine("Child 1 finished");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);

                // create an attached continuation
                childTask.ContinueWith(antecedent => {
                    // write out a message and wait
                    Console.WriteLine("Continuation running");
                    Thread.Sleep(1000);
                    Console.WriteLine("Continuation finished");
                },
                TaskContinuationOptions.AttachedToParent
                | TaskContinuationOptions.OnlyOnFaulted);

                Console.WriteLine("Starting child task...");
                childTask.Start();
            });

            // start the parent task
            parentTask.Start();

            try {
                // wait for the parent task
                Console.WriteLine("Waiting for parent task");
                parentTask.Wait();
                Console.WriteLine("Parent task finished");
            } catch (AggregateException ex) {
                Console.WriteLine("Exception: {0}", ex.InnerException.GetType());
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
