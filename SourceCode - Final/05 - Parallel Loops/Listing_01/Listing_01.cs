using System;
using System.Threading.Tasks;

namespace Listing_01 {

    class Listing_01 {

        static void Main(string[] args) {

            // create the arrays to hold the data and the results
            int[] dataItems = new int[100];
            double[] resultItems = new double[100];

            // create the data items
            for (int i = 0; i < dataItems.Length; i++) {
                dataItems[i] = i;
            }

            // process the data in a parallel for loop
            Parallel.For(0, dataItems.Length, (index, loopState) => {
                resultItems[index] =  Math.Pow(dataItems[index],2);
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();  
        }
    }
}
