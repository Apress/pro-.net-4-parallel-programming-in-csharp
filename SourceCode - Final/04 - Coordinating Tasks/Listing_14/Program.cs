using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_14 {

    class Listing_14 {

        static void Main(string[] args) {

            // create a barrier
            Barrier barrier = new Barrier(2);

            // create a cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();

            // create a task that will complete
            Task.Factory.StartNew(() => {
                Console.WriteLine("Good task starting phase 0");
                barrier.SignalAndWait(tokenSource.Token);
                Console.WriteLine("Good task starting phase 1");
                barrier.SignalAndWait(tokenSource.Token);
            }, tokenSource.Token);

            // create a task that will throw an exception
            // with a selective continuation that will reduce the 
            // particpant count in the barrier
            Task.Factory.StartNew(() => {
                Console.WriteLine("Bad task 1 throwing exception");
                throw new Exception();

            }, tokenSource.Token).ContinueWith(antecedent => {
                // reduce the particpant count
                Console.WriteLine("Cancelling the token");
                tokenSource.Cancel();
            }, TaskContinuationOptions.OnlyOnFaulted);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
