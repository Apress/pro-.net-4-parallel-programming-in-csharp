using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlocked_Task_Scheduler {

    class Deadlocked_Task_Scheduler: TaskScheduler, IDisposable {
        private BlockingCollection<Task> taskQueue;
        private Thread[] threads;

        public Deadlocked_Task_Scheduler(int concurrency) {
            // initialize the collection and the thread array
            taskQueue = new BlockingCollection<Task>();
            threads = new Thread[concurrency];
            // create and start the threads
            for (int i = 0; i < threads.Length; i++) {
                (threads[i] = new Thread(() => {
                    // loop while the blocking collection is not
                    // complete and try to execute the next task
                    foreach (Task t in taskQueue.GetConsumingEnumerable()) {
                        TryExecuteTask(t);
                    }
                })).Start();
            }
        }

        protected override void QueueTask(Task task) {
            if (task.CreationOptions.HasFlag(TaskCreationOptions.LongRunning)) {
                // create a dedicated thread to execute this task
                new Thread(() => {
                    TryExecuteTask(task);
                }).Start();
            } else {
                // add the task to the queue
                taskQueue.Add(task);
            }
        }

        protected override bool TryExecuteTaskInline(Task task, 
            bool taskWasPreviouslyQueued) {
    
            // disallow all inline execution
            return false;
        }

        public override int MaximumConcurrencyLevel {
            get {
                return threads.Length;
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            return taskQueue.ToArray();
        }

        public void Dispose() {
            // mark the collection as complete
            taskQueue.CompleteAdding();
            // wait for each of the threads to finish
            foreach (Thread t in threads) {
                t.Join();
            }
        }
    }


    class Test_Deadlocked_Task_Scheduler {
        static void Main(string[] args) {

            // create the scheduler
            Deadlocked_Task_Scheduler scheduler 
                = new Deadlocked_Task_Scheduler(5);

            // create a token source
            CancellationTokenSource tokenSource = 
                new CancellationTokenSource();

            Task[] tasks = new Task[6];

            for (int i = 0; i < tasks.Length; i++) {
                tasks[i] = Task.Factory.StartNew((object stateObj) => {
                    int index = (int)stateObj;
                    if (index < tasks.Length - 1) {
                        Console.WriteLine("Task {0} waiting for {1}", index, index + 1);
                        tasks[index + 1].Wait();
                    }
                    Console.WriteLine("Task {0} complete", index);
                }, i, tokenSource.Token, TaskCreationOptions.None, scheduler);
            }

            Task.WaitAll(tasks);
            Console.WriteLine("All tasks complete");

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }

}
