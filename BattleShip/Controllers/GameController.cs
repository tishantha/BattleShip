
using DomainLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BattleShip.Controllers
{
    public class GameController : ApiController
    {
        /// <summary>
        /// API InitializeInstance is used to initialize the game
        /// instance. This instance will be saved on db server and 
        /// should update the game status time to time with other API calls
        /// (Future developments)
        /// </summary>
        /// <returns></returns>
        public Bord InitializeInstance()
        {
            CommonServives serObj = new CommonServives();
            Bord b = serObj.InitializeBord(); 
            return b;
        }


        /// <summary>
        /// FireOnUsersShip API is used to send hits to target users ships
        /// In this api it will find a new location using a list of previously 
        /// used, Hits and Missed locations.
        /// Random number between 1 and #of available locations will be genarated and 
        /// selct that number as the new location
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public GridColumn FireOnUsersShip(Bord b)
        {
            GridColumn c = new GridColumn();
            CommonServives serObj = new CommonServives();
            c.Code = serObj.GetRandomColumn(b);
            return c;
        }
    }
}
