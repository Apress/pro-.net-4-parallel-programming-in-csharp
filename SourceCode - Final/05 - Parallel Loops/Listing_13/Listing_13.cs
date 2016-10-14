using System;
using System.Threading.Tasks;

namespace Listing_13 {

    class Transaction {
        public int Amount {
            get;
            set;
        }
    }

    class Listing_13 {

        static void Main(string[] args) {

            // create a random number generator
            Random rnd = new Random();

            // set the number of items created 
            int itemsPerMonth = 100000;

            // create the source data
            Transaction[] sourceData = new Transaction[12 * itemsPerMonth];
            for (int i = 0; i < 12 * itemsPerMonth; i++) {
                sourceData[i] = new Transaction() { Amount = rnd.Next(-400, 500) };
            }

            // create the results array
            int[] monthlyBalances = new int[12];
            
            for (int currentMonth = 0; currentMonth < 12; currentMonth++) {
                // perform the parallel loop on the current month's data
                Parallel.For(
                    currentMonth * itemsPerMonth, 
                    (currentMonth + 1) * itemsPerMonth, 
                    new ParallelOptions(), 
                    () => 0, 
                    (index, loopstate, tlsBalance) => {
                        return tlsBalance += sourceData[index].Amount;
                    }, 
                    tlsBalance => monthlyBalances[currentMonth] += tlsBalance);
                // end of parallel for
                // add the previous month's balance
                if (currentMonth > 0) monthlyBalances[currentMonth] += monthlyBalances[currentMonth - 1];
            }

            for (int i = 0; i < monthlyBalances.Length; i++) {
                Console.WriteLine("Month {0} - Balance: {1}", i, monthlyBalances[i]);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();            
        }
    }
}
