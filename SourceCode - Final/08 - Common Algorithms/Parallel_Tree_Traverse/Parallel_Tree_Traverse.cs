using System;
using System.Threading.Tasks;

namespace Parallel_Tree_Traverse {

    public class Tree<T> {
        public Tree<T> LeftNode, RightNode;
        public T Data;
    }

    class TreeTraverser {
        public static void TraverseTree<T>(Tree<T> tree, Action<T> action) {
            if (tree != null) {
                // invoke the action for the data
                action.Invoke(tree.Data);
                // start tasks to process the left and right nodes if they exist
                if (tree.LeftNode != null && tree.RightNode != null) {
                    Task leftTask = Task.Factory.StartNew(
                        () => TraverseTree(tree.LeftNode, action));
                    Task rightTask = Task.Factory.StartNew(
                        () => TraverseTree(tree.RightNode, action));
                    // wait for the tasks to complete
                    Task.WaitAll(leftTask, rightTask);
                }
            }
        }
    }
}
