using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Listing_17_18 {

    public class StaticPartitioner<T> : Partitioner<T> {
        private T[] Data;

        public StaticPartitioner(T[] data) {
            Data = data;
        }

        public override bool SupportsDynamicPartitions {
            get {
                return false;
            }
        }

        public override IList<IEnumerator<T>> GetPartitions(int partitionCount) {
            // create the list to hold the enumerators
            IList<IEnumerator<T>> list = new List<IEnumerator<T>>();
            // determine how many items per enumerator
            int itemsPerEnum = Data.Length / partitionCount;
            // process each but the last partition
            for (int i = 0; i < partitionCount - 1; i++) {
                list.Add(CreateEnum(i * itemsPerEnum, (i + 1) * itemsPerEnum));
            }
            // handle the last, potentially irregularly sized, partition
            list.Add(CreateEnum((partitionCount - 1) * itemsPerEnum,
                Data.Length));
            // return the list as the result
            return list;
        }

        IEnumerator<T> CreateEnum(int startIndex, int endIndex) {
            int index = startIndex;
            while (index < endIndex) {
                yield return Data[index++];
            }
        }
    }

    class Listing_18 {
        static void Main(string[] args) {


            // create some source data
            int[] sourceData = new int[10];
            for (int i = 0; i < sourceData.Length; i++) {
                sourceData[i] = i;
            }

            // create the partitioner
            StaticPartitioner<int> partitioner = new StaticPartitioner<int>(sourceData);

            // define a query
            IEnumerable<double> results =
                partitioner.AsParallel()
                .Select(item => Math.Pow(item, 2));

            // enumerate the query results
            foreach (double d in results) {
                Console.WriteLine("Enumeration got result {0}", d);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();

        }
    }
}
