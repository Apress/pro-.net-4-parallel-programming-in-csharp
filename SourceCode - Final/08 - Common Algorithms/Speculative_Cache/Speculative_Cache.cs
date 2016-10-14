using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Speculative_Cache {

    class Speculative_Cache<TKey, TValue> {
        private ConcurrentDictionary<TKey, Lazy<TValue>> dictionary;
        private BlockingCollection<TKey> queue;
        private Func<TKey, TKey[]> speculatorFunction;
        private Func<TKey, TValue> factoryFunction;
        
        public Speculative_Cache(Func<TKey, TValue> factory, Func<TKey, TKey[]> speculator) {
            // set the speculator instance variable
            speculatorFunction = speculator;
            // initialize the dictionary
            dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
            // initialize the queue
            queue = new BlockingCollection<TKey>();

            // create the wrapper function
            factoryFunction = (key => {
                // call the factory function 
                TValue value = factory(key);
                // add the key to the speculative queue
                queue.Add(key);
                // return the results
                return value;
            });

            // start the task that will handle speculation
            Task.Factory.StartNew(() => {
                Parallel.ForEach(queue.GetConsumingEnumerable(),
                    new ParallelOptions { MaxDegreeOfParallelism = 2 },
                    key => {
                        // enumerate the keys to speculate 
                        foreach (TKey specKey in speculatorFunction(key)) {
                            TValue res = dictionary.GetOrAdd(
                                specKey, 
                                new Lazy<TValue>(() => factory(specKey))).Value;
                        }
                    });
            });
        }

        public TValue GetValue(TKey key) {
            return  dictionary.GetOrAdd(key,
                new Lazy<TValue>(() => factoryFunction(key))).Value;
        }
    }
}
