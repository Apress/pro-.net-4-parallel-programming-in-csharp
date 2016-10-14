using System;
using System.Threading.Tasks;

namespace Local_Variable_Evaluation {

    class Local_Variable_Evaluation {

        static void Main(string[] args) {

            // create and start the "bad" tasks
            for (int i = 0; i < 5; i++) {
                Task.Factory.StartNew(() => {
                    // write out a message that uses the loop counter
                    Console.WriteLine("Task {0} has counter value: {1}", Task.CurrentId, i);
                });
            }

            // create and start the "good" tasks
            for (int i = 0; i < 5; i++) {
                Task.Factory.StartNew((stateObj) => {
                    // cast the state object to an int
                    int loopValue = (int)stateObj;
                    // write out a message that uses the loop counter
                    Console.WriteLine("Task {0} has counter value: {1}", Task.CurrentId, loopValue);
                }, i);
            }

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
