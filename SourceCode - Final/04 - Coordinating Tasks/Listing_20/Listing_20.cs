using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_20 {

    class Listing_20 {

        static void Main(string[] args) {

            // create a pair of blocking collections
            // that will be used to pass strings
            BlockingCollection<string> bc1 = new BlockingCollection<string>();
            BlockingCollection<string> bc2 = new BlockingCollection<string>();

            // create another blocking collection 
            // that will be used to pass ints
            BlockingCollection<string> bc3 = new BlockingCollection<string>();

            // create two arrays of the blocking collections
            BlockingCollection<string>[] bc1and2 = { bc1, bc2 };
            BlockingCollection<string>[] bcAll = { bc1, bc2, bc3 };

            // create a cancellation token source 
            CancellationTokenSource tokenSource = new CancellationTokenSource();


            // create the first set of producers
            for (int i = 0; i < 5; i++) {
                Task.Factory.StartNew(() => {
                    while (!tokenSource.IsCancellationRequested) {
                        // compose the message
                        string message
                            = String.Format("Message from task {0}", Task.CurrentId);
                        // add the message to either collection
                        BlockingCollection<string>.AddToAny(bc1and2,
                            message, tokenSource.Token);
                        // put the task to sleep 
                        tokenSource.Token.WaitHandle.WaitOne(1000);
                    }
                }, tokenSource.Token);
            }

            // create the second set of producers
            for (int i = 0; i < 3; i++) {
                Task.Factory.StartNew(() => {
                    while (!tokenSource.IsCancellationRequested) {
                        // compose the message
                        string warning
                            = String.Format("Warning from task {0}", Task.CurrentId);
                        // add the message to either collection
                        bc3.Add(warning, tokenSource.Token);
                        // put the task to sleep for 500ms
                        tokenSource.Token.WaitHandle.WaitOne(500);
                    }
                }, tokenSource.Token);
            }

            // create the consumers
            for (int i = 0; i < 2; i++) {
                Task consumer = Task.Factory.StartNew(() => {
                    string item;
                    while (!tokenSource.IsCancellationRequested) {
                        // take an item from any collection
                        int bcid = BlockingCollection<string>.TakeFromAny(bcAll,
                            out item, tokenSource.Token);
                        // write out the item to the console
                        Console.WriteLine("From collection {0}: {1}", bcid, item);
                    }
                }, tokenSource.Token);
            }

            // prompt the user to press enter
            Console.WriteLine("Press enter to cancel tasks");
            Console.ReadLine();
            // cancel the token
            tokenSource.Cancel();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
