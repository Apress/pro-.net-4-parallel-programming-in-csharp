using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_13 {

    class Listing_13 {

        static void Main(string[] args) {

            // declare the name we will use for the mutex
            string mutexName = "myApressMutex";

            // declare the mutext
            Mutex namedMutext;

            try {
                // test to see if the named mutex already exists
                namedMutext = Mutex.OpenExisting(mutexName);
            } catch (WaitHandleCannotBeOpenedException) {
                // the mutext does not exist - we must create it
                namedMutext = new Mutex(false, mutexName);
            }

            // create the task
            Task task = new Task(() => {
                while (true) {
                    // acquire the mutex
                    Console.WriteLine("Waiting to acquire Mutex");
                    namedMutext.WaitOne();
                    Console.WriteLine("Acquired Mutex - press enter to release");
                    Console.ReadLine();
                    namedMutext.ReleaseMutex();
                    Console.WriteLine("Released Mutex");
                }
            });

            // start the task
            task.Start();

            // wait for the task to complete
            task.Wait();
        }
    }
}
