using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private ApplicationDataContext _context;
        private IPersonRepository _personRepositoryImplementation;

        public PersonRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        
        public async Task<Persons> GetById(int id) => await _context.Persons.FindAsync(id);
        public async Task<IEnumerable<Persons>> Get() => await _context.Persons.ToListAsync();

        public async Task Add(Persons entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Persons entity)
        {
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
            _context.Persons.Remove(kitten);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Persons>> GetListWithNav(int offset, int limit)
        {
            return await _context.Persons.Skip(offset).Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<Persons>> Search(string term)
        {
            return await _context.Persons.Where(person
                => EF.Functions.Like(person.FirstName, $"%{term}%") 
                   || EF.Functions.Like(person.SecondName, $"%{term}%")).ToListAsync();
        }
    }
}