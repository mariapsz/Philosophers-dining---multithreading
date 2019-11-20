using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Philosophers_problem___multithreading
{
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
}
