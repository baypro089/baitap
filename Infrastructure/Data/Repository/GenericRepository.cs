using Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class GenericRepository<T, TDto> : IGenericRepository<T, TDto> where T : class
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<T>();
        }

        // Lấy tất cả các bản ghi và chuyển sang DTO
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        // Lấy bản ghi theo Id
        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        // Thêm mới bản ghi
        public async Task AddAsync(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Cập nhật bản ghi
        public async Task UpdateAsync(int id, TDto dto)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            _mapper.Map(dto, entity);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Xóa bản ghi
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Entity not found");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
