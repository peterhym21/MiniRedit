using MiniReditRepository.Entities;
using MiniReditRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Repository
{
    public class PostsRepository : IPostsRepository
    {
        #region Feltes and ctor
        private string conString;
        private List<Posts> posts;
        private Posts post;

        public PostsRepository(string conString)
        {
            this.conString = conString;
        }
        #endregion

        public async Task<List<Posts>> GetPosts()
        {
            posts = new List<Posts>();

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("ReadPosts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                posts.Add(new Posts
                {
                    PostId = (int)myReader["PostId"],
                    Title = (string)myReader["Title"],
                    Content = (string)myReader["Content"],
                    Date = (DateTime)myReader["Date"],
                    BoardId = (int)myReader["BoardId"],
                    UserId = (int)myReader["UserId"],
                    Deletet = (bool)myReader["Deletet"]
                });
            }

            con.Close();
            return posts;
        }


        public async Task<Posts> GetPostById(int postId)
        {
            post = new Posts();

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("ReadOnePost", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PostId", postId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                post.Title = (string)myReader["Title"];
                post.PostId = (int)myReader["PostId"];
                post.Content = (string)myReader["Content"];
                post.BoardId = (int)myReader["BoardId"];
                post.Date = (DateTime)myReader["Date"];
                post.UserId = (int)myReader["UserId"];
            }
            con.Close();
            return post;
        }


        public async Task<List<Posts>> GetPostsFromUser(int userId)
        {
            posts = new List<Posts>();

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("GetPostByuserId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                posts.Add(new Posts
                {
                    PostId = (int)myReader["PostId"],
                    Title = (string)myReader["Title"],
                    Content = (string)myReader["Content"],
                    Date = (DateTime)myReader["Date"],
                    BoardId = (int)myReader["BoardId"],
                    UserId = (int)myReader["UserId"],
                    Deletet = (bool)myReader["Deletet"]
                });
            }

            con.Close();
            return posts;
        }


        public async Task<List<Posts>> GetPostsByBoards(int boardId)
        {
            posts = new List<Posts>();

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("GetPostByBoard", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BoardId", boardId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                posts.Add(new Posts
                {
                    PostId = (int)myReader["PostId"],
                    Title = (string)myReader["Title"],
                    Content = (string)myReader["Content"],
                    Date = (DateTime)myReader["Date"],
                    BoardId = (int)myReader["BoardId"],
                    UserId = (int)myReader["UserId"],
                    Deletet = (bool)myReader["Deletet"]
                });
            }

            con.Close();
            return posts;
        }


        public async Task<List<Posts>> GetTopTen()
        {
            posts = new List<Posts>();

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("GetTopTenPosts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                posts.Add(new Posts
                {
                    PostId = (int)myReader["PostId"],
                    Title = (string)myReader["Title"],
                    Content = (string)myReader["Content"],
                    Date = (DateTime)myReader["Date"],
                    BoardId = (int)myReader["BoardId"],
                    UserId = (int)myReader["UserId"],
                    Deletet = (bool)myReader["Deletet"]
                });
            }

            con.Close();
            return posts;
        }


        public async Task<int> CreatePost(Posts postsEnt)
        {
            int postId = 0;

            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("CreatePost", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", postsEnt.Title);
            cmd.Parameters.AddWithValue("@Content", postsEnt.Content);
            cmd.Parameters.AddWithValue("@UserId", postsEnt.UserId);
            cmd.Parameters.AddWithValue("@BoardId", postsEnt.BoardId);

            SqlParameter param = new SqlParameter("@PostId", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            postId = Convert.ToInt32(cmd.Parameters["@PostId"].Value);
            return postId;
        }


        public async Task<int> UpdatePost(Posts postsEnt)
        {            
            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("UpdatePost", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Title", postsEnt.Title);
            cmd.Parameters.AddWithValue("@Content", postsEnt.Content);
            cmd.Parameters.AddWithValue("@PostId", postsEnt.PostId);
            cmd.Parameters.AddWithValue("@BoardId", postsEnt.BoardId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return postsEnt.PostId;
        }


        public async Task DeletePoste(int postId, int userid)
        {
            //SQL connection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("DeletePost", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@UserId", userid);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
