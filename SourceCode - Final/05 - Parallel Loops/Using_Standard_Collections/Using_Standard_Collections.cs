using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Using_Standard_Collections {
    class Using_Standard_Collections {
        static void Main(string[] args) {

            // create some source data
            int[] sourceData = new int[10000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // create a list to hold the results
            List<int> resultData = new List<int>();

            Parallel.ForEach(sourceData, item => {
                resultData.Add(item);
            });

            Console.WriteLine("Results {0}", resultData.Count);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
