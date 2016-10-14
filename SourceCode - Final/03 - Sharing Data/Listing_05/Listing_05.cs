using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_05 {

    class BankAccount {
        public int Balance {
            get;
            set;
        }
    }

    class Listing_05 {

        static void Main(string[] args) {

            // create the bank account instance
            BankAccount account = new BankAccount();

            // create an array of tasks
            Task<int>[] tasks = new Task<int>[10];

            // create the thread local storage
            ThreadLocal<int> tls = new ThreadLocal<int>(() => {
                Console.WriteLine("Value factory called for value: {0}",
                   account.Balance);
                return account.Balance;
            });

            for (int i = 0; i < 10; i++) {
                // create a new task
                tasks[i] = new Task<int>(() => {

                    // enter a loop for 1000 balance updates
                    for (int j = 0; j < 1000; j++) {
                        // update the TLS balance
                        tls.Value++;
                    }

                    // return the updated balance
                    return tls.Value;

                });

                // start the new task
                tasks[i].Start();
            }

            // get the result from each task and add it to
            // the balance
            for (int i = 0; i < 10; i++) {
                account.Balance += tasks[i].Result;
            }

            // write out the counter value
            Console.WriteLine("Expected value {0}, Balance: {1}",
                10000, account.Balance);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
