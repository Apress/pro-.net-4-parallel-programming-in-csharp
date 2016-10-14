using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Listing_04 {

    class Listing_04 {

        static void Main(string[] args) {

            // create a collection of strings
            List<string> dataList = new List<string> {
                "the", "quick", "brown", "fox", "jumps", "etc"
            };

            // process the elements of the collection
            // using a parallel foreach loop
            Parallel.ForEach(dataList, item => {
                Console.WriteLine("Item {0} has {1} characters",
                    item, item.Length);
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();  
        }
    }
}
