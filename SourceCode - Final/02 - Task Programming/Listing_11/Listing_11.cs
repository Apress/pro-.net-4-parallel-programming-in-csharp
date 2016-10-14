using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_11 {

    class Listing_11 {

        static void Main(string[] args) {

            // create the cancellation token sources
            CancellationTokenSource tokenSource1 = new CancellationTokenSource();
            CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            CancellationTokenSource tokenSource3 = new CancellationTokenSource();

            // create a composite token source using multiple tokens
            CancellationTokenSource compositeSource =
                CancellationTokenSource.CreateLinkedTokenSource(
                    tokenSource1.Token, tokenSource2.Token, tokenSource3.Token);

            // create a cancellable task using the composite token
            Task task = new Task(() => {
                // wait until the token has been cancelled
                compositeSource.Token.WaitHandle.WaitOne();
                // throw a cancellation exception 
                throw new OperationCanceledException(compositeSource.Token);
            }, compositeSource.Token);

            // start the task
            task.Start();

            // cancel one of the original tokens
            tokenSource2.Cancel();

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
