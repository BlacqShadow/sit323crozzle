using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Ass1Sol1
{
    public static class Log
    {
        public static void Error(string file, string message)
        {
            Trace.WriteLine(string.Format("{0}: Error({1}) - {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),file,message));
        }
    }
}
