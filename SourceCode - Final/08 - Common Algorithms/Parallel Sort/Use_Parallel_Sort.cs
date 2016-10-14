using System;
using System.Collections.Generic;

namespace Parallel_Sort {
   
    class Use_Parallel_Sort {

        static void Main(string[] args) {
            // generate some random source data
            Random rnd = new Random();
            int[] sourceData = new int[5000000];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = rnd.Next(1, 100);
            }

            // perform the parallel sort
            Parallel_Sort<int>.ParallelQuickSort(sourceData, new IntComparer());
        }

        public class IntComparer : IComparer<int> {
            public int Compare(int first, int second) {
                return first.CompareTo(second);
            }
        }
    }
}
