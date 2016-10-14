using System;
using System.Threading.Tasks;

namespace Listing_05 {

    class Listing_05 {

        static void Main(string[] args) {

            // create the task
            Task<int> task1 = new Task<int>(() => {
                int sum = 0;
                for (int i = 0; i < 100; i++) {
                    sum += i;
                }
                return sum;
            });

            // start the task
            task1.Start();

            // write out the result
            Console.WriteLine("Result 1: {0}", task1.Result);

            // create the task using state
            Task<int> task2 = new Task<int>(obj => {
                int sum = 0;
                int max = (int)obj;
                for (int i = 0; i < max; i++) {
                    sum += i;
                }
                return sum;
            }, 100);

            // start the task
            task2.Start();

            // write out the result
            Console.WriteLine("Result 2: {0}", task2.Result);

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
