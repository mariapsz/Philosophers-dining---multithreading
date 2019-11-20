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
        private Random actionTimeGenerator;
        public bool wasEating = false;
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
            int sleepTime = this.actionTimeGenerator.Next(100, 500);
            Thread.Sleep(sleepTime);
        }


        private void Eat()
        {
            int sleepTime = this.actionTimeGenerator.Next(100, 500);
            Thread.Sleep(sleepTime);
            wasEating = true;
        }
    }

    class PhilosophersDining
    {
        private Thread[] philosophersThreads;

        public PhilosophersDining(int numberOfPhilosphers)
        {
            philosophersThreads = new Thread[numberOfPhilosphers];
        }

        public void Start()
        {
            Console.WriteLine("Dining started!\n\n");
            int numberOfPhilosphers = philosophersThreads.Length;
            Seat seat = new Seat(numberOfPhilosphers);
            Fork[] forks = new Fork[numberOfPhilosphers];
            Philosopher[] philosophers = new Philosopher[numberOfPhilosphers];

            for (int i = 0; i < numberOfPhilosphers; i++)
            {
                forks[i] = new Fork(i);
            }

            for (int i = 0; i < numberOfPhilosphers; i++)
            {
                Philosopher philosopher = new Philosopher(i, forks[i], forks[(i + 1) % numberOfPhilosphers], seat);
                philosophersThreads[i] = new Thread(philosopher.Start);
                philosophersThreads[i].Start();
                philosophers[i] = philosopher;
            }

            Thread.Sleep(5000);
            foreach (Thread philosopherThread in philosophersThreads) {
                philosopherThread.Abort();
            }
            PhilosophersDiningReport.ShowReport(philosophers);
        }
    }

    class PhilosophersDiningReport
    {
        public static void ShowReport(Philosopher[] philosophers)
        {
            Console.WriteLine("\n\n\nPHILOSOPHERS DINING REPORT\n");
            int philosophersCount = philosophers.Length;
            int numberOfStuffed = 0;
            for (int i = 0; i < philosophersCount; i++)
            {
                if (philosophers[i].wasEating)
                {
                    Console.WriteLine("Philosopher " + philosophers[i].index + " was eating");
                    numberOfStuffed++;
                }
                else
                    Console.WriteLine("Philosopher " + philosophers[i].index + " wasn't eating");
            }
            Console.WriteLine("\n\nSUMMARY: " + numberOfStuffed + "/" + philosophersCount + " philosophers were eating");
        }
    }

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

    class Seat
    {
        public Semaphore semaphore;
        public Seat(int numberOfSeats)
        {
            semaphore = new Semaphore(numberOfSeats - 1, numberOfSeats - 1);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PhilosophersDining philosophersDining = new PhilosophersDining(15);
            philosophersDining.Start();

            Console.ReadKey();
        }
    }
}
