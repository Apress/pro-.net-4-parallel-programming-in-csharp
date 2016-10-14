using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Listing_21 {

    class BankAccount {
        public int Balance {
            get;
            set;
        }
    }

    class Listing_21 {

        static void Main(string[] args) {

            // create the bank account instance
            BankAccount account = new BankAccount();

            // create a shared dictionary
            ConcurrentDictionary<object, int> sharedDict
                = new ConcurrentDictionary<object, int>();

            // create tasks to process the list
            Task<int>[] tasks = new Task<int>[10];
            for (int i = 0; i < tasks.Length; i++) {

                // put the initial value into the dictionary
                sharedDict.TryAdd(i, account.Balance);

                // create the new task
                tasks[i] = new Task<int>((keyObj) => {

                    // define variables for use in the loop
                    int currentValue;
                    bool gotValue;

                    // enter a loop for 1000 balance updates
                    for (int j = 0; j < 1000; j++) {
                        // get the current value from the dictionary
                        gotValue = sharedDict.TryGetValue(keyObj, out currentValue);
                        // increment the value and update the dictionary entry
                        sharedDict.TryUpdate(keyObj, currentValue + 1, currentValue);
                    }

                    // define the final result
                    int result;
                    // get our result from the dictionary
                    gotValue = sharedDict.TryGetValue(keyObj, out result);
                    // return the result value if we got one
                    if (gotValue) {
                        return result;
                    } else {
                        // there was no result available - we have encountered a problem
                        throw new Exception(
                            String.Format("No data item available for key {0}", keyObj));
                    }
                }, i);

                // start the new task
                tasks[i].Start();
            }

            // update the balance of the account using the task results
            for (int i = 0; i < tasks.Length; i++) {
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
