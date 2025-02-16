using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProcess
{
    public class MyProcess
    {
        public Process ProcessRef { get; set; }
        public string ProcessName { get; set; }
      

        public MyProcess(Process process)
        {
            ProcessRef = process;
            ProcessName = process.ProcessName;
           
        }

        public override string ToString()
        {
            return ProcessName;
        }
    }
}
