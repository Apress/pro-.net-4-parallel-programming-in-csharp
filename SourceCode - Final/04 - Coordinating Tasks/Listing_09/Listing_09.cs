using System;
using System.Threading.Tasks;

namespace Listing_09 {

    class Listing_09 {

        static void Main(string[] args) {

            // create a first generation task
            Task gen1 = new Task(() => {
                // write out a message
                Console.WriteLine("First generation task");
            });

            // create a second generation task
            Task gen2 = gen1.ContinueWith(antecedent => {
                // write out a message
                Console.WriteLine("Second generation task - throws exception");
                throw new Exception();
            });

            // create a third generation task
            Task gen3 = gen2.ContinueWith(antecedent => {
                // check to see if the antecedent threw an exception
                if (antecedent.Status == TaskStatus.Faulted) {
                    // get and rethrow the antecedent exception
                    throw antecedent.Exception.InnerException;
                }
                // write out a message
                Console.WriteLine("Third generation task");
            });

            // start the first gen task
            gen1.Start();

            try {
                // wait for the last task in the chain to complete
                gen3.Wait();
            } catch (AggregateException ex) {
                ex.Handle(inner => {
                    Console.WriteLine("Handled exception of type: {0}", inner.GetType());
                    return true;
                });
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
