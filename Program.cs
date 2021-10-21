using AntColonySystem.Controller;
using AntColonySystem.Models;
using System;
using System.Collections.Generic;

namespace AntColonySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            int processorCount = 4; // number of processors in the MPSoC. 
            int taskSetSize = 5; // number of tasks per task set. 
            p.PrintMessage(); // Print the welcome message. 

            Console.WriteLine("MPSoC Processor count: {0}", processorCount);
            Console.WriteLine("Task Set size: {0}", taskSetSize);

            List<ProcessorModel> newMPSoC = new List<ProcessorModel>();

            double[] TaskSet = new double[] { 0.45, 0.4, 0.2, 0.2, 0.16 }; // Task set with utiliziation
            double[] SpeedRate = new double[] { 0.15, 0.4, 0.6, 0.8, 1.0 }; // processor speed rates available. 

            newMPSoC = TaskAllocator.TaskAllocate(TaskSet, SpeedRate, processorCount);


            // Get processor with single task. 
            List<int> coreWithOneTask = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                if (newMPSoC[i].taskCount == 1)
                {
                    coreWithOneTask.Add(i);
                }
            }

            List<myAnt> myAnts = new List<myAnt>();
            Random rnd = new Random();

            Console.WriteLine("----------------------");
            foreach (var item in coreWithOneTask)
            {
                double Ux = newMPSoC[item].procUtilization;
                int cnt = 0;

                for (double i = 0; i < Ux; i = i + 0.01)
                {
                    int randCore = rnd.Next(0, 3);
                    double sy = SpeedRate[rnd.Next(0, SpeedRate.Length)];

                    myAnts.Add(new myAnt()
                    {
                        Id = cnt,
                        Particle = i * 100,
                        tx = newMPSoC[item].procUtilization,
                        ty = newMPSoC[randCore].procUtilization,
                        Px = newMPSoC[item].id,
                        Py = randCore,
                        Sx = newMPSoC[item].speedRate,
                        Sy = sy
                    });
                    cnt++;
                }
            }

            Program m = new Program();

            //m.CalculatePower(myAnts);


            // List<Point> points = TspFileReader.ReadTspFile(@"TSP\kroA100.tsp");

            List<Point> points = m.CalculatePower(myAnts);


            Graph graph = new Graph(points, true);  // Create Graph
            GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(graph);
            double greedyShortestTourDistance = greedyAlgorithm.Run();

            Parameters parameters = new Parameters()
            {
                T0 = (1.0 / (graph.Dimensions * greedyShortestTourDistance))
            };
            parameters.Show();

            Solver solver = new Solver(parameters, graph);
            List<double> results = solver.RunACS(); // Run ACS

            Console.WriteLine("Time: " + solver.GetExecutionTime());
            Console.ReadLine();
        }


        private List<Point> CalculatePower(List<myAnt> myants)
        {
            double[] P = new double[] { 100, 200, 300, 400, 500 };
            double[] S = new double[] { 0.15, 0.4, 0.6, 0.8, 1.0 };
            List<Point> temp = new List<Point>();
            int power = 0;
            int cnt = 0;
            foreach (var ant in myants)
            {
                double x = ((ant.ty + ant.Particle) * P[Array.IndexOf(S, ant.Sy)]) / ant.Sy;
                double y = ((1 - (ant.ty + ant.Particle)) * 20) / ant.Sy;

                power = (int)(x + y);

                temp.Add(new Point(cnt, (float)ant.Particle, (float)power));
                cnt++;
            }

            return temp;
        }


        public void PrintMessage()
        {
            Console.WriteLine("     ---------------------------------------------------------------");
            Console.WriteLine("     ███╗   ███╗██████╗  █████╗  ██████╗ ██████╗      ██╗    ██████╗ ");
            Console.WriteLine("     ████╗ ████║██╔══██╗██╔══██╗██╔════╝██╔═══██╗    ███║   ██╔═████╗");
            Console.WriteLine("     ██╔████╔██║██████╔╝███████║██║     ██║   ██║    ╚██║   ██║██╔██║");
            Console.WriteLine("     ██║╚██╔╝██║██╔═══╝ ██╔══██║██║     ██║   ██║     ██║   ████╔╝██║");
            Console.WriteLine("     ██║ ╚═╝ ██║██║     ██║  ██║╚██████╗╚██████╔╝     ██║██╗╚██████╔╝");
            Console.WriteLine("     ╚═╝     ╚═╝╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═════╝      ╚═╝╚═╝ ╚═════╝");
            Console.WriteLine("     ---------------------------------------------------------------");
            Console.WriteLine("                    Ant Colony Based MPSoC Optimization             ");
            Console.WriteLine("              Dulana Rupanetti - University of St.Thomas, MN        ");
            Console.WriteLine("     ---------------------------------------------------------------\n");
        }

    }
}
