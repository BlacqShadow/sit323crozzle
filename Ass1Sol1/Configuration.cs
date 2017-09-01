using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ass1Sol1
{
    class Configuration
    {

        private string[] data;
        public bool isValid;
        private bool invalidCrozzleScoreExists = false;
        private const string logFilePattern = @"^\s*LOGFILE_NAME="".+""\s*$";
        private const string minUniqueWordsPattern = @"MINIMUM_NUMBER_OF_UNIQUE_WORDS=\s*\d+";
        private const string maxUniqueWordsPattern = @"MAXIMUM_NUMBER_OF_UNIQUE_WORDS=\s*\d+";
        private const string invalidCrozzleScorePattern = @"\s*INVALID_CROZZLE_SCORE=""INVALID CROZZLE""\s*";
        public uint MINIMUM_NUMBER_OF_UNIQUE_WORDS = 0;
        public uint MAXIMUM_NUMBER_OF_UNIQUE_WORDS = 0;

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

            isValid = true;
            extractData();
        }

        private void extractData()
        {
            foreach (string l in data)
            {
                //SRS #2 Check if the logfile name exists
                if(Regex.IsMatch(l,logFilePattern))
                {
                    isValid = true;
                }

                if(Regex.IsMatch(l,minUniqueWordsPattern))
                {
                    isValid = true;
                    //Extract the number from the string
                    MINIMUM_NUMBER_OF_UNIQUE_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                }
                if (Regex.IsMatch(l, maxUniqueWordsPattern))
                {
                    isValid = true;
                    //Extract the number from the string
                    MAXIMUM_NUMBER_OF_UNIQUE_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                }
                if (Regex.IsMatch(l, invalidCrozzleScorePattern))
                {
                    invalidCrozzleScoreExists = true;
                }
            }
        }

    }
}
