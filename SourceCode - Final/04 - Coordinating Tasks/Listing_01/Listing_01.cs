using System;
using System.Threading.Tasks;

namespace Listing_01 {

    class BankAccount {
        public int Balance {
            get;
            set;
        }
    }

    class Listing_01 {

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

            task.ContinueWith((Task<BankAccount> antecedent) => {
                Console.WriteLine("Final Balance: {0}", antecedent.Result.Balance);
            });

            // start the task
            task.Start();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
