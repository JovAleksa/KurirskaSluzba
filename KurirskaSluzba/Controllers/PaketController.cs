using AutoMapper;
using AutoMapper.QueryableExtensions;
using KurirskaSluzba.IRepository;
using KurirskaSluzba.Models;
using KurirskaSluzba.Models.DTO;
using Microsoft.AspNetCore.Authorization;
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
    public class PaketController : ControllerBase
    {
        private readonly IPaketRepository _paketRepository;
        private readonly IMapper _mapper;
        public PaketController(IPaketRepository paketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _paketRepository = paketRepository;

        }
        [HttpGet("/api/paketi/{id}")]
        public IActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = _paketRepository.GetById(id);

                if (employee == null)
                {
                    return NotFound();
                }
                else if (id != employee.Id)
                {
                    return BadRequest();
                }

                return Ok(_mapper.Map<PaketDTO>(employee));
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        [Route("/api/paketi")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_paketRepository.GetAll().ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());
        }
        [HttpPost("/api/paketi")]
        public IActionResult Add(Paket paket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _paketRepository.Add(paket);
            return CreatedAtAction("GetAll", new { id = paket.Id }, _mapper.Map<PaketDTO>(paket));
        }
        [HttpGet]
        [Route("/api/dostave/granica={vrednost}")]
        public IActionResult GetKuririPoVrednosti(int vrednost)
        {
            var response = _paketRepository.GetKuririPoVrednosti(vrednost);
            if (response == null)
            {
                return BadRequest();
            }
            else if (!response.Any())
            {
                return NotFound();
            }
            return Ok(response.ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());
        }
        [HttpGet("/api/stanje/")]
        public IActionResult KuririPaketi()
        {
            return Ok(_paketRepository.Brojnost().ToList());

            //return Ok(_paketRepository.Brojnost().ProjectTo<KuririPaketiDTO>(_mapper.ConfigurationProvider).ToList());
        }
        [Authorize]
        [HttpPut]
        [Route("/api/paketi")]
        public IActionResult Put(Paket paket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            try
            {
                _paketRepository.Update(paket);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<PaketDTO>(paket));
        }
        [HttpDelete]
        [Route("/api/paketi/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var paketi = _paketRepository.GetById(id);
            if (paketi == null)
            {
                return NotFound();
            }

            _paketRepository.Delete(paketi);
            return NoContent();
        }
        [HttpPost("/api/pretraga")]
       [Authorize]
        public IActionResult PretragaDvaPaketa([FromBody] SearchBetweenTwoNumberDTO filter)
        {
            if (filter.FirstValue == filter.SecondValue || filter.FirstValue > filter.SecondValue)
            {
                return BadRequest();
            }
            try
            {
                var response = _paketRepository.PretragaDvaPaketa(filter.FirstValue, filter.SecondValue);
                if (response == null)
                {
                    return BadRequest();
                }
                else if (!response.Any())
                {
                    return NotFound();
                }
                return Ok(response.ProjectTo<PaketDTO>(_mapper.ConfigurationProvider).ToList());

            }
            catch (System.Exception)
            {

                throw;
            }

        }
        [HttpPut]
        [Route("/api/paketi/{id}")]
        public IActionResult Put(int id, Paket paket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paket.Id)
            {
                return BadRequest();
            }

            try
            {
                _paketRepository.Update(paket);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<PaketDTO>(paket));
        }
    }
}
