using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Words
{
    static class Log
    {
        public static void Write(string Message)
        {
            using (StreamWriter w = File.AppendText("logs.txt"))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("  :{0}", Message);
                w.WriteLine("-------------------------------");
            }
        }
        public static void Clear()
        {
            File.Delete("logs.txt");
        }
    }
}
