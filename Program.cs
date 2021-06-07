using AntColonySystem.Models;
using AntColonySystem.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
            double[] SpeedRate = new double[] { 0.15, 0.4, 0.6, 0.8, 1.0}; // processor speed rates available. 

            newMPSoC = TaskAllocator.TaskAllocate(TaskSet, SpeedRate, processorCount);


            // Get processor with single task. 
            int antCount = 0;

            for(int i = 0; i < 4; i++)
            {
                if(newMPSoC[i].taskCount == 1)
                {
                    double Ui = newMPSoC[i].procUtilization;
                    antCount = Convert.ToInt32(Ui / 0.01);
                    Console.WriteLine(i);
                }
            }
            double[] Ant = new double[antCount];




            //List<Point> points = TspFileReader.ReadTspFile(@"TSP\kroA100.tsp");    // Parse TSPlib file and load as List<Point>
            //Graph graph = new Graph(points, true);  // Create Graph
            //GreedyAlgorithm greedyAlgorithm = new GreedyAlgorithm(graph);
            //double greedyShortestTourDistance = greedyAlgorithm.Run();  // get shortest tour using greedy algorithm

            //Parameters parameters = new Parameters()  // Most parameters will be default. We only have to set T0 (initial pheromone level)
            //{
            //    T0 = (1.0 / (graph.Dimensions * greedyShortestTourDistance))
            //};
            //parameters.Show();

            //Solver solver = new Solver(parameters, graph);
            //List<double> results = solver.RunACS(); // Run ACS

            //Console.WriteLine("Time: " + solver.GetExecutionTime());
            //Console.ReadLine(); 
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
