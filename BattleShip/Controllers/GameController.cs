
using DomainLayer;
using Newtonsoft.Json;
using ServiceLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml;



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
        /// 
        //Install-Package Microsoft.AspNet.WebApi.Cors
        [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        //[HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public GridColumn FireOnUsersShip(Bord b)
        {
            GridColumn c = new GridColumn();
            CommonServives serObj = new CommonServives();
            c.Code = serObj.GetRandomColumn(b);
            return c;
        }
    }
}
