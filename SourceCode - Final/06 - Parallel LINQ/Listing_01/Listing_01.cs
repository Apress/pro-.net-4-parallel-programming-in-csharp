using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing_01 {
    class Listing_01 {
        static void Main(string[] args) {

            int[] sourceData = new int[100];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            IEnumerable<int> results =
                from item in sourceData.AsParallel()
                where item % 2 == 0
                select item;

            foreach (int item in results) {
                Console.WriteLine("Item {0}", item);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
