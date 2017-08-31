using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ass1Sol1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //string html = @"<table style=""width:100%; border-color: black"" border=""1"">";
            //int row =  Convert.ToInt32(System.Console.ReadLine());
            //int column = Convert.ToInt32(System.Console.ReadLine());

            //for (int i = 0; i < row; i++)
            //{
            //    html += @"<tr>";
            //    for (int j = 0; j < column; j++)
            //    {
            //        html += @"<td>";
            //        html += @"test";
            //        html += @"</td>";
            //    }
            //    html += @"</tr>";
            //}
//  + @" <tr>
//    <th>Firstname</th>
//    <th>Lastname</th>
//    <th>Age</th>
//  </tr>
//  <tr>
//    <td>Jill</td>
//    <td>Smith</td>
//    <td>50</td>
//  </tr>
//  <tr>
//    <td>Eve</td>
//    <td>Jackson</td>
//    <td>94</td>
//  </tr>
//</table> ";

            //webBrowser1.DocumentText = html;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                Crozzle newCrozzle = new Crozzle(openFileDialog1.FileName);
                webBrowser1.DocumentText = newCrozzle.populateForm();
            }
        }
    }
}
