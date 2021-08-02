using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Model;
using DataLayer.Abstractions;
using DataLayer.Abstractions.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private ApplicationDataContext _context;

        public PersonRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        
        public async Task<Persons> GetById(int id) => await _context.Persons.FindAsync(id);
        public async Task<IEnumerable<Persons>> Get() => await _context.Persons.ToListAsync();

        public async Task<Persons> Add(Persons entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
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

        public async Task AttachToClinic(int personId, int clinicId)
        {
            var person = await _context.Persons.FindAsync(personId);
            var clinic = await _context.Clinics.FindAsync(clinicId);
            if (person != null && clinic != null && (person.Clinics == null || !person.Clinics.Contains(clinic)))
            {
                if (person.Clinics == null)
                {
                    person.Clinics = new List<Clinic>();
                }
                person.Clinics.Add(clinic);
                await Update(person);
            }
        }

        public async Task<IEnumerable<Clinic>> GetClinics(int personId, Paginator paginator)
        {
            var clinic = await _context.Persons.Include(e => e.Clinics)
                .FirstOrDefaultAsync(e => e.Id == personId);
            return clinic.Clinics.Skip(paginator.Offset).Take(paginator.Limit).ToList();
        }
    }
}