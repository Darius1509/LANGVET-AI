using Domain.Common;
using LangVet.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LangVet.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly LangVetContext context;

        public BaseRepository(LangVetContext context)
        {
            this.context = context;
        }

        public async Task<Result<T>> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public async Task<Result<T>> DeleteAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            context.Set<T>().Remove(result.Value);
            await context.SaveChangesAsync();
            return Result<T>.Success(result.Value);
        }

        public async Task<Result<T>> FindByIdAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return Result<T>.Failure($"Entity with id {id} not found.");
            }
            return Result<T>.Success(entity);
        }

        public async Task<Result<IReadOnlyList<T>>> FindAllAsync()
        {
            var entities = await context.Set<T>().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(entities);
        }

        public async Task<Result<T>> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }
    }
}
