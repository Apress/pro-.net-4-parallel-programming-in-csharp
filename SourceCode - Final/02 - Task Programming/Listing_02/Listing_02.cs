using System;
using System.Threading.Tasks;

namespace Listing_02 {

    class Listing_02 {

        static void Main(string[] args) {

            // use an Action delegate and a named method
            Task task1 = new Task(new Action(printMessage));

            // use a anonymous delegate
            Task task2 = new Task(delegate {
                printMessage();
            });

            // use a lambda expression and a named method
            Task task3 = new Task(() => printMessage());

            // use a lambda expression and an anonymous method
            Task task4 = new Task(() => {
                printMessage();
            });

            task1.Start();
            task2.Start();
            task3.Start();
            task4.Start();

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }

        static void printMessage() {
            Console.WriteLine("Hello World");
        }
    }
}
