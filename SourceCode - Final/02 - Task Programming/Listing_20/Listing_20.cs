using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_20 {

    class Listing_20 {

        static void Main(string[] args) {

            // create the cancellation token source and the token
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            // create a task that waits on the cancellation token
            Task task1 = new Task(() => {
                // wait forever or until the token is cancelled
                token.WaitHandle.WaitOne(-1);
                // throw an exception to acknowledge the cancellation
                throw new OperationCanceledException(token);
            }, token);

            // create a task that throws an exceptiono
            Task task2 = new Task(() => {
                throw new NullReferenceException();
            });

            // start the tasks
            task1.Start(); task2.Start();

            // cancel the token
            tokenSource.Cancel();

            // wait on the tasks and catch any exceptions
            try {
                Task.WaitAll(task1, task2);
            } catch (AggregateException ex) {
                // iterate through the inner exceptions using 
                // the handle method
                ex.Handle((inner) => {
                    if (inner is OperationCanceledException) {
                        // ...handle task cancellation...
                        return true;
                    } else {
                        // this is an exception we don't know how
                        // to handle, so return false
                        return false;
                    }
                });
            }

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
