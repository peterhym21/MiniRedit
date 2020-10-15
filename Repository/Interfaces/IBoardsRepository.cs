using MiniReditRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditRepository.Interfaces
{
    public interface IBoardsRepository
    {
        Task<List<Boards>> GetBoards();
        Task<Boards> GetOneBoard(int BoardId);
        Task<int> CreateBoard(Boards boardsEnt);
        Task<int> UpdateBoard(Boards boardsEnt);
        Task DeleteBoard(int BoardId);
    }
}
