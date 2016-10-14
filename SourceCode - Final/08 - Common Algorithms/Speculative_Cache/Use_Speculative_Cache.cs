using System;
using System.Linq;

namespace Speculative_Cache {

    class Use_Speculative_Cache {
        static void Main(string[] args) {

            // create a new instance of the cache
            Speculative_Cache<int, double> cache
                = new Speculative_Cache<int, double>(
                    key1 => {
                        Console.WriteLine("Created value for key {0}", key1);
                        return Math.Pow(key1, 2);
                    },
                    key2 => Enumerable.Range(key2 + 1, 5).ToArray());

            // request some values from the cache
            for (int i = 0; i < 100; i++) {
                double value = cache.GetValue(i);
                Console.WriteLine("Got result {0} for key {1}", value, i);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
