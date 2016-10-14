using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_07 {

    class Listing_07 {

        static void Main(string[] args) {

            // create a cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();

            // create the antecedent task
            Task task = new Task(() => {
                // write out a message
                Console.WriteLine("Antecedent running");
                // wait indefinately on the token wait handle
                tokenSource.Token.WaitHandle.WaitOne();
                // handle the cancellation exception
                tokenSource.Token.ThrowIfCancellationRequested();
            }, tokenSource.Token);

            // create a selective continuation 
            Task neverScheduled = task.ContinueWith(antecedent => {
                // write out a message
                Console.WriteLine("This task will never be scheduled");
            }, tokenSource.Token);

            // create a bad selective contination 
            Task badSelective = task.ContinueWith(antecedent => {
                // write out a message
                Console.WriteLine("This task will never be scheduled");
            }, tokenSource.Token, TaskContinuationOptions.OnlyOnCanceled,
            TaskScheduler.Current);

            // create a good selective contiuation
            Task continuation = task.ContinueWith(antecedent => {
                // write out a message
                Console.WriteLine("Continuation running");
            }, TaskContinuationOptions.OnlyOnCanceled);

            // start the task
            task.Start();

            // prompt the user so they can cancel the token
            Console.WriteLine("Press enter to cancel token");
            Console.ReadLine();
            // cancel the token source
            tokenSource.Cancel();

            // wait for the good continuation to complete
            continuation.Wait();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
