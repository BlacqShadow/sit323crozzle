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
            string[] data = System.IO.File.ReadAllLines(fileName);

 

            
        }
        // Comment
        public string populateForm()
        {
            foreach (string l in data)
            {
                if (Regex.IsMatch(l, rowPattern))
                {
                    //int index = l.IndexOf('=');
                    rows = Convert.ToInt32(String.Format("{0}{1}", l[index + 1], l[index + 2]));
                }
                if (Regex.IsMatch(l, columnPattern))
                {
                    int index = l.IndexOf('=');
                    columns = Convert.ToInt32(String.Format("{0}{1}", l[index + 1], l[index + 2]));
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
