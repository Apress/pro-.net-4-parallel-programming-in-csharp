using System;
using System.Threading.Tasks;

namespace Loop_Body_Data_Races {
    class Loop_Body_Data_Races {
        static void Main(string[] args) {

            // create the shared data value
            double total = 0;

            // perform a parallel loop
            Parallel.For(
                0,
                100000,
                item => {
                    // add the square of the current 
                    // value to the running total
                    total += Math.Pow(item, 2);
                });

            Console.WriteLine("Expected result: 333328333350000");
            Console.WriteLine("Actual result: {0}", total);
        }
    }
}
