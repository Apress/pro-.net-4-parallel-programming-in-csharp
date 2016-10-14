using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_06 {

    class Listing_06 {

        static void Main(string[] args) {

            // create an array of tasks
            Task<int>[] tasks = new Task<int>[10];

            // create a cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create the random number generator
            Random rnd = new Random();

            for (int i = 0; i < 10; i++) {
                // create a new task
                tasks[i] = new Task<int>(() => {
                    // define the variable for the sleep interval
                    int sleepInterval;
                    // acquire exclusive access to the random
                    // number generator and get a random value
                    lock (rnd) {
                        sleepInterval = rnd.Next(500, 2000);
                    }
                    // put the task thread to sleep for the interval
                    tokenSource.Token.WaitHandle.WaitOne(sleepInterval);
                    // check to see the current task has been cancelled
                    tokenSource.Token.ThrowIfCancellationRequested();
                    // return the sleep interval as the result
                    return sleepInterval;
                }, tokenSource.Token);
            }

            // set up a when-any multi-task continuation
            Task continuation = Task.Factory.ContinueWhenAny<int>(tasks,
                (Task<int> antecedent) => {
                    // write out a message using the antecedent result
                    Console.WriteLine("The first task slept for {0} milliseconds",
                        antecedent.Result);
                });

            // start the atecedent tasks
            foreach (Task t in tasks) {
                t.Start();
            }

            // wait for the contination task to complete
            continuation.Wait();

            // cancel the remaining tasks
            tokenSource.Cancel();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
