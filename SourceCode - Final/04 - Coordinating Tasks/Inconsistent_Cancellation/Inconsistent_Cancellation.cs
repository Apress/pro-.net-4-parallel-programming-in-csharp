using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inconsistent_Cancellation {
    class Inconsistent_Cancellation {

        static void Main(string[] args) {

            // create a token source
            CancellationTokenSource tokenSource 
                = new CancellationTokenSource();

            // create the antecedent task
            Task<int> task1 = new Task<int>(() => {
                // wait for the token to be cancelled
                tokenSource.Token.WaitHandle.WaitOne();
                // throw the cancellation exception
                tokenSource.Token.ThrowIfCancellationRequested();
                // return the result - this code will
                // never be reached but is required to 
                // satisfy the compiler
                return 100;
            }, tokenSource.Token);

            // create a continuation
            // *** BAD CODE ***
            Task task2 = task1.ContinueWith((Task<int> antecedent) => {
                // read the antecedent result without checking
                // the status of the task
                Console.WriteLine("Antecedent result: {0}", antecedent.Result);
            });

            // create a continuation, but use a token
            Task task3 = task1.ContinueWith((Task<int> antecedent) => {
                // this task will never be executed 
            }, tokenSource.Token);

            // create a continuation that checks the status
            // of the antecedent
            Task task4 = task1.ContinueWith((Task<int> antecedent) => {
                if (antecedent.Status == TaskStatus.Canceled) {
                    Console.WriteLine("Antecedent cancelled");
                } else {
                    Console.WriteLine("Antecedent Result: {0}", antecedent.Result);
                }
            });

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
