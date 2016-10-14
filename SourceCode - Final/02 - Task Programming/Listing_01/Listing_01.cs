using System;
using System.Threading.Tasks;

namespace Listing_01 {

    class Listing_01 {

        static void Main(string[] args) {

            Task.Factory.StartNew(() => {
                Console.WriteLine("Hello World");
            });

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
