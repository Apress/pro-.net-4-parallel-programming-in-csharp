using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_21 {

    class Listing_21 {

        static void Main(string[] args) {

            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create a task that throws an exception
            Task task1 = new Task(() => {
                throw new NullReferenceException();
            });

            Task task2 = new Task(() => {
                // wait until we are cancelled
                tokenSource.Token.WaitHandle.WaitOne(-1);
                throw new OperationCanceledException();
            }, tokenSource.Token);

            // start the tasks
            task1.Start();
            task2.Start();

            // cancel the token
            tokenSource.Cancel();

            // wait for the tasks, ignoring the exceptions
            try {
                Task.WaitAll(task1, task2);
            } catch (AggregateException) {
                // ignore exceptions
            }

            // write out the details of the task exception
            Console.WriteLine("Task 1 completed: {0}", task1.IsCompleted);
            Console.WriteLine("Task 1 faulted: {0}", task1.IsFaulted);
            Console.WriteLine("Task 1 cancelled: {0}", task1.IsCanceled);
            Console.WriteLine(task1.Exception);

            // write out the details of the task exception
            Console.WriteLine("Task 2 completed: {0}", task2.IsCompleted);
            Console.WriteLine("Task 2 faulted: {0}", task2.IsFaulted);
            Console.WriteLine("Task 2 cancelled: {0}", task2.IsCanceled);
            Console.WriteLine(task2.Exception);

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
