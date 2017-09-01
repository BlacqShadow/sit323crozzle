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

        // Regex Patterns
        private const string logFilePattern = @"\s*LOGFILE_NAME="".+""\s*";
        private const string minUniqueWordsPattern = @"MINIMUM_NUMBER_OF_UNIQUE_WORDS=\s*\d+";
        private const string maxUniqueWordsPattern = @"MAXIMUM_NUMBER_OF_UNIQUE_WORDS=\s*\d+";
        private const string invalidCrozzleScorePattern = @"\s*INVALID_CROZZLE_SCORE=""INVALID CROZZLE""\s*";
        private const string stylePattern = @"^STYLE=""<style>.*</style>""$";
        private const string upperCasePattern = @"^UPPERCASE=(true|false)$";
        private const string bgColorPattern = @"^BGCOLOUR_(NON_)?EMPTY_TD=#[0-9a-fA-F]{6,6}";
        private const string numberOfRowsColumnsPattern = @"M(IN|AX)IMUM_NUMBER_OF_(ROWS|COLUMNS)=\d+";
        private const string numberOfHorizontalVerticalPattern = @"M(IN|AX)IMUM_(HORIZONTAL|VERTICAL)_WORDS=\d+";
        private const string numberOfIntersectionsPattern = @"M(IN|AX)IMUM_INTERSECTIONS_IN_(HORIZONTAL|VERTICAL)_WORDS=\d+";
        private const string numberOfSameWordPattern = @"M(IN|AX)IMUM_NUMBER_OF_THE_SAME_WORD=\d+";
        private const string numberOfGroupsPattern = @"M(IN|AX)IMUM_NUMBER_OF_GROUPS=\d+";
        private const string pointsPerWordPattern = @"POINTS_PER_WORD=\d+";
        private const string pointsPerLetterPattern = @"^(INTERSECTING|NON_INTERSECTING)_POINTS_PER_LETTER=""([A-Z]=-?\d+,){25,25}([A-Z]=-?\d+)""$";






        // Verification markers
        public string style = null;
        public bool isValid;
        private bool containsInvalidCrozzleScore = false;
        private bool containsLogFile = false;
        private string invalidCrozzleScore = null;
        public string bgColorEmpty = null;
        public string bgColorNonEmpty = null;
        public bool isUpper;
        public uint MINIMUM_NUMBER_OF_UNIQUE_WORDS = 0;
        public uint MAXIMUM_NUMBER_OF_UNIQUE_WORDS = 0;
        public uint MINIMUM_NUMBER_OF_ROWS = 0;
        public uint MINIMUM_NUMBER_OF_COLUMNS = 0;
        public uint MAXIMUM_NUMBER_OF_ROWS = 0;
        public uint MAXIMUM_NUMBER_OF_COLUMNS = 0;
        public uint MINIMUM_HORIZONTAL_WORDS = 0;
        public uint MAXIMUM_HORIZONTAL_WORDS = 0;
        public uint MINIMUM_VERTICAL_WORDS = 0;
        public uint MAXIMUM_VERTICAL_WORDS = 0;
        public uint MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = 0;
        public uint MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = 0;
        public uint MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = 0;
        public uint MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = 0;
        public uint MINIMUM_NUMBER_OF_THE_SAME_WORD = 0;
        public uint MAXIMUM_NUMBER_OF_THE_SAME_WORD = 0;
        public uint MINIMUM_NUMBER_OF_GROUPS = 0;
        public uint MAXIMUM_NUMBER_OF_GROUPS = 0;
        public uint POINTS_PER_WORD = 0;
        public string INTERSECTING_POINTS_PER_LETTER = "";
        public string NON_INTERSECTING_POINTS_PER_LETTER = "";


        private void validateconfig()
        {
       
        }

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

            isValid = false;
            extractData();
        }

        
        #region extracting data from the config file
        private void extractData()
        {
            foreach (string l in data)
            {
                //SRS #2 Check if the logfile name exists
                if(Regex.IsMatch(l,logFilePattern))
                {
                    containsLogFile = true;
                }

                // Extract minimum and maximum unique words
                if(Regex.IsMatch(l,minUniqueWordsPattern))
                {
                    try
                    {

                        //Extract the number from the string
                        MINIMUM_NUMBER_OF_UNIQUE_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                }
                if (Regex.IsMatch(l, maxUniqueWordsPattern))
                {
                    try
                    {

                    //Extract the number from the string
                    MAXIMUM_NUMBER_OF_UNIQUE_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                }
                if (Regex.IsMatch(l, invalidCrozzleScorePattern))
                {
                    containsInvalidCrozzleScore = true;
                }

                //Is uppercase
                if(Regex.IsMatch(l,upperCasePattern))
                {
                    try
                    {

                        isUpper = bool.Parse(Regex.Match(l,@"true|false").Value);
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                   
                }

                //Extract HTML STYLE
                if(Regex.IsMatch(l,stylePattern))
                {
                    try
                    {

                        style = Regex.Match(l, @"<style>.*</style>").Value;
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                    
                }

                // BGCOLORS for empty and non-empty cells
                if(Regex.IsMatch(l, bgColorPattern))
                {
                    try
                    {

                            if (Regex.IsMatch(l, @"NON"))
                        {
                            bgColorNonEmpty = Regex.Match(l, @"#[0-9a-fA-F]{6,6}").Value;
                        }
                        else
                        {
                            bgColorEmpty = Regex.Match(l, @"#[0-9a-fA-F]{6,6}").Value;
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                    
                }
                
                // Invalid Crozzle score
                if(Regex.IsMatch(l, invalidCrozzleScorePattern))
                {
                    try
                    {

                       invalidCrozzleScore = Regex.Match(l,@""".*""").Value;
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                    
                }

                // MIN AND MAX ROWS + Columns
                if(Regex.IsMatch(l,numberOfRowsColumnsPattern))
                {

                    try
                    {
                    if (Regex.IsMatch(l,@"ROWS"))
                                        {
                                            if (Regex.IsMatch(l, @"MINIMUM"))
                                                MINIMUM_NUMBER_OF_ROWS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                                            else
                                                MAXIMUM_NUMBER_OF_ROWS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                                        }
                                        else
                                        {
                                            if (Regex.IsMatch(l, @"MINIMUM"))
                                                MINIMUM_NUMBER_OF_COLUMNS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                                            else
                                                MAXIMUM_NUMBER_OF_COLUMNS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                    
                }
                // MIN MAX HORIZONTAL VERTICAL WORDS
                if (Regex.IsMatch(l, numberOfHorizontalVerticalPattern))
                {
                    try
                    {

                        if (Regex.IsMatch(l, @".*HORIZONTAL.*"))
                    {
                        if (Regex.IsMatch(l, @".*MINIMUM.*"))
                            MINIMUM_HORIZONTAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                        else
                            MAXIMUM_HORIZONTAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    }
                    else
                    {
                        if (Regex.IsMatch(l, @".*MINIMUM.*"))
                            MINIMUM_VERTICAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                        else
                            MAXIMUM_VERTICAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    }
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                    
                }


                // MIN MAX INTERSECTIONS HORIZONTALLY AND VERTICALLY
                if (Regex.IsMatch(l, numberOfIntersectionsPattern))
                {
                    try
                    {

                        if (Regex.IsMatch(l, @".*HORIZONTAL.*"))
                    {
                        if (Regex.IsMatch(l, @".*MINIMUM.*"))
                            MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                        else
                            MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    }
                    else
                    {
                        if (Regex.IsMatch(l, @".*MINIMUM.*"))
                            MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                        else
                            MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    }
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                    
                }

                // MIN MAX SAME WORD 

                if (Regex.IsMatch(l, numberOfSameWordPattern))
                {
                    try
                    {
                    if (Regex.IsMatch(l, @"MINIMUM"))
                        MINIMUM_NUMBER_OF_THE_SAME_WORD = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    else
                        MAXIMUM_NUMBER_OF_THE_SAME_WORD = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                        
                    }
                    catch (Exception e)
                    {
                        Log.Error("Config", e.Message);
                    }
                    
                }


                // MIN MAX GROUPS
                if (Regex.IsMatch(l, numberOfGroupsPattern))
                {

                    if (Regex.IsMatch(l, @"MINIMUM"))
                        MINIMUM_NUMBER_OF_GROUPS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                    else
                        MAXIMUM_NUMBER_OF_GROUPS = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                }

                // POINTS PER WORD
                if (Regex.IsMatch(l, pointsPerWordPattern))
                {
                    POINTS_PER_WORD = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                }

                // POINTS PER INTERSECTING/NON LETTER
                if (Regex.IsMatch(l, pointsPerLetterPattern))
                {
                    if (Regex.IsMatch(l, @".*NON.*"))
                        NON_INTERSECTING_POINTS_PER_LETTER = Regex.Match(l, @"""([A-Z]=-?\d+,){25,25}([A-Z]=-?\d+)""").Value;
                    else
                        INTERSECTING_POINTS_PER_LETTER = Regex.Match(l, @"""([A-Z]=-?\d+,){25,25}([A-Z]=-?\d+)""").Value;
                }
                
            }
        }
        #endregion

    }
}
