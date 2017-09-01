using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass1Sol1
{
    class Configuration
    {

        private string[] data; 
        public Configuration(string configurationFile)
        {
            try
            {
                data = System.IO.File.ReadAllLines(configurationFile);
            }
            catch (Exception e)
            {
                Log.Error("Config File", string.Format("Unable to open file error: {0}", e.Message));
            }
        }
    }
}
