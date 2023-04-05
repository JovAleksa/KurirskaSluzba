using KurirskaSluzba.IRepository;
using KurirskaSluzba.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurirskaSluzba.Repository
{
    public class KurirRepository : IKurirRepository
    {
        private readonly AppDbContext _context;

        public KurirRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Kurir kurir)
        {
            _context.Kuriri.Add(kurir);
            _context.SaveChanges();
        }

        public void Delete(Kurir kurir)
        {
            _context.Kuriri.Remove(kurir);
            _context.SaveChanges();
        }

        public IQueryable<Kurir> GetAll()
        {
            try
            {
                return _context.Kuriri;

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public Kurir GetById(int id)
        {
            return _context.Kuriri.Where(s => s.Id == id).FirstOrDefault();


        }

        public IQueryable<Kurir> GetByIme(string vrednost)
        {
            return _context.Kuriri.Where(e => e.Ime.Contains(vrednost)).OrderByDescending(x=>x.GodinaRodjenja).OrderBy(x=>x.Ime);
        }

        public void Update(Kurir kurir)
        {
            _context.Entry(kurir).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
