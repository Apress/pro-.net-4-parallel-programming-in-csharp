using System;
using System.Threading;
using System.Threading.Tasks;

namespace Speculative_Selection {

    class Use_Speculative_Suggestion {

        static void Main(string[] args) {

            // create some sample functions
            Func<int, double> pFunction = value => {
                Random rnd = new Random();
                Thread.Sleep(rnd.Next(1, 2000));
                return Math.Pow(value, 2);
            };

            // create some sample functions
            Func<int, double> pFunction2 = value => {
                Random rnd = new Random();
                Thread.Sleep(rnd.Next(1, 1000));
                return Math.Pow(value, 2);
            };

            // define the callback
            Action<long, double> callback = (index, result) => {
                Console.WriteLine("Received result of {0} from function {1}",
                    result, index);
            };

            // speculative compute for some values
            for (int i = 0; i < 10; i++) {
                Speculative_Selection.Compute<int, double>(
                    i, 
                    callback, 
                    pFunction, pFunction2);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
