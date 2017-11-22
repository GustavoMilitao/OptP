using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UserCore
    {
        public static List<User> Get()
        {
            return UserDAL.Get();
        }

        public static User Get(string userId)
        {
            return UserDAL.Get(userId);
        }

        public static User GetByUsuarioESenha(string usuario, string senha)
        {
            return UserDAL.GetByUsuarioESenha(usuario, senha);
        }

        public static User GetByUsuario(string usuario)
        {
            return UserDAL.GetByUsuario(usuario);
        }

        public static void Post(User user)
        {
            UserDAL.Post(user);
        }

        public static void Put(string userId, string usuario, string senha)
        {
            UserDAL.Put(userId, usuario, senha);
        }

        public static void Delete(string userId)
        {
            UserDAL.Delete(userId);
        }
    }
}
