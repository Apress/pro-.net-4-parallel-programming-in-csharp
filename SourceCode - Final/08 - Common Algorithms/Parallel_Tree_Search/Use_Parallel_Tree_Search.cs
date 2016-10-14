using System;

namespace Parallel_Tree_Search {
    class Use_TreeSearch {

        static void Main(string[] args) {

            // create and populate a simple tree
            Tree<int> tree = populateTree(new Tree<int>(), new Random(2));

            // traverse the tree, print out the even values
            int result = TreeSearch.SearchTree(tree, item => {
                if (item == 183) Console.WriteLine("Value : {0}", item);
                return item == 183;
            });

            Console.WriteLine("Search match ? {0}", result);

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }

        private static Tree<int> populateTree(Tree<int> parentNode, 
            Random rnd, int depth = 0) {

            parentNode.Data = rnd.Next(1, 1000);
            if (depth < 10) {
                parentNode.LeftNode = new Tree<int>();
                parentNode.RightNode = new Tree<int>();
                populateTree(parentNode.LeftNode, rnd, depth + 1);
                populateTree(parentNode.RightNode, rnd, depth + 1);
            }
            return parentNode;
        }
    }
}
