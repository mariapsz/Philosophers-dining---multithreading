using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philosophers_problem___multithreading
{
    class PhilosophersDining
    {
        private Thread[] philosophersThreads;
        private Philosopher[] philosophers;
        private readonly int philosophersCount;
        public PhilosophersDining(int philosophersCount)
        {
            this.philosophersCount = philosophersCount;
            philosophersThreads = new Thread[philosophersCount];
        }

        public void Dining()
        {
            StartDining();
            Thread.Sleep(1000);
            FinishDining();
            PhilosophersDiningReport.ShowReport(philosophers);
        }

        public void StartDining()
        {
            Console.WriteLine("Dining started!\n\n");
            Seat seat = new Seat(philosophersCount);
            Fork[] forks = GetForks(philosophersCount);
            philosophers = new Philosopher[philosophersCount];

            for (int i = 0; i < philosophersCount; i++)
            {
                Philosopher philosopher = new Philosopher(i, forks[i], forks[(i + 1) % philosophersCount], seat);
                philosophersThreads[i] = new Thread(philosopher.Start);
                philosophersThreads[i].Start();
                philosophers[i] = philosopher;
            }     
        }

        private Fork[] GetForks(int forksCount) {
            Fork[] forks = new Fork[forksCount];
            for (int i = 0; i < forksCount; i++)
            {
                forks[i] = new Fork(i);
            }
            return forks;
        }

        public void FinishDining()
        {
            foreach (Thread philosopherThread in philosophersThreads)
            {
                philosopherThread.Abort();
            }
        }
    }
}
