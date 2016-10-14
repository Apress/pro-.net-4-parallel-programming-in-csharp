using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lock_Acquisition_Order {

    class Lock_Acquisition_Order {

        static void Main(string[] args) {

            // create two lock objects
            object lock1 = new object();
            object lock2 = new object();

            // create a task that acquires lock 1
            // and then lock 2
            Task task1 = new Task(() => {
                lock (lock1) {
                    Console.WriteLine("Task 1 acquired lock 1");
                    Thread.Sleep(500);
                    lock (lock2) {
                        Console.WriteLine("Task 1 acquired lock 2");
                    }
                }
            });

            // create a task that acquires lock 2
            // and then lock 1
            Task task2 = new Task(() => {
                lock (lock2) {
                    Console.WriteLine("Task 2 acquired lock 2");
                    Thread.Sleep(500);
                    lock (lock1) {
                        Console.WriteLine("Task 2 acquired lock 1");
                    }
                }
            });

            // start the tasks
            task1.Start();
            task2.Start();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
