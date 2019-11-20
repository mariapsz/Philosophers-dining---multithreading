using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philosophers_problem___multithreading
{
    class Seat
    {
        public Semaphore semaphore;
        public Seat(int numberOfSeats)
        {
            semaphore = new Semaphore(numberOfSeats - 1, numberOfSeats - 1);
        }
    }
}