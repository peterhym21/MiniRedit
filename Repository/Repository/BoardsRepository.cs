using MiniReditRepository.Entities;
using MiniReditRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MiniReditRepository.Repository
{
    public class BoardsRepository : IBoardsRepository
    {
        #region Feltes and ctor
        private string conString;
        private List<Boards> boards;
        private Boards board;

        public BoardsRepository(string conString)
        {
            this.conString = conString;
        }
        #endregion

        public async Task<List<Boards>> GetBoards()
        {
            boards = new List<Boards>();

            // SQL Conection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("ReadBoards", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                boards.Add(new Boards 
                { 
                    BoardId = (int)myReader["BoardId"], 
                    Title = (string)myReader["Title"], 
                    Date = (DateTime)myReader["Date"], 
                    Deletet = (bool)myReader["deletet"]
                });
            }

            con.Close();
            return boards;
        }


        public async Task<Boards> GetOneBoard(int BoardId)
        {
            board = new Boards();

            // SQL Conection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("ReadOneBoard", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BoardId", BoardId);

            con.Open();
            SqlDataReader myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                board.BoardId = (int)myReader["BoardId"];
                board.Title = (string)myReader["Title"];
                board.Date = (DateTime)myReader["Date"];
                board.Deletet = (bool)myReader["Deletet"];
            }

            con.Close();
            return board;
        }


        public async Task<int> CreateBoard(Boards boardsEnt)
        {
            boards = new List<Boards>();
            int boardId = 0;

            // SQL Conection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("CreateBoard", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewBoardTitle", boardsEnt.Title);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            boards = await GetBoards();

            foreach (Boards boardsl in boards)
            {
                if (boardsl.Title == boardsEnt.Title)
                {
                    boardId = boardsl.BoardId;
                }
            }
            return boardId;
        }


        public async Task<int> UpdateBoard(Boards boardsEnt)
        {            
            // SQL Conection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("UpdateCategorys", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NewBoardTitle", boardsEnt.Title);
            cmd.Parameters.AddWithValue("@BoardId", boardsEnt.BoardId);

            con.Open();
            int affectedRows = cmd.ExecuteNonQuery();
            con.Close();
            return affectedRows;
        }


        public async Task DeleteBoard(int BoardId)
        {
            // SQL Conection
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("DeleteBoard", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BoardId", BoardId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
