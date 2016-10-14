using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_15 {

    class Listing_15 {

        static void Main(string[] args) {

            // create a CountDownEvent with a condition
            // counter of 5
            CountdownEvent cdevent = new CountdownEvent(5);

            // create a Random that we will use to generate
            // sleep intervals
            Random rnd = new Random();

            // create 5 tasks, each of which will wait for
            // a random period and then signal the event
            Task[] tasks = new Task[6];
            for (int i = 0; i < tasks.Length; i++) {
                // create the new task
                tasks[i] = new Task(() => {
                    // put the task to sleep for a random period
                    // up to one second
                    Thread.Sleep(rnd.Next(500, 1000));
                    // signal the event
                    Console.WriteLine("Task {0} signalling event", Task.CurrentId);
                    cdevent.Signal();
                });
            };

            // create the final task, which will rendezous with the other 5
            // using the count down event
            tasks[5] = new Task(() => {
                // wait on the event
                Console.WriteLine("Rendezvous task waiting");
                cdevent.Wait();
                Console.WriteLine("Event has been set");

            });

            // start the tasks
            foreach (Task t in tasks) {
                t.Start();
            }

            Task.WaitAll(tasks);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
