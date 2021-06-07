using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AntColonySystem.Models
{
    public class ProcessorModel
    {
        public int id { get; set; }
        public double speedRate { get; set; }
        public double procUtilization { get; set; }
        public int taskCount { get; set; }
    }
}
