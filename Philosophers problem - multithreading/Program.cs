﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philosophers_problem___multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            PhilosophersDining philosophersDining = new PhilosophersDining(15);
            philosophersDining.Dining();

            Console.ReadKey();
        }
    }
}
