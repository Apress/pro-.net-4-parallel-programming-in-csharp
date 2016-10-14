using System;
using System.Threading.Tasks;

namespace Listing_03 {

    class Listing_03 {

        static void Main(string[] args) {

            Parallel.For(0, 10, index => {
                Console.WriteLine("Task ID {0} processing index: {1}",
                    Task.CurrentId, index);
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();  
        }
    }
}
