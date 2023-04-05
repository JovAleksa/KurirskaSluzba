using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KurirskaSluzba.Models
{
    public class Kurir
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120, ErrorMessage ="Dozvoljeno je do 120 karaktera.")]
        public string Ime { get; set; }
        [Required]
        [Range(1940,2005, ErrorMessage ="Dozvoljen raspon godina od 1940 do 2005.")]
        public int GodinaRodjenja { get; set; }
    }
}
