using System;
using System.Collections.Generic;
using System.Linq;

namespace Parallel_MapReduce {

    class Use_Parallel_MapReduce {

        static void Main(string[] args) {

            // create a function that lists the factors
            Func<int, IEnumerable<int>> map = value => {
                IList<int> factors = new List<int>();
                for (int i = 1; i < value; i++) {
                    if (value % i == 0) {
                        factors.Add(i);
                    }
                }
                return factors;
            };

            // create the group function - in this example
            // we want to group the same results together, 
            // so we select the value itself
            Func<int, int> group = value => value;

            // create the reduce function - this simply
            // counts the number of elements in the grouping
            // and returns a Key/Value pair with the result as the
            // key and the count as the value
            Func<IGrouping<int, int>, 
                KeyValuePair<int, int>> reduce = 
                grouping => {
                    return new KeyValuePair<int, int>(
                        grouping.Key, grouping.Count());
            };

            // create some source data
            IEnumerable<int> sourceData = Enumerable.Range(1, 50);

            // use parallel map reduce with the source data
            // and the map, group and reduce functions
            IEnumerable<KeyValuePair<int, int>> result = 
                Parallel_MapReduce.MapReduce(
                    sourceData, 
                    map, 
                    group, 
                    reduce);

            // process the results
            foreach (KeyValuePair<int, int> kvp in result) {
                Console.WriteLine("{0} is a factor {1} times", 
                    kvp.Key, 
                    kvp.Value);
            }
        }
    }
}
