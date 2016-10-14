using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Parallel_Sort {

    class Use_Parallel_Sort {

        static void Main(string[] args) {

            // create the stopwatch
            Stopwatch watch = new Stopwatch();

            // define the iteration count
            int iterations = 10;

            // define the buffer size
            int buffer = 64 * 4;

            long elaspedTime = 0;





            // perform the work
            for (int iter = 0; iter < iterations; iter++) {

                // generate some ramdom source data
                Random rnd = new Random(2);
                double[] sourceData = new double[5000000];
                for (int i = 0; i < sourceData.Length; i++) {
                    sourceData[i] = rnd.Next(1, 100);
                }

                // start the timer
                watch.Reset();
                watch.Start();

                //var part = Partitioner.Create(0, sourceData.Length, 64);

                //Parallel.ForEach(part, tuple => {
                //    for (int i = tuple.Item1; i < tuple.Item2; i++) {
                //        sourceData[i] = Math.Pow(sourceData[i], 2);
                //    }
                //});

                Parallel.For(0, sourceData.Length, index => {
                    sourceData[index] = Math.Pow(sourceData[index], 2);
                });

                watch.Stop();
                elaspedTime += watch.ElapsedMilliseconds;
            }

            // print out the per-iteration time
            Console.WriteLine("Per iteration time: {0}", elaspedTime/iterations);
        }


    }

 
    
}
