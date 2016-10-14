using System;
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

            for (int i = 0; i < 10; i++) {
                // create a new task
                tasks[i] = new Task<int>((stateObject) => {

                    // get the state object
                    int isolatedBalance = (int)stateObject;

                    // enter a loop for 1000 balance updates
                    for (int j = 0; j < 1000; j++) {
                        // update the balance
                        isolatedBalance++;
                    }

                    // return the updated balance
                    return isolatedBalance;

                }, account.Balance);
            }

            // set up a multi-task continuation
            Task continuation = Task.Factory.ContinueWhenAll<int>(tasks, antecedents => {
                // run through and sum the individual balances
                foreach (Task<int> t in antecedents) {
                    account.Balance += t.Result;
                }
            });

            // start the atecedent tasks
            foreach (Task t in tasks) {
                t.Start();
            }

            // wait for the contination task to complete
            continuation.Wait();

            // write out the counter value
            Console.WriteLine("Expected value {0}, Balance: {1}", 10000, account.Balance);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
