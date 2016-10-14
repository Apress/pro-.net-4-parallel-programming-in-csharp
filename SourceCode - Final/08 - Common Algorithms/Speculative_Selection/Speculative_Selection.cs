using System;
using System.Threading;
using System.Threading.Tasks;

namespace Speculative_Selection {

    static class Speculative_Selection {

        public static void Compute<TInput, TOutput>(
            TInput value, 
            Action<long, TOutput> callback,
            params Func<TInput, TOutput>[] functions) {

            // define a counter to indicate the results produced
            int resultCounter = 0;

            // start a task to perform the parallel loop, otherwise
            // this method will block until a result has been found
            // and the functions running at that time have finished,
            // even if they are unsuccessful
            Task.Factory.StartNew(() => {
                // perform the parallel foreach
                Parallel.ForEach(functions,
                    (Func<TInput, TOutput> func,
                    ParallelLoopState loopState,
                    long iterationIndex) => {
                        // compute the result
                        TOutput localResult = func(value);
                        // increment the counter
                        if (Interlocked.Increment(ref resultCounter) == 1) {
                            // we are the first iteration to produce the result
                            // stop the loop
                            loopState.Stop();
                            // invoke the callback
                            callback(iterationIndex, localResult);
                        }
                    }
                );
            });
        }
    }
}
