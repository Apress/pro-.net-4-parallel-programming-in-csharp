using System;

namespace Pipeline {

    class Use_Pipleline {
        static void Main(string[] args) {

            // create a set of functions that we want to pipleline together
            Func<int, double> func1 = (input => Math.Pow(input, 2));
            Func<double, double> func2 = (input => input / 2);
            Func<double, bool> func3 = (input => input % 2 == 0 && input > 100);

            // define a callback
            Action<int, bool> callback = (input, output) => {
                if (output) {
                    Console.WriteLine("Found value {0} with result {1}", input, output);
                }
            };

            // create the pipeline
            Pipeline<int, bool> pipe = new Pipeline<int, double>(func1).AddFunction(func2).AddFunction(func3);
            // start the pipeline
            pipe.StartProcessing();

            // generate values and push them into the pipeline
            for (int i = 0; i < 1000; i++) {
                Console.WriteLine("Added value {0}", i);
                pipe.AddValue(i, callback);
            }

            // stop the pipeline
            pipe.StopProcessing();

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
