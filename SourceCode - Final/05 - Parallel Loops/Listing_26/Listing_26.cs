using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Listing_26 {

    class WorkItem {
        public int WorkDuration {
            get;
            set;
        }

        public void performWork() {
            // simulate work by sleeping
            Thread.Sleep(WorkDuration);
        }
    }

    class ContextPartitioner : OrderablePartitioner<WorkItem> {
        // the set of data items to partition
        protected WorkItem[] dataItems;
        // the target sum of values per chunk
        protected int targetSum;
        // the first unchunked item
        private long sharedStartIndex = 0;
        // lock object to avoid index data races
        private object lockObj = new object();
        // the object used to create enumerators
        private EnumerableSource enumSource;

        public ContextPartitioner(WorkItem[] data, int target) : base(true, false, true) {
            // set the instance variables from the parameters
            dataItems = data;
            targetSum = target;
            // create the enumerable source
            enumSource = new EnumerableSource(this);
        }

        public override bool SupportsDynamicPartitions {
            get {
                // dynamic partitions are required for
                // parallel foreach loops
                return true;
            }
        }

        public override IList<IEnumerator<KeyValuePair<long, WorkItem>>> 
            GetOrderablePartitions(int partitionCount) {

            // create the list which will be the result
            IList<IEnumerator<KeyValuePair<long, WorkItem>>> partitionsList 
                = new List<IEnumerator<KeyValuePair<long, WorkItem>>>();
            // get the IEnumerable that will generate dynamic partitions
            IEnumerable<KeyValuePair<long, WorkItem>> enumObj = GetOrderableDynamicPartitions();
            // create the required number of partitions
            for (int i = 0; i < partitionCount; i++) {
                partitionsList.Add(enumObj.GetEnumerator());
            }
            // return the result
            return partitionsList;
        }

        public override IEnumerable<KeyValuePair<long, WorkItem>> GetOrderableDynamicPartitions() {
            return enumSource;
        }

        private Tuple<long, long> getNextChunk() {
            // create the result tuple
            Tuple<long, long> result;
            // get an exclusive lock as we perform this operation
            lock (lockObj) {
                // check that there is still data available
                if (sharedStartIndex < dataItems.Length) {
                    int sum = 0;
                    long endIndex = sharedStartIndex;
                    while (endIndex < dataItems.Length && sum < targetSum) {
                        sum += dataItems[endIndex].WorkDuration;
                        endIndex++;
                    }
                    result = new Tuple<long, long>(sharedStartIndex, endIndex);
                    sharedStartIndex = endIndex;
                } else {
                    // there is no data available
                    result = new Tuple<long, long>(-1, -1);
                }
            }
            // end of locked region
            // return the result
            return result;
        }

        class EnumerableSource : IEnumerable<KeyValuePair<long, WorkItem>> {
            ContextPartitioner parentPartitioner;

            public EnumerableSource(ContextPartitioner parent) {
                parentPartitioner = parent;
            }

            IEnumerator IEnumerable.GetEnumerator() {
                return ((IEnumerable<WorkItem>)this).GetEnumerator();
            }

            IEnumerator<KeyValuePair<long, WorkItem>> IEnumerable<KeyValuePair<long, WorkItem>>.GetEnumerator() {
                return new ChunkEnumerator(parentPartitioner).GetEnumerator();
            }
        }

        class ChunkEnumerator {
            private ContextPartitioner parentPartitioner;

            public ChunkEnumerator(ContextPartitioner parent) {
                parentPartitioner = parent;
            }

            public IEnumerator<KeyValuePair<long, WorkItem>> GetEnumerator() {
                while (true) {
                    // get the indices of the next chunk
                    Tuple<long, long> chunkIndices = parentPartitioner.getNextChunk();
                    // check that we have data to deliver
                    if (chunkIndices.Item1 == -1 && chunkIndices.Item2 == -1) {
                        // there is no more data
                        break;
                    } else {
                        // enter a loop to yield the data items
                        for (long i = chunkIndices.Item1; i < chunkIndices.Item2; i++) {
                            yield return new KeyValuePair<long, WorkItem>(i, parentPartitioner.dataItems[i]);
                        }
                    }
                }
            }
        }
    }

    class ContextTest {
        static void Main(string[] args) {

            // create a random number source
            Random rnd = new Random();

            // create the source data
            WorkItem[] sourceData = new WorkItem[10000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = new WorkItem() { WorkDuration = rnd.Next(1, 11) };
            }

            // create the result data array
            WorkItem[] resultData = new WorkItem[sourceData.Length];

            // created the contentual partitioner
            OrderablePartitioner<WorkItem> cPartitioner = new ContextPartitioner(sourceData, 100);

            // create the parallel 
            Parallel.ForEach(cPartitioner, (WorkItem item, ParallelLoopState loopState, long index) => {
                // perform the work item
                item.performWork();
                // place the work item in the result array
                resultData[index] = item;
            });

            // compare the source items to the result items
            for (int i = 0; i < sourceData.Length; i++) {
                if (sourceData[i].WorkDuration != resultData[i].WorkDuration) {
                    Console.WriteLine("Discrepancy at index {0}", i);
                    break;
                }
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
