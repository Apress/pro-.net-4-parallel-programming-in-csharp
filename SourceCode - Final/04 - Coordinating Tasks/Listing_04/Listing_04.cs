using System;
using System.Threading.Tasks;

namespace Listing_04 {

    class Listing_04 {

        static void Main(string[] args) {

            // create the first generation task
            Task firstGen = new Task(() => {
                Console.WriteLine("Message from first generation task");
                // comment out this line to stop the fault
                throw new Exception();
            });

            // create the second generation task - only to run on exception
            Task secondGen1 = firstGen.ContinueWith(antecedent => {
                // write out a message with the antecedent exception
                Console.WriteLine("Antecedent task faulted with type: {0}",
                    antecedent.Exception.GetType());
            }, TaskContinuationOptions.OnlyOnFaulted);

            // create the second generation task - only to run on no exception
            Task secondGen2 = firstGen.ContinueWith(antecedent => {
                Console.WriteLine("Antecedent task NOT faulted");
            }, TaskContinuationOptions.NotOnFaulted);

            // start the first generation task
            firstGen.Start();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
