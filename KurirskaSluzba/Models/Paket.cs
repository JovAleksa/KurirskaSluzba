using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KurirskaSluzba.Models
{
    public class Paket
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120,ErrorMessage ="Duzina moze biti maksimalno 90 karaktera")]
        public string Posiljalac { get; set; }
        [Required]
        [StringLength(120, ErrorMessage = "Duzina moze biti maksimalno 120 karaktera")]
        public string Primalac { get; set; }
        [Required]
       // [Range(0.1, 9.99, ErrorMessage = "Price must be greater than 0.00")]
        //[Column(TypeName = "decimal(1, 2)")]        
        public decimal Tezina { get; set; }
        [Required]
        [Range(250,10000, ErrorMessage ="Raspon je dozvoljen u rasponu od 250 do 10000")]
        public int CenaPostarine { get; set; }
        public int KurirId { get; set; }
        public Kurir Kurir { get; set; }

        public IQueryable<Paket> AsQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
