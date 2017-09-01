﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass1Sol1
{
    class Crozzle
    {
        private uint rows = 0;
        private uint columns = 0;
        private string[] data;
        private const string rowPattern = @"ROWS=\s*\d+";
        private const string columnPattern = @"COLUMNS=\s*\d+";
        private const string configurationFilePattern = @"^\s*CONFIGURATION_FILE="".+""\s*$";
        private string configurationFile;
        private string wordlistFilePattern = @"^\s*WORDLIST_FILE="".+""\s*$";
        private string wordlistFile;
        private char[,] crozzleMap;
        private string wordPattern = @"^\s*(ROW|COLUMN)=\d+,[a-zA-Z]+,\d+\s*";
        private bool isValid;

        public Crozzle(string fileName)
        {
            // Read the data from the txt file and load the crozzle
            try
            {
                data = System.IO.File.ReadAllLines(fileName);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to open file error: {0}", e.Message);
            }

            //Initialize variables
            configurationFile = null;
            crozzleMap = null;
            isValid = true;

            //Initialize form
            extractData();
            validateCrozzle(new Configuration(configurationFile));
            validateWordList(new Wordlist(wordlistFile));
            
            
        }


        private void validateWordList(Wordlist wordlist)
        {
            Console.WriteLine("Wordlist: {0}",wordlist.isValid);
        }

        private void validateCrozzle(Configuration configuration)
        {
            Console.WriteLine("Test this function");
        }

        // Comment
        public string populateForm()
        {
            //Display the table with the right no of rows and columns
            string html = @"<table style=""width:100%; border-color: black"" border=""1"">";
            for (int i = 0; i < rows; i++)
            {
                html += @"<tr>";
                for (int j = 0; j < columns; j++)
                {
                    html += @"<td>";
                    html += crozzleMap[i,j];
                    html += @"</td>";
                }
                html += @"</tr>";
            }
            return html;
        }

        private void extractData()
        {
            foreach (string l in data)
            {
                // Extract rows and columns
                if (Regex.IsMatch(l, rowPattern))
                {
                
                    //Extract the number from the string
                    rows = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                }
                if (Regex.IsMatch(l, columnPattern))
                {
                    //Extract the number from the string
                    columns = UInt32.Parse(Regex.Match(l, @"\d+").Value);
                }

                // Extract Configuration File
                if (Regex.IsMatch(l, configurationFilePattern))
                {
                    //extract the file name 
                    for(int i = l.IndexOf("=") + 2 
                        ; i < l.Length - 1; i++)
                    {
                        configurationFile += l[i];
                    }
                }

                // Extract Wordlist File 
                if (Regex.IsMatch(l, wordlistFilePattern))
                {
                    //extract the file name 
                    for (int i = l.IndexOf("=") + 2
                        ; i < l.Length - 1; i++)
                    {
                        wordlistFile += l[i];
                    }
                }

                // Extract Horizontal and vertical words
                if (Regex.IsMatch(l, wordPattern))
                {
                    createMap(Regex.Match(l, wordPattern).Value);
                }
            }
        }

        //Takes a string in the format (Orientation=number, WORD, Start)
        private void createMap (string data)
        {
            var splitData = data.Split(',');
            
            
            // Extract the orientation
            // 0 for rows and 1 for columns
            bool orientation = false;
            if (Regex.IsMatch(splitData[0], @".*ROW.*"))
                orientation = false;
            else
                orientation = true;
            string word = splitData[1];


            // Populate the crozzleMap array with the appropriate values
            if (crozzleMap == null)
                crozzleMap = new char[rows, columns];
            

            // populate the array depending upon the text
            if(orientation)
            {
                int column = Int32.Parse(Regex.Match(splitData[0], @"\d+").Value) - 1;
                int row = Int32.Parse(splitData[2]) - 1;
                for(int i = 0; i < word.Length; i++)
                {
                    try
                    {
                        crozzleMap[row + i, column] = word[i];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Log.Error("Crozzle", string.Format("Invalid Crozzle! check word: {0}", word));
                    }
                }
            }
            else
            {
                int row = Int32.Parse(Regex.Match(splitData[0], @"\d+").Value) - 1;
                int column = Int32.Parse(splitData[2]) - 1;
                for (int i = 0; i < word.Length; i++)
                {
                    try
                    {
                        crozzleMap[row, column + i] = word[i];
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Log.Error("Crozzle", string.Format("Invalid Crozzle! check word: {0}", word));
                    }
                }
            }
        }


    }
}
