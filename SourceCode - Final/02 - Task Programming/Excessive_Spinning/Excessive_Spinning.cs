using System;
using System.Threading;
using System.Threading.Tasks;

namespace Excessive_Spinning {

    class Excessive_Spinning {

        static void Main(string[] args) {

            // create a cancellation token source
            CancellationTokenSource tokenSource =
                new CancellationTokenSource();

            // create the first task
            Task t1 = Task.Factory.StartNew(() => {
                Console.WriteLine("Task 1 waiting for cancellation");
                tokenSource.Token.WaitHandle.WaitOne();
                Console.WriteLine("Task 1 cancelled");
                tokenSource.Token.ThrowIfCancellationRequested();
            }, tokenSource.Token);

            // create the second task, which will use a code loop
            Task t2 = Task.Factory.StartNew(() => {
                // enter a loop until t1 is cancelled
                while (!t1.Status.HasFlag(TaskStatus.Canceled)) {
                    // do nothing - this is a code loop
                }
                Console.WriteLine("Task 2 exited code loop");
            });

            // create the third loop which will use spin waiting
            Task t3 = Task.Factory.StartNew(() => {
                // enter the spin wait loop
                while (t1.Status != TaskStatus.Canceled) {
                    Thread.SpinWait(1000);
                }
                Console.WriteLine("Task 3 exited spin wait loop");
            });

            // prompt the user to hit enter to cancel
            Console.WriteLine("Press enter to cancel token");
            Console.ReadLine();
            tokenSource.Cancel();

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
