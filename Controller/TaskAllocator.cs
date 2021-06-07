using System;
using System.Collections.Generic;
using System.Text;
using AntColonySystem.Models;

namespace AntColonySystem.Controller
{
    public class TaskAllocator
    {
        public static List<ProcessorModel> TaskAllocate(double[] taskSet, double[] speedRates, int processorCnt)
        {
            // Initialize MPSoC and its processors. 
            List<ProcessorModel> newMPSoC = new List<ProcessorModel>();

            Array.Sort(speedRates); // arrange speed rates in ascending order. 

            Array.Sort(taskSet);
            Array.Reverse(taskSet); // arrange task set in descending order. 

            for (int i = 0; i < 4; i++)
            {
                newMPSoC.Add(new ProcessorModel() { id = i, procUtilization = 0.0, speedRate = 0.0, taskCount = 0 });
            }

            for (int j = 0; j < 5; j++)
            {
                int bnd = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (newMPSoC[i].procUtilization + taskSet[j] <= speedRates[bnd])
                    {
                        newMPSoC[i].procUtilization = newMPSoC[i].procUtilization + taskSet[j];
                        newMPSoC[i].speedRate = speedRates[bnd];
                        newMPSoC[i].taskCount = newMPSoC[i].taskCount + 1; 
                        break;
                    }

                    else if(i == 3)
                    {
                        bnd++;
                        i = -1;
                    }
                    
                }

            }

            Console.WriteLine("\n === First Fit Task Allocation === \n");

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Processor {0}: {1}\tTask count: {2}\tSpeed: {3}", newMPSoC[i].id  ,newMPSoC[i].procUtilization, newMPSoC[i].taskCount, newMPSoC[i].speedRate);
            }

            return newMPSoC;
        }

    }
}
