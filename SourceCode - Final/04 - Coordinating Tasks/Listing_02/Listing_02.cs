using System;
using System.Threading.Tasks;

namespace Listing_02 {

    class BankAccount {
        public int Balance {
            get;
            set;
        }
    }

    class Listing_02 {

        static void Main(string[] args) {

            Task<BankAccount> task = new Task<BankAccount>(() => {
                // create a new bank account
                BankAccount account = new BankAccount();
                // enter a loop
                for (int i = 0; i < 1000; i++) {
                    // increment the account total
                    account.Balance++;
                }
                // return the bank account
                return account;
            });

            Task<int> continuationTask
                = task.ContinueWith<int>((Task<BankAccount> antecedent) => {
                    Console.WriteLine("Interim Balance: {0}", antecedent.Result.Balance);
                    return antecedent.Result.Balance * 2;
                });

            // start the task
            task.Start();

            Console.WriteLine("Final balance: {0}", continuationTask.Result);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
