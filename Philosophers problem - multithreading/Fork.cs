using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philosophers_problem___multithreading
{
    class Fork
    {
        public Semaphore semaphore;
        public int index;
        public Fork(int index)
        {
            this.index = index;
            this.semaphore = new Semaphore(1, 1);
        }
    }
}
