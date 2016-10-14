using System;
using System.Collections.Concurrent;

namespace Parallel_Cache {

    class Parallel_Cache<TKey,TValue> {
        private ConcurrentDictionary<TKey, Lazy<TValue>> dictionary;
        private Func<TKey, TValue> valueFactory;

        public Parallel_Cache(Func<TKey, TValue> factory) {
            // set the factory instance variable
            valueFactory = factory;
            // initialize the dictionary
            dictionary = new ConcurrentDictionary<TKey,Lazy<TValue>>();
        }

        public TValue GetValue(TKey key) {
            return dictionary.GetOrAdd(key, 
                new Lazy<TValue>(() => valueFactory(key))).Value;
        }
    }
}
