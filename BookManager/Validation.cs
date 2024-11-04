using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookManager
{
    internal class Validation
    {
        public int lettersInNumbers( string userInput){
            string userCOnverted;
            try
            {
                return Convert.ToInt32(userInput);
            }
            catch (Exception ex)
            {
                return -1;
            }
        
        }      
    }
}
