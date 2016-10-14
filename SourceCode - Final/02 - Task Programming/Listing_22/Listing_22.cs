using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_22 {

    class Listing_22 {

        static void Main(string[] args) {

            // create the new escalation policy
            TaskScheduler.UnobservedTaskException +=
                (object sender, UnobservedTaskExceptionEventArgs eventArgs) => {
                    // mark the exception as being handled
                    eventArgs.SetObserved();
                    // get the aggregate exception and process the contents
                    ((AggregateException)eventArgs.Exception).Handle(ex => {
                        // write the type of the exception to the console
                        Console.WriteLine("Exception type: {0}", ex.GetType());
                        return true;
                    });
                };

            // create tasks that will throw an exception
            Task task1 = new Task(() => {
                throw new NullReferenceException();
            });
            Task task2 = new Task(() => {
                throw new ArgumentOutOfRangeException();
            });

            // start the tasks
            task1.Start(); task2.Start();

            // wait for the tasks to complete - but do so
            // without calling any of the trigger members
            // so that the exceptions remain unhandled
            while (!task1.IsCompleted || !task2.IsCompleted) {
                Thread.Sleep(500);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish and finalize tasks");
            Console.ReadLine();
        }
    }
}
