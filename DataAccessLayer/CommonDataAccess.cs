using DomainLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CommonDataAccess
    {
        public static bool SaveGame(Bord data , string type)
        {
            try
            {
         
                if (type == "S")
                {
               
 
                }
                else if (type == "U")
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
