using AutoMapper;
using AutoMapper.QueryableExtensions;
using KurirskaSluzba.IRepository;
using KurirskaSluzba.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurirskaSluzba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KurirController : ControllerBase
    {
        private readonly IKurirRepository _kurirRepository;
        private readonly IPaketRepository _paketRepository;
        private readonly IMapper _mapper;

        public KurirController(IKurirRepository kurirRepository, IMapper mapper, IPaketRepository paketRepository)
        {
            _kurirRepository = kurirRepository;
            _paketRepository = paketRepository;
            _mapper = mapper;
        }
        [Route("/api/kuriri")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_kurirRepository.GetAll());
        }
        [HttpGet("/api/kuriri/{id}")]
        public IActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sector = _kurirRepository.GetById(id);

                if (sector == null)
                {
                    return NotFound();
                }
                else if (id != sector.Id)
                {
                    return BadRequest();
                }

                return Ok(sector);
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        [HttpGet("/api/kuiriri/nadji/ime={vrednost}")]
        public IActionResult GetByIme(string vrednost)
        {
            return Ok(_kurirRepository.GetByIme(vrednost));
        }


    }
}
