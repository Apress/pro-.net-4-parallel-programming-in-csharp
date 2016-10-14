using System;
using System.Threading.Tasks;

namespace Listing_19 {

    class Listing_19 {

        static void Main(string[] args) {

            // create the tasks
            Task task1 = new Task(() => {
                ArgumentOutOfRangeException exception = new ArgumentOutOfRangeException();
                exception.Source = "task1";
                throw exception;
            });
            Task task2 = new Task(() => {
                throw new NullReferenceException();
            });
            Task task3 = new Task(() => {
                Console.WriteLine("Hello from Task 3");
            });

            // start the tasks
            task1.Start(); task2.Start(); task3.Start();

            // wait for all of the tasks to complete
            // and wrap the method in a try...catch block
            try {
                Task.WaitAll(task1, task2, task3);
            } catch (AggregateException ex) {
                // enumerate the exceptions that have been aggregated
                foreach (Exception inner in ex.InnerExceptions) {
                    Console.WriteLine("Exception type {0} from {1}",
                        inner.GetType(), inner.Source);
                }
            }

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
