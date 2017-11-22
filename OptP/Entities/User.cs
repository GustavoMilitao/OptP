using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        [BsonId]
        public string _id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public User(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }
    }
}
