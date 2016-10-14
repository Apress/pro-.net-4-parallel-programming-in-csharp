using System;
using System.Threading.Tasks;

namespace Decoupled_Console {
    class Using_Decoupled_Console {
      
        static void Main(string[] args) {
            // create a set of tasks that each writes messages
            for (int i = 0; i < 10; i++) {
                Task.Factory.StartNew(state => {
                    for (int j = 0; j < 10; j++) {
                        Decoupled_Console.WriteLine("Message from task {0}", Task.CurrentId);
                    }
                }, i);
            }

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}
