using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_06 {

    class Listing_06 {

        static void Main(string[] args) {
            // specify the number of tasks
            int taskCount = 10;
            // create a countdown event so that
            // we can wait until all of the tasks 
            // have been created before breaking
            CountdownEvent cdEvent = new CountdownEvent(taskCount);
            // create the set of tasks
            Task[] tasks = new Task[taskCount];
            for (int i = 0; i < taskCount; i++) {
                tasks[i] = Task.Factory.StartNew((stateObj) => {
                    // signalthe countdown event
                    cdEvent.Signal();
                    // wait on the next task in the array
                    tasks[(((int)stateObj) + 1) % taskCount].Wait();
                }, i);
            }

            // wait for the count down event 
            cdEvent.Wait();
            // break if there is a debugger attached
            if (Debugger.IsAttached) {
                Debugger.Break();
            }
        }
    }
}
