using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_17 {

    class Listing_17 {

        static void Main(string[] args) {

            // create the primtive
            AutoResetEvent arEvent = new AutoResetEvent(false);

            // create the cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();

            // create and start the task that will wait on the event
            for (int i = 0; i < 3; i++) {
                Task.Factory.StartNew(() => {
                    while (!tokenSource.Token.IsCancellationRequested) {
                        // wait on the primtive
                        arEvent.WaitOne();
                        // print out a message when we are released
                        Console.WriteLine("Task {0} released", Task.CurrentId);

                    }
                    // if we reach this point, we know the task has been cancelled
                    tokenSource.Token.ThrowIfCancellationRequested();
                }, tokenSource.Token);
            }

            // create and start the signalling task
            Task signallingTask = Task.Factory.StartNew(() => {
                // loop while the task has not been cancelled
                while (!tokenSource.Token.IsCancellationRequested) {
                    // go to sleep for a random period
                    tokenSource.Token.WaitHandle.WaitOne(500);
                    // set the event
                    arEvent.Set();
                    Console.WriteLine("Event set");
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
