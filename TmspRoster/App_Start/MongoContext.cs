using MongoDB.Driver;
using System;
using System.Configuration;

namespace TmspRoster.App_Start
{
    public class MongoContext
    {
        MongoClient _client;

        public MongoContext()        //constructor   
        {
            // Reading credentials from Web.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //CarDatabase  
            var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"]; //demouser  
            var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"]; //Pass@123  
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];  //27017  
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];  //localhost  

            // Creating credentials  
            var credential = MongoCredential.CreateCredential
                            (MongoDatabaseName,
                             MongoUsername,
                             MongoPassword);

            // Creating MongoClientSettings  
            var settings = new MongoClientSettings
            {
                Credential = credential,
                Server = new MongoServerAddress(MongoHost, Convert.ToInt32(MongoPort))
            };

            _client = new MongoClient(settings);
            var database = _client.GetDatabase("TmspDatabase");
        }
    }
}