using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_08 {

    class BankAccount {
        public int Balance = 0;
    }

    class Listing_08 {

        static void Main(string[] args) {

            // create the bank account instance
            BankAccount account = new BankAccount();

            // create an array of tasks
            Task[] tasks = new Task[10];

            for (int i = 0; i < 10; i++) {
                // create a new task
                tasks[i] = new Task(() => {
                    // enter a loop for 1000 balance updates
                    for (int j = 0; j < 1000; j++) {
                        // update the balance
                        Interlocked.Increment(ref account.Balance);
                    }
                });
                // start the new task
                tasks[i].Start();
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
