using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unexpected_Immutability {

    class MyReferenceData {
        public double PI = 3.14;
    }

    class MyImmutableType {
        public readonly MyReferenceData refData = new MyReferenceData();
        public readonly int circleSize = 1;
    }

    class UnexpectedImmutability {

        static void Main(string[] args) {

            // create a new instance of the immutable type
            MyImmutableType immutable = new MyImmutableType();

            // create a cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            // create a task that will calculate the circumference
            // of a 1 unit circle and check the result
            Task task1 = new Task(() => {
                while (true) {
                    // perform the calculation
                    double circ = 2 * immutable.refData.PI * immutable.circleSize;
                    Console.WriteLine("Circumference: {0}", circ);
                    // check for the mutation
                    if (circ == 4) {
                        // the mutation has occurred - break
                        // out of the loop
                        Console.WriteLine("Mutation detected");
                        break;
                    }
                    // sleep for a moment
                    tokenSource.Token.WaitHandle.WaitOne(250);
                }
            }, tokenSource.Token);

            // start the task
            task1.Start();

            // wait to let the task start work
            Thread.Sleep(1000);

            // perform the mutation
            immutable.refData.PI = 2;

            // join the task
            task1.Wait();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
