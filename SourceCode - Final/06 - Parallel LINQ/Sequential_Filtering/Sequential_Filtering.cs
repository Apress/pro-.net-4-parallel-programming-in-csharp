using System;
using System.Linq;

namespace Sequential_Filtering {
    class Sequential_Filtering {
        static void Main(string[] args) {

            // create some source data
            int[] source1 = new int[100];
            for (int i = 0; i < source1.Length; i++) {
                source1[i] = i;
            }

            // perform the query - but note 
            // where the AsParallel call is
            var result = source1
                .Where(item => item % 2 == 0)
                .AsParallel()
                .Select(item => item);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
