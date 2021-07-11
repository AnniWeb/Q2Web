using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public bool Add(Persons entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                return false;
            }
 
            return true;
        }

        public IEnumerable<Persons> Get()
        {
            return _context.Persons.ToList();
        }

        public bool Update(Persons entity)
        {
            try
            {
                if (entity.Id == null || entity.Id < 1)
                {
                    throw new ArgumentException();
                }
                var person = GetById(entity.Id);
                if (person == null)
                {
                    throw new KeyNotFoundException();
                }
                _context.Update(person);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                return false;
            }
 
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var person = GetById(id);
                if (person == null)
                {
                    throw new KeyNotFoundException();
                }

                _context.Remove(person);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }

        public Persons GetById(int id)
        {
            return _context.Persons.Where(person => person.Id == id)?.First();
        }

        public IEnumerable<Persons> GetListWithNav(int offset, int limit)
        {
            return _context.Persons.Skip(offset).Take(limit).ToList();
        }

        public IEnumerable<Persons> Search(string term)
        {
            return _context.Persons.Where(person
                => EF.Functions.Like(person.FirstName, $"%{term}%") 
                   || EF.Functions.Like(person.SecondName, $"%{term}%"));
        }
    }
}