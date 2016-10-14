using System;
using System.Linq;

namespace Confusing_Ordering {
    class Confusing_Ordering {
        static void Main(string[] args) {

            string[] sourceData = new string[] {
                "an", "apple", "a", "day", "keeps",
                "the", "doctor", "away"};

            // create an AsOrdered() query 
            var result1 = sourceData.AsParallel()
                .AsOrdered()
                .Select(item => item);

            // enumerate the results
            foreach (var v in result1) {
                Console.WriteLine("AsOrdered() - {0}", v);                    
            }

            // create an OrderBy() query 
            var result2 = sourceData.AsParallel()
                .OrderBy(item => item)
                .Select(item => item);

            // enumerate the results
            foreach (var v in result2) {
                Console.WriteLine("OrderBy() - {0}", v);
            }
        }
    }
}
