using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Listing_01 {
    class Listing_01 {
        static void Main(string[] args) {

            Task[] tasks = new Task[2];

            tasks[0] = Task.Factory.StartNew(() => {
                tasks[1].Wait();
            });

            tasks[1] = Task.Factory.StartNew(() => {
                tasks[0].Wait();
            });

            Console.WriteLine("Waiting for tasks to complete.");
            Task.WaitAll(tasks);
        }
    }
}
