using AutoMapper;
using DataAccess.Db;
using DataAccess.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Services.Abstracts;

namespace Services.Concretes
{
    public class BaseService<TRequestDTO, TEntity, TResponseDTO> 
        : IBaseService<TRequestDTO, TEntity, TResponseDTO> where TEntity : BaseEntity
    {
        protected readonly IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseService(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual TResponseDTO Create(TRequestDTO requestDTO)
        {
            var entity = _mapper.Map<TRequestDTO, TEntity>(requestDTO);
            _dbSet.Add(entity);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedUserId = 1;
            _dbContext.SaveChanges();
            var responseDTO = _mapper.Map<TEntity, TResponseDTO>(entity);
            return responseDTO;
        }
    }
}