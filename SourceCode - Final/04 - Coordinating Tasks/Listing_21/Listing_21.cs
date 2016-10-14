using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_21 {

    public class CustomScheduler : TaskScheduler, IDisposable {
        private BlockingCollection<Task> taskQueue;
        private Thread[] threads;

        public CustomScheduler(int concurrency) {
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
            // only allow inline execution if the executing thread is one 
            // belonging to this scheduler
            if (threads.Contains(Thread.CurrentThread)) {
                return TryExecuteTask(task);
            } else {
                return false;
            }
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
}
