using System;

namespace Listing_02 {

    class ImmutableBankAccount {
        public const int AccountNumber = 123456;
        public readonly int Balance;

        public ImmutableBankAccount(int InitialBalance) {
            Balance = InitialBalance;
        }

        public ImmutableBankAccount() {
            Balance = 0;
        }
    }

    class Listing_02 {

        static void Main(string[] args) {

            // create a bank account with the default balance
            ImmutableBankAccount bankAccount1 = new ImmutableBankAccount();
            Console.WriteLine("Account Number: {0}, Account Balance: {1}",
                ImmutableBankAccount.AccountNumber, bankAccount1.Balance);

            // create a bank account with a starting balance
            ImmutableBankAccount bankAccount2 = new ImmutableBankAccount(200);
            Console.WriteLine("Account Number: {0}, Account Balance: {1}",
                ImmutableBankAccount.AccountNumber, bankAccount2.Balance);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
