using System;
using System.Collections.Generic;
using System.Linq;
using Database.Model;

namespace Database.Repository
{
    public class KittensRepository : IKittenRepository
    {
        private ApplicationDataContext _context;

        public KittensRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        
        public bool Add(Kittens entity)
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

        public IEnumerable<Kittens> Get()
        {
            return _context.Kittens.ToList();
        }

        public bool Update(Kittens entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}