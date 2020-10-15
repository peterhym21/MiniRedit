using MiniReditRepository.Entities;
using MiniReditRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Repository
{
    public class UsersRepository : IUsersRepository
    {
        #region Feltes and ctor
        private string conString;
        private List<Users> users;
        private Users user;

        public UsersRepository(string conString)
        {
            this.conString = conString;
        }
        #endregion

        public async Task<List<Users>> GetUsers()
        {
            users = new List<Users>();

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("ReadUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                users.Add(new Users { UserId = (int)myReader["UseriId"], 
                    Name = (string)myReader["Name"], 
                    UserName = (string)myReader["UserName"], 
                    Password = (string)myReader["Password"],
                    Date = (DateTime)myReader["Date"],
                    Deletet = (bool)myReader["Deletet"]
                });
            }

            con.Close();
            return users;
        }


        public async Task<Users> GetUserById(int userId)
        {
            user = new Users();
            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("ReadOneUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                user.UserId = (int)myReader["UserId"];
                user.Name = (string)myReader["Name"];
                user.UserName = (string)myReader["UserName"];
                user.Password = (string)myReader["Password"];
                user.Date = (DateTime)myReader["Date"];
                user.Deletet = (bool)myReader["Deletet"];
            }

            con.Close();
            return user;
        }


        public async Task<Users> GetUserlogin(string username, string password)
        {
            user = new Users();
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("ReadOneUserForLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                user.UserId = (int)myReader["UserId"];
                user.Name = (string)myReader["Name"];
                user.UserName = (string)myReader["UserName"];
                user.Password = (string)myReader["Password"];
                user.Date = (DateTime)myReader["Date"];
                user.Deletet = (bool)myReader["Deletet"];
            }

            con.Close();
            return user;
        }


        public async Task<int> CreateUseres(Users userEn)
        {
            int userid = 0;

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("CreateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", userEn.Name);
            cmd.Parameters.AddWithValue("@Username", userEn.UserName);
            cmd.Parameters.AddWithValue("@Password", userEn.Password);

            SqlParameter param = new SqlParameter("@UserId", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            userid = Convert.ToInt32(cmd.Parameters["@UserId"].Value);
            return userid;
        }


        public async Task<int> UpdateUser(Users userEn)
        {
            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("UpdateUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", userEn.Name);
            cmd.Parameters.AddWithValue("@updateUsername", userEn.UserName);
            cmd.Parameters.AddWithValue("@UpdatePassword", userEn.Password);
            cmd.Parameters.AddWithValue("@UserId", userEn.UserId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return userEn.UserId;
        }


        public async Task DeleteUser(int Userid)
        {
            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("DeleteUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", Userid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
