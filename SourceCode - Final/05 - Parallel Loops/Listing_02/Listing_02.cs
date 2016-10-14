using System;
using System.Threading.Tasks;

namespace Listing_02 {

    class Listing_02 {

        static void Main(string[] args) {

            // invoke actions described by lambda expressions
            Parallel.Invoke(
                () => Console.WriteLine("Action 1"),
                () => Console.WriteLine("Action 2"),
                () => Console.WriteLine("Action 3"));

            // explicitly create an array of actions
            Action[] actions = new Action[3];
            actions[0] = new Action(() => Console.WriteLine("Action 4"));
            actions[1] = new Action(() => Console.WriteLine("Action 5"));
            actions[2] = new Action(() => Console.WriteLine("Action 6"));

            // invoke the actions array
            Parallel.Invoke(actions);

            // create the same effect using tasks explicitly
            Task parent = Task.Factory.StartNew(() => {
                foreach (Action action in actions) {
                    Task.Factory.StartNew(action, TaskCreationOptions.AttachedToParent);
                }
            });
            // wait for the task to finish
            parent.Wait();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();  
        }
    }
}
