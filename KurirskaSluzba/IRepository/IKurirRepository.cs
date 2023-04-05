using KurirskaSluzba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurirskaSluzba.IRepository
{
    public interface IKurirRepository
    {
        IQueryable<Kurir> GetAll();
        Kurir GetById(int id);
        void Add(Kurir kurir);
        void Update(Kurir kurir);
        void Delete(Kurir kurir);
        IQueryable<Kurir> GetByIme(string vrednost);
        
    }
}
