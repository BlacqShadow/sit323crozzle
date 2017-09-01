using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass1Sol1
{
    class Wordlist
    {
        private string data;
        private string[] words;
        public bool isValid = true;
        public Wordlist(string filename)
        {
            try
            {
                data = System.IO.File.ReadAllText(filename);
                words = data.Split(',');
                checkvalidity();
            }
            catch (Exception e)
            {
                Log.Error("Word List", string.Format("Unable to open file error: {0}", e.Message));
            }
        }

        //Check to see if there are any duplicate words in the array 
        private void checkvalidity()
        {
            if (words.Length != words.Distinct().Count())
            {
                isValid = false;
                Log.Error("Wordlist", "Duplicates found!!, invalid file.");
            }
            else
                isValid = true;
        }
    }
}
