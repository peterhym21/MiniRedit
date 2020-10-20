using MiniReditServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditServices.Interfaces
{
    public interface IBoardsServices
    {
        Task<List<BoardsDTO>> GetBoards();
        Task<BoardsDTO> GetOneBoard(int BoardId);
        Task<int> CreateBoard(string title);
        Task<int> UpdateBoard(BoardsDTO boardsDTO);
        Task DeleteBoard(int BoardId);
    }
}
