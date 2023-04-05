using KurirskaSluzba.IRepository;
using KurirskaSluzba.Models;
using KurirskaSluzba.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurirskaSluzba.Repository
{
    public class PaketRepository : IPaketRepository
    {
        private readonly AppDbContext _context;
        public PaketRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Paket paket)
        {
            _context.Paketi.Add(paket);
            _context.SaveChanges();
        }

        public IQueryable<KuririPaketiDTO> Brojnost()
        {
            var response = _context.Paketi.GroupBy(x => x.KurirId).Select(s =>
                      new KuririPaketiDTO()
                      {
                          Paketi = _context.Paketi.Where(a => a.KurirId== s.Key).Count(),//(int)s.Sum(x => x.Id),
                          KurirIme = _context.Kuriri.Where(z => z.Id== s.Key).Select(t => t.Ime).Single()
                      }
                      ).OrderByDescending(a => a.Paketi);

            return response;
        }

        public void Delete(Paket paket)
        {
            _context.Paketi.Remove(paket);
            _context.SaveChanges();
        }

        public IQueryable<Paket> GetAll()
        {
            return _context.Paketi.Include(s => s.Kurir);
        }

        public Paket GetById(int id)
        {
            return _context.Paketi.Include(s => s.Kurir).Where(e => e.Id == id).FirstOrDefault();
        }

        public IQueryable<Paket> GetKuririPoVrednosti(int vrednost)
        {
            return _context.Paketi.Include(o => o.Kurir).Where(x => x.Tezina< vrednost).OrderByDescending(a => a.Kurir.Ime);
        }

        public IQueryable<Paket> PretragaDvaPaketa(decimal firstvalue, decimal secondvalue)//SearchBetweenTwoNumberDTO filter)
        {
            return _context.Paketi.Include(p => p.Kurir).Where(p => p.Tezina > firstvalue && p.Tezina < secondvalue).OrderByDescending(p => p.Tezina);

            //return _context.Paketi.Include(c => c.Kurir).Where(p => p.Tezina > filter.FirstValue && p.Tezina < filter.SecondValue).OrderByDescending(p => p.Tezina);
        }

        public void Update(Paket paket)
        {
            _context.Entry(paket).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;
            }
        }
    }
}
