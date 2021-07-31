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
    public class ClinicRepository : IClinicRepository
    {
        
        private ApplicationDataContext _context;

        public ClinicRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        
        public async Task<Clinic> GetById(int id) => await _context.Clinics.FindAsync(id);
        public async Task<IEnumerable<Clinic>> Get() => await _context.Clinics.ToListAsync();
        
        public async Task<Clinic> Add(Clinic entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Clinic entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var clinic = await GetById(id);
            if (clinic == null)
            {
                throw new KeyNotFoundException();
            }
            _context.Clinics.Remove(clinic);
            await _context.SaveChangesAsync();
        }

        public async Task<Persons> AttachPatient(int clinicId, int personId)
        {
            var person = await _context.Persons.FindAsync(personId);
            var clinic = await _context.Clinics.Include(c => c.Patients).FirstOrDefaultAsync(c => c.Id == clinicId);
            if (person != null && clinic != null && (clinic.Patients == null || !clinic.Patients.Contains(person)))
            {
                if (clinic.Patients == null)
                {
                    clinic.Patients = new List<Persons>();
                }
                clinic.Patients.Add(person);
                await Update(clinic);
            }

            return person;
        }

        public async Task<IEnumerable<Persons>> GetPatients(int clinicId, Paginator paginator)
        {
            var clinic = await _context.Clinics.Include(c => c.Patients)
                    .FirstOrDefaultAsync(c => c.Id == clinicId);
            return clinic.Patients.Skip(paginator.Offset).Take(paginator.Limit).ToList();
        }
        
        public async Task<IEnumerable<Clinic>> GetList(Paginator paginator)
        {
            return await _context.Clinics.Skip(paginator.Offset).Take(paginator.Limit).ToListAsync();
        }
    }
}