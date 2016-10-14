using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_16 {

    class Listing_16 {

        static void Main(string[] args) {

            // create the primtive
            ManualResetEventSlim manualResetEvent
                = new ManualResetEventSlim();

            // create the cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();

            // create and start the task that will wait on the event
            Task waitingTask = Task.Factory.StartNew(() => {
                while (true) {
                    // wait on the primitive
                    manualResetEvent.Wait(tokenSource.Token);
                    // print out a message
                    Console.WriteLine("Waiting task active");
                }
            }, tokenSource.Token);

            // create and start the signalling task
            Task signallingTask = Task.Factory.StartNew(() => {
                // create a random generator for sleep periods
                Random rnd = new Random();
                // loop while the task has not been cancelled
                while (!tokenSource.Token.IsCancellationRequested) {
                    // go to sleep for a random period
                    tokenSource.Token.WaitHandle.WaitOne(rnd.Next(500, 2000));
                    // set the event
                    manualResetEvent.Set();
                    Console.WriteLine("Event set");
                    // go to sleep again
                    tokenSource.Token.WaitHandle.WaitOne(rnd.Next(500, 2000));
                    // reset the event
                    manualResetEvent.Reset();
                    Console.WriteLine("Event reset");
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
            try {
                Task.WaitAll(waitingTask, signallingTask);
            } catch (AggregateException) {
                // discard exceptions
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
