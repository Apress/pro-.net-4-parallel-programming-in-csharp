using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Tree_Search {

    public class Tree<T> {
        public Tree<T> LeftNode, RightNode;
        public T Data;
    }

    class TreeSearch {

        public static T SearchTree<T>(Tree<T> tree, Func<T, bool> searchFunction) {
            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            // search the tree
            TWrapper<T> result = performSearch(tree, searchFunction, tokenSource);
            return result == null ? default(T) : result.Value;
        }

        class TWrapper<T> {
            public T Value;
        }

        private static TWrapper<T> performSearch<T>(Tree<T> tree, 
            Func<T, bool> searchFunction, 
            CancellationTokenSource tokenSource) {

            // define the result
            TWrapper<T> result = null;
            // only proceed if we have something to search
            if (tree != null) {
                // apply the search function to the current tree
                if (searchFunction(tree.Data)) {
                    //cancel the token source
                    tokenSource.Cancel();
                    // set the result
                    result = new TWrapper<T>() { Value = tree.Data };
                } else {
                    // we have not found a result - continue the search
                    if (tree.LeftNode != null && tree.RightNode != null) {
                        // start the task for the left node
                        Task<TWrapper<T>> leftTask = Task<TWrapper<T>>.Factory.StartNew(
                            () => performSearch(tree.LeftNode, searchFunction, tokenSource), 
                            tokenSource.Token);
                        // start the task for the right node
                        Task<TWrapper<T>> rightTask = Task<TWrapper<T>>.Factory.StartNew(
                            () => performSearch(tree.RightNode, searchFunction, tokenSource), 
                            tokenSource.Token);

                        try {
                            // set the result based on the tasks
                            result = leftTask.Result != null ? 
                                leftTask.Result : rightTask.Result != null ?
                                rightTask.Result : null;
                        } catch (AggregateException) { }
                    }
                }
            }
            // return the result
            return result;
        }
    }
}
