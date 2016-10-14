using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_15 {

    class Listing_15 {

        static void Main(string[] args) {

            // create the reader-writer lock
            ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

            // create a cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create an array of tasks
            Task[] tasks = new Task[5];

            for (int i = 0; i < 5; i++) {
                // create a new task
                tasks[i] = new Task(() => {
                    while (true) {
                        // acqure the read lock
                        rwlock.EnterReadLock();
                        // we now have the lock
                        Console.WriteLine("Read lock acquired - count: {0}",
                            rwlock.CurrentReadCount);
                        // wait - this simulates a read operation
                        tokenSource.Token.WaitHandle.WaitOne(1000);
                        // release the read lock
                        rwlock.ExitReadLock();
                        Console.WriteLine("Read lock released - count {0}",
                            rwlock.CurrentReadCount);
                        // check for cancellation
                        tokenSource.Token.ThrowIfCancellationRequested();
                    }
                }, tokenSource.Token);
                // start the new task
                tasks[i].Start();
            }

            // prompt the user
            Console.WriteLine("Press enter to acquire write lock");
            // wait for the user to press enter 
            Console.ReadLine();

            // acquire the write lock
            Console.WriteLine("Requesting write lock");
            rwlock.EnterWriteLock();

            Console.WriteLine("Write lock acquired");
            Console.WriteLine("Press enter to release write lock");
            // wait for the user to press enter 
            Console.ReadLine();
            // release the write lock
            rwlock.ExitWriteLock();

            // wait for 2 seconds and then cancel the tasks
            tokenSource.Token.WaitHandle.WaitOne(2000);
            tokenSource.Cancel();

            try {
                // wait for the tasks to complete
                Task.WaitAll(tasks);
            } catch (AggregateException) {
                // do nothing
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
