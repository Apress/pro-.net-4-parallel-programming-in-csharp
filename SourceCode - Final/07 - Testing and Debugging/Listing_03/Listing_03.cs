using System;
using System.Threading.Tasks;

namespace Listing_03 {
    class Listing_03 {

        static void Main(string[] args) {

            // create a lock object
            object lockObj = new object();

            // create a sequence of tasks that acquire 
            // the lock in order to perform a
            // time-expensive function over and over
            Task[] tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++) {
                tasks[i] = Task.Factory.StartNew(() => {
                    // acquire the lock
                    lock (lockObj) {
                        // perform some work
                        for (int index = 0; index < 50000000; index++) {
                            Math.Pow(index, 2);
                        }
                    }
                });
            }

            // wait for the tasks to complete
            Task.WaitAll(tasks);
        }
    }
}