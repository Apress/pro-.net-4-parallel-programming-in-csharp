using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_18 {

    class Listing_18 {

        static void Main(string[] args) {

            // create the primtive
            SemaphoreSlim semaphore = new SemaphoreSlim(2);

            // create the cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();

            // create and start the task that will wait on the event
            for (int i = 0; i < 10; i++) {
                Task.Factory.StartNew(() => {
                    while (true) {
                        semaphore.Wait(tokenSource.Token);
                        // print out a message when we are released
                        Console.WriteLine("Task {0} released", Task.CurrentId);
                    }
                }, tokenSource.Token);
            }

            // create and start the signalling task
            Task signallingTask = Task.Factory.StartNew(() => {
                // loop while the task has not been cancelled
                while (!tokenSource.Token.IsCancellationRequested) {
                    // go to sleep for a random period
                    tokenSource.Token.WaitHandle.WaitOne(500);
                    // signal the semaphore
                    semaphore.Release(2);
                    Console.WriteLine("Semaphore released");
                }
                // if we reach this point, we know the task has been cancelled
                tokenSource.Token.ThrowIfCancellationRequested();
            }, tokenSource.Token);

            // ask the user to press return before we cancel 
            // the token and bring the tasks to an end
            Console.WriteLine("Press enter to cancel tasks");
            Console.ReadLine();

            // cancel the token source and wait for the tasks
            tokenSource.Cancel();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
