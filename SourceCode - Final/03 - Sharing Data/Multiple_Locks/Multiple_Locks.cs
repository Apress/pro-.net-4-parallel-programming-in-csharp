using System;
using System.Threading.Tasks;

namespace Multiple_Locks {

    class BankAccount {
        public int Balance {
            get;
            set;
        }
    }

    class Multiple_Locks {

        static void Main(string[] args) {

            // create the bank account instance
            BankAccount account = new BankAccount();

            // create two lock objects
            object lock1 = new object();
            object lock2 = new object();

            // create an array of tasks
            Task[] tasks = new Task[10];

            // create five tasks that use the first lock object
            for (int i = 0; i < 5; i++) {
                // create a new task
                tasks[i] = new Task(() => {
                    // enter a loop for 1000 balance updates
                    for (int j = 0; j < 1000; j++) {
                        lock (lock1) {
                            // update the balance
                            account.Balance++;
                        }
                    }
                });
            }

            // create five tasks that use the second lock object
            for (int i = 5; i < 10; i++) {
                // create a new task
                tasks[i] = new Task(() => {
                    // enter a loop for 1000 balance updates
                    for (int j = 0; j < 1000; j++) {
                        lock (lock2) {
                            // update the balance
                            account.Balance++;
                        }
                    }
                });
            }

            // start the tasks
            foreach (Task task in tasks) {
                task.Start();
            }

            // wait for all of the tasks to complete
            Task.WaitAll(tasks);

            // write out the counter value
            Console.WriteLine("Expected value {0}, Balance: {1}",
                10000, account.Balance);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
