using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass1Sol1
{
    class Crozzle
    {
        private int rows = 0;
        private int columns = 0;
        private string[] data;
        const string rowPattern = @"ROWS=\s*\d+";
        const string columnPattern = @"COLUMNS=\s*\d+";


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


 

            
        }
        // Comment
        public string populateForm()
        {
            foreach (string l in data)
            {
                if (Regex.IsMatch(l, rowPattern))
                {
                    //Extract the number from the string
                    rows = Int32.Parse(Regex.Match(l, @"\d+").Value);
                }
                if (Regex.IsMatch(l, columnPattern))
                {
                    //Extract the number from the string
                    columns = Int32.Parse(Regex.Match(l, @"\d+").Value);
                }
            }


            //Display the table with the right no of rows and columns
            string html = @"<table style=""width:100%; border-color: black"" border=""1"">";
            for (int i = 0; i < rows; i++)
            {
                html += @"<tr>";
                for (int j = 0; j < columns; j++)
                {
                    html += @"<td>";
                    html += @"test";
                    html += @"</td>";
                }
                html += @"</tr>";
            }
            return html;
        }
    }
}
