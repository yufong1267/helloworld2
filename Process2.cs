using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B10517025
{
    class Process2 : Process
    {
        public String process_name = "";
        public Process2(String reg_Process_name)
        {
            this.process_name = reg_Process_name;
            this.StartInfo.FileName = "cmd.exe";
            this.StartInfo.UseShellExecute = false;
      /*      this.StartInfo.RedirectStandardInput = true;
            this.StartInfo.RedirectStandardOutput = true;
            this.StartInfo.RedirectStandardError = true;*/
            this.StartInfo.CreateNoWindow = true;
            
        }

        
    }
}
