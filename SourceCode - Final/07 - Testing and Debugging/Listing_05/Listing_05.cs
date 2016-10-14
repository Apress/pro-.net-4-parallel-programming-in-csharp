using System;
using System.Threading.Tasks;

namespace Listing_05 {

    class Listing_05 {

        static void Main(string[] args) {

            Task[] tasks = new Task[2];
            for (int i = 0; i < tasks.Length; i++) {
                tasks[i] = Task.Factory.StartNew(() => {
                    for (int j = 0; j < 5000000; j++) {
                        if (j == 500) {
                            throw new Exception("Value is 500");
                        }
                        Math.Pow(j, 2);
                    }
                });
            }

            // wait for the tasks and catch any exceptions
            try {
                Task.WaitAll(tasks);
            } catch (AggregateException ex) {
                ex.Handle(innerEx => {
                    Console.WriteLine("Exception message is {0}", innerEx.Message);
                    return true;
                });
            }

            Console.ReadLine();
        }
    }
}
