using MiniReditRepository.Entities;
using MiniReditRepository.Interfaces;
using MiniReditServices.DTO;
using MiniReditServices.Interfaces;
using Service.Services;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniReditServices.Services
{
    public class BoardsServices : BaseService, IBoardsServices
    {
        private readonly MappingService _mappingService;
        private readonly IBoardsRepository _boardsRepository;

        public BoardsServices(MappingService mappingService, IBoardsRepository boardsRepository)
        {
            _mappingService = mappingService;
            _boardsRepository = boardsRepository;
        }

        public async Task<int> CreateBoard(BoardsDTO boardsDTO)
        {
            try
            {
                int resultId = await _boardsRepository.CreateBoard(_mappingService._mapper.Map<Boards>(boardsDTO));
                LogInformation($"Successfully created a new board : BoardId {boardsDTO.BoardId}, Title {boardsDTO.Title} :");
                return resultId;
            }
            catch (Exception ex)
            {
                LogError($"Failed to create a new board : boardId {boardsDTO.BoardId}, Title {boardsDTO.Title} :", ex);
                return 1;

            }
        }

        public async Task DeleteBoard(int BoardId)
        {
            try
            {
                await _boardsRepository.DeleteBoard(BoardId);
                LogInformation($"Successfully removed a board : BoardId {BoardId} :");
            }
            catch (Exception ex)
            {
                LogError($"Failed to remove a board : BoardId {BoardId}", ex);

            }
        }

        public async Task<List<BoardsDTO>> GetBoards()
        {
            try
            {
                List<BoardsDTO> boards = _mappingService._mapper.Map<List<BoardsDTO>>(await _boardsRepository.GetBoards());
                LogInformation($"Successfully fetched a list of boards");
                return boards;
            }
            catch (Exception ex)
            {
                LogError("Failed to fetch a list of boards", ex);
                return null;

            }
        }

        public async Task<BoardsDTO> GetOneBoard(int BoardId)
        {
            if (BoardId == 0)
            {
                return null;
            }
            try
            {
                BoardsDTO board = _mappingService._mapper.Map<BoardsDTO>(await _boardsRepository.GetOneBoard(BoardId));
                LogInformation($"Successfully fetched a Board : boardId {board.BoardId}, Title {board.Title} :");
                return board;
            }
            catch (Exception ex)
            {
                LogError($"Failed to fetch a Board : boardId {BoardId}", ex);
                return null;

            }
        }

        public async Task<int> UpdateBoard(BoardsDTO boardsDTO)
        {
            try
            {
                int result = await _boardsRepository.UpdateBoard(_mappingService._mapper.Map<Boards>(boardsDTO));
                LogInformation($"Successfully updated a board : boardId { boardsDTO.BoardId} :");
                return result;
            }
            catch (Exception ex)
            {
                LogError($"Failed to update a board : boardId { boardsDTO.BoardId} :", ex);
                return 1;
            }
        }
    }
}
