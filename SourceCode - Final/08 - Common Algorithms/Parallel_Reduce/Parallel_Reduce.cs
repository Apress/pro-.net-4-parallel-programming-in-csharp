using System;
using System.Linq;

namespace Parallel_Reduce {

    class Parallel_Reduce {

        public static TValue Reduce<TValue>(
            TValue[] sourceData,
            TValue seedValue,
            Func<TValue, TValue, TValue> reduceFunction) {

            // perform the reduction
            return sourceData
                .AsParallel()
                .Aggregate(
                    seedValue,
                    (localResult, value) => reduceFunction(localResult, value),
                    (overallResult, localResult) => reduceFunction(overallResult, localResult),
                    overallResult => overallResult);
        }
    }
}
