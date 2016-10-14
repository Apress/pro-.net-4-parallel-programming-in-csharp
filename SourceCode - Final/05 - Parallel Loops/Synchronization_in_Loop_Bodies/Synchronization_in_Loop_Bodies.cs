using System;
using System.Threading.Tasks;

namespace Synchronization_in_Loop_Bodies {
    class Synchronization_in_Loop_Bodies {
        static void Main(string[] args) {

            // create the shared data value
            double total = 0;
            // create a lock object
            object lockObj = new object();
            
            // perform a parallel loop
            Parallel.For(
                0,
                100000,
                item => {
                    // get the lock on the shared value
                    lock (lockObj) {
                        // add the square of the current 
                        // value to the running total
                        total += Math.Pow(item, 2);
                    }
                });

        }
    }
}
