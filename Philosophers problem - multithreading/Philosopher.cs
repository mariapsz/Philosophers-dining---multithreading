using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philosophers_problem___multithreading
{
    class Philosopher
    {
        public int index;
        public Fork leftFork;
        public Fork rightFork;
        public Seat seat;    
        public bool wasEating = false;
        private Random actionTimeGenerator;

        public Philosopher(int index, Fork leftFork, Fork rightFork, Seat seat)
        {
            this.index = index;
            actionTimeGenerator = new Random();
            this.leftFork = leftFork;
            this.rightFork = rightFork;
            this.seat = seat;
        }

        public void Start()
        {
            Console.WriteLine("Philosopher " + index + "came\n");
            while (true)
            {
                this.Think();
                Console.WriteLine("philosopher " + index + " is waiting for seat");
                seat.semaphore.WaitOne();
                Console.WriteLine("philosopher " + index + " is waiting for left Fork");
                leftFork.semaphore.WaitOne();
                Console.WriteLine("philosopher " + index + " got left Fork");
                rightFork.semaphore.WaitOne();
                Console.WriteLine("philosopher " + index + " is eating");
                this.Eat();
                Console.WriteLine("philosopher " + index + " finished eating");
                leftFork.semaphore.Release();
                rightFork.semaphore.Release();
                seat.semaphore.Release();
            }
        }

        private void Think()
        {
            int sleepTime = this.actionTimeGenerator.Next(10, 50);
            Thread.Sleep(sleepTime);
        }


        private void Eat()
        {
            int sleepTime = this.actionTimeGenerator.Next(10, 50);
            Thread.Sleep(sleepTime);
            wasEating = true;
        }
    }
}
