using System;
using System.Threading;
using System.Threading.Tasks;
using Listing_21;

namespace Listing_22 {

    class Listing_22 {

        static void Main(string[] args) {

            // get the processor count for the system
            int procCount = System.Environment.ProcessorCount;

            // create a custom scheduler
            CustomScheduler scheduler = new CustomScheduler(procCount);

            Console.WriteLine("Custom scheduler ID: {0}", scheduler.Id);
            Console.WriteLine("Default scheduler ID: {0}", TaskScheduler.Default.Id);

            // create a cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create a task
            Task task1 = new Task(() => {

                Console.WriteLine("Task {0} executed by scheduler {1}",
                        Task.CurrentId, TaskScheduler.Current.Id);

                // create a child task - this will use the same
                // scheduler as its parent
                Task.Factory.StartNew(() => {
                    Console.WriteLine("Task {0} executed by scheduler {1}",
                        Task.CurrentId, TaskScheduler.Current.Id);
                });

                // create a child and specify the default scheduler
                Task.Factory.StartNew(() => {
                    Console.WriteLine("Task {0} executed by scheduler {1}",
                        Task.CurrentId, TaskScheduler.Current.Id);
                }, tokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);

            });

            // start the task using the custom scheduler
            task1.Start(scheduler);

            // create a continuation - this will use the default scheduler
            task1.ContinueWith(antecedent => {
                Console.WriteLine("Task {0} executed by scheduler {1}",
                    Task.CurrentId, TaskScheduler.Current.Id);
            });

            // create a continuation using the custom scheduler
            task1.ContinueWith(antecedent => {
                Console.WriteLine("Task {0} executed by scheduler {1}",
                    Task.CurrentId, TaskScheduler.Current.Id);
            }, scheduler);
        }
    }
}
