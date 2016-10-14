using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_16 {
    class Listing_16 {

        static void Main(string[] args) {

            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            // create the cancellation token
            CancellationToken token = tokenSource.Token;

            // create and start the first task, which we will let run fully
            Task task = createTask(token);
            task.Start();

            // wait for the task
            Console.WriteLine("Waiting for task to complete.");
            task.Wait();
            Console.WriteLine("Task Completed.");

            // create and start another task
            task = createTask(token);
            task.Start();

            Console.WriteLine("Waiting 2 secs for task to complete.");
            bool completed = task.Wait(2000);
            Console.WriteLine("Wait ended - task completed: {0}", completed);

            // create and start another task
            task = createTask(token);
            task.Start();

            Console.WriteLine("Waiting 2 secs for task to complete.");
            completed = task.Wait(2000, token);
            Console.WriteLine("Wait ended - task completed: {0} task cancelled {1}", 
                completed, task.IsCanceled);

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }

        static Task createTask(CancellationToken token) {
            return new Task(() => {
                for (int i = 0; i < 5; i++) {
                    // check for task cancellation
                    token.ThrowIfCancellationRequested();
                    // print out a message
                    Console.WriteLine("Task - Int value {0}", i);
                    // put the task to sleep for 1 second
                    token.WaitHandle.WaitOne(1000);
                }
            }, token);
        }
    }
}
