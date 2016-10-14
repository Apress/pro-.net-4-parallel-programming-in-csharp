using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_04 {

    class Listing_04 {
        static CountdownEvent cdEvent;
        static SemaphoreSlim semA, semB;
        
        static void Main(string[] args) {
            // initialize the semaphores
            semA = new SemaphoreSlim(2);
            semB = new SemaphoreSlim(2);

            // define the number of tasks we will use
            int taskCount = 10;

            // initialize the barrier
            cdEvent = new CountdownEvent(taskCount);

            Task[] tasks = new Task[10];
            for (int i = 0; i < taskCount; i++) {
                tasks[i] = Task.Factory.StartNew((stateObject) => {
                    InitialMethod((int)stateObject);
                    Console.WriteLine("Task {0} completed", Task.CurrentId);
                }, i);
            }

            // wait for all of the tasks to have reached a terminal method
            cdEvent.Wait();

            // throw an exception to force the debugger to break
            throw new Exception();
        }

        static void InitialMethod(int argument) {
            if (argument % 2 == 0) {
                MethodA(argument);
            } else {
                MethodB(argument);
            }
        }

        static void MethodA(int argument) {
            if (argument < 5) {
                TerminalMethodA();
            } else {
                TerminalMethodB();
            }
        }

        static void MethodB(int argument) {
            if (argument < 5) {
                TerminalMethodA();
            } else {
                TerminalMethodB();
            }
        }

        static void TerminalMethodA() {
            // signal the countdown event
            cdEvent.Signal();
            Console.WriteLine("Method A decrement {0} {1}", cdEvent.CurrentCount, Task.CurrentId);
            // acquire the lock for this method
            semA.Wait();
            // perform some work
            for (int i = 0; i < 500000000; i++) {
                Math.Pow(i, 2);
            }
            // release the semaphore
            semA.Release();
        }

        static void TerminalMethodB() {
            // signal the countdown event
            cdEvent.Signal();
            Console.WriteLine("Method B decrement {0} {1}", cdEvent.CurrentCount, Task.CurrentId);
            // acquire the lock for this method
            semB.Wait();
            // perform some work
            for (int i = 0; i < 500000000; i++) {
                Math.Pow(i, 3);
            }
            // release the semaphore
           semB.Release();
        }
    }
}
