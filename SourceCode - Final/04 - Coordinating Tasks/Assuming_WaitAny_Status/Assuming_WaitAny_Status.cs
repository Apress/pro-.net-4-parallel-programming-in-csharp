using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assuming_WaitAny_Status {
    class Assuming_WaitAny_Status {
        static void Main(string[] args) {

            // create a cancellation token source
            CancellationTokenSource tokenSource
                = new CancellationTokenSource();

            Task<int>[] tasks = new Task<int>[2];

            tasks[0] = new Task<int>(() => {
                while (true) {
                    // sleep 
                    Thread.Sleep(100000);
                }
            });

            tasks[1] = new Task<int>(() => {
                // wait for the token to be cancelled
                tokenSource.Token.WaitHandle.WaitOne();
                // throw a cancellation exceptioon
                tokenSource.Token.ThrowIfCancellationRequested();
                // return a result to satisfy the compiler
                return 200;
            }, tokenSource.Token);

            Task.Factory.ContinueWhenAny(tasks, (Task<int> antecedent) => {
                Console.WriteLine("Result of antecedent is {0}", 
                    antecedent.Result);
            });

            // start the tasks
            tasks[1].Start();
            tasks[0].Start();

            // prompt the user and cancel the token
            Console.WriteLine("Press enter to cancel token");
            Console.ReadLine();
            tokenSource.Cancel();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
