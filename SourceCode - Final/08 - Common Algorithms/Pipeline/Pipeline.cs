using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Pipeline {

    class Pipeline <TInput, TOutput> {
        // queue based blocking collection
        private BlockingCollection<ValueCallBackWrapper> valueQueue;
        // the function to use
        Func<TInput, TOutput> pipelineFunction;


        public Pipeline(Func<TInput, TOutput> function) {
            // assign the function
            pipelineFunction = function;
        }

        public Pipeline<TInput, TNewOutput> 
            AddFunction<TNewOutput>(Func<TOutput, TNewOutput> newfunction) {

            // create a composite function
            Func<TInput, TNewOutput> compositeFunction = (inputValue => {
                return newfunction(pipelineFunction(inputValue));
            });
            // return a new pipeline around the composite function
            return new Pipeline<TInput, TNewOutput>(compositeFunction);
        }

        public void AddValue(TInput value, Action<TInput, TOutput> callback) {
            // add the value to the queue for processing
            valueQueue.Add(new ValueCallBackWrapper {
                Value = value, Callback = callback
            });
        }

        public void StartProcessing() {
            // initialize the collection
            valueQueue = new BlockingCollection<ValueCallBackWrapper>();
            // create a parallel loop to consume
            // items from the collection
            Task.Factory.StartNew(() => {
                Parallel.ForEach(
                    valueQueue.GetConsumingEnumerable(),
                    wrapper => {
                        wrapper.Callback(wrapper.Value, pipelineFunction(wrapper.Value));
                    });
            });
        }

        public void StopProcessing() {
            // signal to the collection that no
            // further values will be added
            valueQueue.CompleteAdding();
        }

        private class ValueCallBackWrapper {
            public TInput Value;
            public Action<TInput, TOutput> Callback;
        }

    }
}
