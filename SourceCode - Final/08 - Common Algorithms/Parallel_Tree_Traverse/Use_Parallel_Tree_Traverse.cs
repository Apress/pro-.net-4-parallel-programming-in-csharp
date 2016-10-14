using System;

namespace Parallel_Tree_Traverse {

    class Use_Parallel_Tree_Traverse {

        static void Main(string[] args) {

            // create and populate a simple tree
            Tree<int> tree = populateTree(new Tree<int>(), new Random());

            // traverse the tree, print out the even values
            TreeTraverser.TraverseTree(tree, item => {
                if (item % 2 == 0) {
                    Console.WriteLine("Item {0}", item);
                }
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }

        internal static Tree<int> populateTree(Tree<int> parentNode, 
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
