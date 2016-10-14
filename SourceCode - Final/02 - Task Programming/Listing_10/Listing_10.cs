using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_10 {

    class Listing_10 {

        static void Main(string[] args) {

            // create the cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();
            // create the cancellation token
            CancellationToken token = tokenSource.Token;

            // create the tasks
            Task task1 = new Task(() => {
                for (int i = 0; i < int.MaxValue; i++) {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine("Task 1 - Int value {0}", i);
                }
            }, token);

            Task task2 = new Task(() => {
                for (int i = 0; i < int.MaxValue; i++) {
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine("Task 2 - Int value {0}", i);
                }
            }, token);

            // wait for input before we start the tasks
            Console.WriteLine("Press enter to start tasks");
            Console.WriteLine("Press enter again to cancel tasks");
            Console.ReadLine();

            // start the tasks
            task1.Start();
            task2.Start();

            // read a line from the console.
            Console.ReadLine();

            // cancel the task
            Console.WriteLine("Cancelling tasks");
            tokenSource.Cancel();

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
