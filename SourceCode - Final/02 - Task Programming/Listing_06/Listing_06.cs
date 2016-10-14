using System;
using System.Threading.Tasks;

namespace Listing_06 {

    class Listing_06 {

        static void Main(string[] args) {

            // create the task
            Task<int> task1 = Task.Factory.StartNew<int>(() => {
                int sum = 0;
                for (int i = 0; i < 100; i++) {
                    sum += i;
                }
                return sum;
            });

            // write out the result
            Console.WriteLine("Result 1: {0}", task1.Result);

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
