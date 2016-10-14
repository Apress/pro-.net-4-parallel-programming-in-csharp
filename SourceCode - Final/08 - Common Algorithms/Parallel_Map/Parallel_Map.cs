using System;
using System.Linq;

namespace Parallel_Map {

    class Parallel_Map {
                
        public static TOutput[] ParallelMap<TInput, TOutput>(
            Func<TInput, TOutput> mapFunction, 
            TInput[] input) {

            return input
                .AsParallel()
                .AsOrdered()
                .Select(value => mapFunction(value))
                .ToArray();
        }
    }
}
