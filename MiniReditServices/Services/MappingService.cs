using MiniReditRepository.Entities;
using MiniReditServices.DTO;
using Service.Services.Base;
using System;

namespace Service.Services
{
    /// <summary>
    /// The MappingService. Used for Automapper so only 1 mapper is needed.
    /// </summary>
    public class MappingService : BaseService
    {
        public readonly AutoMapper.IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingService"/> class.
        /// </summary>
        public MappingService()
        {
            AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {

                // Boards
                cfg.CreateMap<Boards, BoardsDTO>();
                cfg.CreateMap<BoardsDTO, Boards>();

                // Posts
                cfg.CreateMap<Posts, PostsDTO>();
                cfg.CreateMap<PostsDTO, Posts>();


                // Users
                cfg.CreateMap<Users, UsersDTO>();
                cfg.CreateMap<UsersDTO, Users>();

            });

            try
            {
                _mapper = mapperConfig.CreateMapper();
            }
            catch (Exception ex)
            {
                LogError("Failed to create mappings", ex);
            }

        }
    }
}
