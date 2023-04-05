using KurirskaSluzba.Models;
using KurirskaSluzba.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurirskaSluzba.IRepository
{
    public interface IPaketRepository
    {
        IQueryable<Paket> GetAll();
        Paket GetById(int id);
        void Add(Paket paket);
        void Update(Paket paket);
        void Delete(Paket paket);
        IQueryable<Paket> GetKuririPoVrednosti(int vrednost);
        IQueryable<KuririPaketiDTO> Brojnost();
        IQueryable<Paket> PretragaDvaPaketa(decimal firstvalue, decimal secondvalue);

    }
}
