using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_12 {

    class Listing_12 {

        static void Main(string[] args) {

            // create the cancellation token source
            CancellationTokenSource tokenSource1 = new CancellationTokenSource();
            // create the cancellation token
            CancellationToken token1 = tokenSource1.Token;

            // create the first task, which we will let run fully
            Task task1 = new Task(() => {
                for (int i = 0; i < 10; i++) {
                    token1.ThrowIfCancellationRequested();
                    Console.WriteLine("Task 1 - Int value {0}", i);
                }
            }, token1);

            // create the second cancellation token source
            CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            // create the cancellation token
            CancellationToken token2 = tokenSource2.Token;

            // create the second task, which we will cancel
            Task task2 = new Task(() => {
                for (int i = 0; i < int.MaxValue; i++) {
                    token2.ThrowIfCancellationRequested();
                    Console.WriteLine("Task 2 - Int value {0}", i);
                }
            }, token2);

            // start all of the tasks
            task1.Start();
            task2.Start();

            // cancel the second token source
            tokenSource2.Cancel();

            // write out the cancellation detail of each task
            Console.WriteLine("Task 1 cancelled? {0}", task1.IsCanceled);
            Console.WriteLine("Task 2 cancelled? {0}", task2.IsCanceled);

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();

        }
    }
}
