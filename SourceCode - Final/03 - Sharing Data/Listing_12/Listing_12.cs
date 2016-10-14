using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_12 {

    class BankAccount {
        public int Balance {
            get;
            set;
        }
    }

    class Listing_12 {

        static void Main(string[] args) {

            // create the bank account instances
            BankAccount account1 = new BankAccount();
            BankAccount account2 = new BankAccount();

            // create the mutexes
            Mutex mutex1 = new Mutex();
            Mutex mutex2 = new Mutex();

            // create a new task to update the first account
            Task task1 = new Task(() => {
                // enter a loop for 1000 balance updates
                for (int j = 0; j < 1000; j++) {
                    // acquire the lock for the account
                    bool lockAcquired = mutex1.WaitOne();
                    try {
                        // update the balance
                        account1.Balance++;
                    } finally {
                        if (lockAcquired) mutex1.ReleaseMutex();
                    }
                }
            });

            // create a new task to update the first account
            Task task2 = new Task(() => {
                // enter a loop for 1000 balance updates
                for (int j = 0; j < 1000; j++) {
                    // acquire the lock for the account
                    bool lockAcquired = mutex2.WaitOne(); ;
                    try {
                        // update the balance
                        account2.Balance += 2;
                    } finally {
                        if (lockAcquired) mutex2.ReleaseMutex();
                    }
                }
            });

            // create a new task to update the first account
            Task task3 = new Task(() => {
                // enter a loop for 1000 balance updates
                for (int j = 0; j < 1000; j++) {
                    // acquire the locks for both accounts
                    bool lockAcquired = Mutex.WaitAll(new WaitHandle[] { mutex1, mutex2 });
                    try {
                        // simulate a transfer between accounts
                        account1.Balance++;
                        account2.Balance--;
                    } finally {
                        if (lockAcquired) {
                            mutex1.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }
                }
            });

            // start the tasks
            task1.Start();
            task2.Start();
            task3.Start();

            // wait for the tasks to complete
            Task.WaitAll(task1, task2, task3);

            // write out the counter value
            Console.WriteLine("Account1 balance {0}, Account2 balance: {1}",
                account1.Balance, account2.Balance);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
