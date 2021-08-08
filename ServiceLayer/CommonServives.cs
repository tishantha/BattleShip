using DataAccessLayer;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class CommonServives
    {
        /// <summary>
        /// Initialize the Game
        /// Save game details (Futher Implementation)
        /// </summary>
        /// <returns></returns>
        public Bord InitializeBord()
        {
            Bord b = new Bord();
            b.SetDefault();
            bool status = CommonDataAccess.SaveGame(b,"S");
            return b;
        }


        /// <summary>
        /// Generate a random postion to attack
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string GetRandomColumn(Bord b)
        {
            string Code = "";
            List<string> sList = new List<string>();
            List<GridColumn> used = b.ComHit.Concat(b.ComMis).ToList();
            Bord b2 = new Bord();
            b2.SetDefault();
            foreach (BordColumns bc in b2.UserGrid)
            {
                sList.Add(bc.A.Code);
                sList.Add(bc.B.Code);
                sList.Add(bc.C.Code);
                sList.Add(bc.D.Code);
                sList.Add(bc.E.Code);
                sList.Add(bc.F.Code);
                sList.Add(bc.G.Code);
                sList.Add(bc.H.Code);
                sList.Add(bc.I.Code);
                sList.Add(bc.J.Code);
            }

            foreach (GridColumn us in used)
            {
                sList.Remove(us.Code);
            }
            
            Random rnd = new Random();
            int index = sList.Count - 1==0?0:rnd.Next(1, sList.Count-1);
            Code = sList[index];
            return Code;
        }
    }
}
