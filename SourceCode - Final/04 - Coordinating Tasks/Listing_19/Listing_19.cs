using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Listing_19 {

    class BankAccount {
        public int Balance {
            get;
            set;
        }
    }

    class Deposit {
        public int Amount {
            get;
            set;
        }
    }

    class Listing_19 {

        static void Main(string[] args) {


            // create the blocking collection
            BlockingCollection<Deposit> blockingCollection
                = new BlockingCollection<Deposit>();

            // create and start the producers, which will generate
            // deposits and place them into the collection
            Task[] producers = new Task[3];
            for (int i = 0; i < 3; i++) {
                producers[i] = Task.Factory.StartNew(() => {
                    // create a series of deposits
                    for (int j = 0; j < 20; j++) {
                        // create the transfer
                        Deposit deposit = new Deposit { Amount = 100 };
                        // place the transfer in the collection
                        blockingCollection.Add(deposit);
                    }
                });
            };

            // create a many to one continuation that will signal
            // the end of production to the consumer
            Task.Factory.ContinueWhenAll(producers, antecedents => {
                // signal that production has ended
                Console.WriteLine("Signalling production end");
                blockingCollection.CompleteAdding();
            });

            // create a bank account
            BankAccount account = new BankAccount();

            // create the consumer, which will update
            // the balance based on the deposits
            Task consumer = Task.Factory.StartNew(() => {
                while (!blockingCollection.IsCompleted) {
                    Deposit deposit;
                    // try to take the next item 
                    if (blockingCollection.TryTake(out deposit)) {
                        // update the balance with the transfer amount
                        account.Balance += deposit.Amount;
                    }
                }
                // print out the final balance
                Console.WriteLine("Final Balance: {0}", account.Balance);
            });

            // wait for the consumer to finish
            consumer.Wait();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
