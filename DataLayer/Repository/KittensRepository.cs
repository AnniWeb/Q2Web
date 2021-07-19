using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using DataLayer.Abstractions.Repository;

namespace DataLayer.Repository
{
    public class KittensRepository : IKittenRepository
    {
        private readonly ApplicationDataContext _context;

        public KittensRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        
        public async Task<Kittens> GetById(int id) => await _context.Kittens.FindAsync(id);
        public async Task<IEnumerable<Kittens>> Get() => await _context.Kittens.ToListAsync();

        public async Task Add(Kittens entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Kittens entity)
        {
            if (entity.Id == null || entity.Id < 1)
            {
                throw new ArgumentException();
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var kitten = await GetById(id);
            if (kitten == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Kittens.Remove(kitten);
            await _context.SaveChangesAsync();
        }
    }
}