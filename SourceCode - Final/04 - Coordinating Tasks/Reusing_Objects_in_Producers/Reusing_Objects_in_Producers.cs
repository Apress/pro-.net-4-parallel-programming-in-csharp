using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Reusing_Objects_in_Producers {

    class DataItem {

        public int Counter {
            get;
            set;
        }
    }

    class Reusing_Objects_in_Producers {
        static void Main(string[] args) {

            // create a blocking collection
            BlockingCollection<DataItem> blockingCollection
                = new BlockingCollection<DataItem>();

            // create and start a consumer
            Task consumer = Task.Factory.StartNew(() => {
                // define a data item to use in the loop
                DataItem item;
                while (!blockingCollection.IsCompleted) {
                    if (blockingCollection.TryTake(out item)) {
                        Console.WriteLine("Item counter {0}", item.Counter);
                    }
                }
            });

            // create and start a producer
            Task.Factory.StartNew(() => {
                // create a data item to use in the loop
                DataItem item = new DataItem();
                for (int i = 0; i < 100; i++) {
                    // set the numeric value
                    item.Counter = i;
                    // add the item to the collection
                    blockingCollection.Add(item);
                }
                // mark the collection as finished
                blockingCollection.CompleteAdding();
            });

            // wait for the consumer to finish
            consumer.Wait();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
