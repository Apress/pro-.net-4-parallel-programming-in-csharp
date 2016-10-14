using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Decoupled_Console {
    class Decoupled_Console {
        // queue-based blocking collection
        private static BlockingCollection<Action> blockingQueue;
        // task that processes messages to the console
        private static Task messageWorker;

        static Decoupled_Console() {
            // create the blocking collection
            blockingQueue = new BlockingCollection<Action>();
            // create and start the worker task
            messageWorker = Task.Factory.StartNew(() => {
                foreach (Action action in blockingQueue.GetConsumingEnumerable()) {
                    // invoke the action
                    action.Invoke();
                }
            }, TaskCreationOptions.LongRunning);
        }

        public static void WriteLine(object value) {
            blockingQueue.Add(new Action(() =>
                Console.WriteLine(value)));
        }

        public static void WriteLine(string format, params object[] values) {
            blockingQueue.Add(new Action(() =>
                Console.WriteLine(format, values)));
        }
    }
}
