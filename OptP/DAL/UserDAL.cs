using Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        public const string connectionString = @"mongodb://eckounltd:cefet123@custerpokemon-shard-00-00-zznsg.mongodb.net:27017,custerpokemon-shard-00-01-zznsg.mongodb.net:27017,custerpokemon-shard-00-02-zznsg.mongodb.net:27017/test?ssl=true&replicaSet=CusterPokemon-shard-0&authSource=admin";
        private static IMongoDatabase connection;
        public static IMongoDatabase Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = getNewConnection("OPTP");
                }
                return connection;
            }
        }
        static IMongoDatabase getNewConnection(string dataBase)
        {
            MongoClient client = new MongoClient(connectionString);
            return client.GetDatabase(dataBase);
        }

        public static List<User> Get()
        {
            var db = Connection;
            var users = db.GetCollection<User>("users");
            return users.Find(_ => true).ToList();
        }

        public static User Get(string userId)
        {
            var db = Connection;
            var users = db.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(userId));
            return users.Find(filter).FirstOrDefault();
        }

        public static User GetByUsuarioESenha(string usuario,string senha)
        {
            var db = Connection;
            var users = db.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq("Usuario", usuario);
            filter = filter & (Builders<User>.Filter.Eq("Senha", senha));
            return users.Find(filter).FirstOrDefault();
        }

        public static User GetByUsuario(string usuario)
        {
            var db = Connection;
            var users = db.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq("Usuario", usuario);
            return users.Find(filter).FirstOrDefault();
        }

        public static void Post(User user)
        {
            var db = Connection;
            var users = db.GetCollection<User>("users");
            users.InsertOne(user);
        }

        public static void Put(string userId, string usuario, string senha)
        {
            var db = Connection;
            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(userId));
            var users = db.GetCollection<User>("users");
            var update = Builders<User>.Update.Set("Usuario", usuario).Set("Senha", senha);
            users.UpdateOne(filter, update);
        }

        public static void Delete(string userId)
        {
            var db = Connection;
            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(userId));
            var users = db.GetCollection<User>("users");
            users.DeleteOne(filter);
        }
    }
}
