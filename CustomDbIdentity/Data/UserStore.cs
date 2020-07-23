using CustomDbIdentity.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml.Schema;

namespace CustomDbIdentity.Data
{
    public class UserStore : IUserStore<AppUser>, IUserPasswordStore<AppUser>
    {
        private string _connString = "server=127.0.0.1;port=3306;user=root;password=AtutAdmin1;database=identitytest";

        #region IUserStore

        public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using(var connection = new MySqlConnection(_connString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"insert into userss VALUES ('{user.Id}','{user.UserName}', '{user.Password}')", connection);
                cmd.ExecuteNonQuery();

                connection.Close();
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        
        {
            //throw new NotImplementedException();
        }

        public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            string answer;

            using (var connection = new MySqlConnection(_connString))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand($"select Id from userss where Id == {userId}", connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    answer = reader.ToString();
                }
                reader.Close();
                connection.Close();
            }

            return new AppUser { Id=0,UserName="Dan", Password="ddd"} ;
        }

        public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            //cancellationToken.ThrowIfCancellationRequested();
            //string answer;

            //using (var connection = new MySqlConnection(_connString))
            //{
            //    connection.Open();
            //    MySqlCommand cmd = new MySqlCommand($"select * from userss where userName  == {normalizedUserName}", connection);
            //    MySqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        answer = reader.ToString();
            //    }
            //    reader.Close();
            //    connection.Close();
            //}

            return new AppUser { Id = 0, UserName = "Dan", Password = "dddd" };
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

    

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

     

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);

        }

      

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        #endregion
    }
}
