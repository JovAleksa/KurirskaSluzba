using AutoMapper;
using KurirskaSluzba.Controllers;
using KurirskaSluzba.IRepository;
using KurirskaSluzba.Models;
using KurirskaSluzba.Models.DTO;
using KurirskaSluzba.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestProjectFinalni
{
    public class UnitTest1
    {
        [Fact]
        public void GetPaket_ValidId_ReturnsObject()
        {
            // Arrange
            Paket employee = new Paket()
            {
                Id = 1,
                Primalac = "Test1",
                Posiljalac = "Test2",
                Tezina = 1,
                CenaPostarine= 710,
                                KurirId = 1,
                Kurir = new Kurir() { Id = 1, Ime = "Proba1", GodinaRodjenja = 1990 },
            };

            PaketDTO employeeDto = new PaketDTO()
            {
                Id = 1,
                Primalac = "Test1",
                Posiljalac = "Test2",
                Tezina = 1,
                CenaPostarine= 710,
                KurirIme= "Proba1",
                KurirGodinaRodjenja = 1990
            };

            var mockRepository = new Mock<IPaketRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(employee);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.GetById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            PaketDTO dtoResult = (PaketDTO)actionResult.Value;
            Assert.Equal(dtoResult, employeeDto);

        }
        [Fact]
        public void PutPaket_InvalidPaketId_ReturnBadRequest()
        {
            Paket paket = new Paket()
            {
                Id = 12,
                Primalac = "Test1",
                Posiljalac = "Test1",
                CenaPostarine = 300,
                Tezina = 0.5M,
                KurirId = 1,
                Kurir = new Kurir() { Id = 1, Ime = "Test1", GodinaRodjenja = 1999 }
            };
            var mockRepository = new Mock<IPaketRepository>();
                        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketController(mockRepository.Object, mapper);

            var actionResult = controller.Put(1, paket) as BadRequestResult;

            Assert.NotNull(actionResult);
        }


        [Fact]
        public void DeletePaket_InvalidId_ReturnsNotFound()
        {
            var mockRepository = new Mock<IPaketRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketController(mockRepository.Object, mapper);

            var actionResult = controller.Delete(12) as NotFoundResult;

            Assert.NotNull(actionResult);
        }
        [Fact]
        public void PostPretraga_ValidPretraga_ReturnsCollection()
        {
            List<Paket> paketi = new List<Paket>()
            {
                new Paket()
                {
                    Id = 1,
                    Primalac = "Test1",
                    Posiljalac = "Test1",
                    CenaPostarine = 700,
                    Tezina = 1.5M,
                    KurirId = 1,
                    Kurir = new Kurir() { Id = 1, Ime = "Test1", GodinaRodjenja = 1999 }
                },
                 new Paket()
                {
                    Id = 2,
                    Primalac = "Test2",
                    Posiljalac = "Test2",
                    CenaPostarine = 600,
                    Tezina = 0.5M,
                    KurirId = 2,
                    Kurir = new Kurir() { Id = 2, Ime = "Test2", GodinaRodjenja = 1988 }
                },
                  new Paket()
                {
                    Id = 3,
                    Primalac = "Test3",
                    Posiljalac = "Test3",
                    CenaPostarine = 500,
                    Tezina = 2.5M,
                    KurirId = 3,
                    Kurir = new Kurir() { Id = 3, Ime = "Test3", GodinaRodjenja = 2001 }
                }
            };

            List<PaketDTO> paketiDTO = new List<PaketDTO>()
            {
                new PaketDTO()
                {
                    Id = 1,
                    Primalac = "Test1",
                    Posiljalac = "Test1",
                    CenaPostarine = 700,
                    Tezina = 1.5M,
                    KurirIme = "Test1",
                    KurirGodinaRodjenja=1999
                },
                 new PaketDTO()
                {
                    Id = 2,
                    Primalac = "Test2",
                    Posiljalac = "Test2",
                    CenaPostarine = 600,
                    Tezina = 0.5M,
                    KurirIme = "Test2",
                    KurirGodinaRodjenja=1988

                },
                  new PaketDTO()
                {
                    Id = 3,
                    Primalac = "Test3",
                    Posiljalac = "Test3",
                    CenaPostarine = 500,
                    Tezina = 2.5M,
                    KurirIme = "Test3",
                    KurirGodinaRodjenja=2001
                },
            };
            SearchBetweenTwoNumberDTO searchDTO = new SearchBetweenTwoNumberDTO()
            {
                FirstValue = 0.2M,
                SecondValue = 1.7M
            };

            var mockRepository = new Mock<IPaketRepository>();
            mockRepository.Setup(x => x.PretragaDvaPaketa(searchDTO.FirstValue, searchDTO.SecondValue)).Returns(paketi.AsQueryable());


            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new PaketProfile()));
            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new PaketController(mockRepository.Object, mapper);

            // Act
            var actionResult = controller.PretragaDvaPaketa(searchDTO) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.NotNull(actionResult.Value);

            List<PaketDTO> listResult = (List<PaketDTO>)actionResult.Value;

            Assert.Equal(paketiDTO[0], listResult[0]);
            Assert.Equal(paketiDTO[1], listResult[1]);
            Assert.Equal(paketiDTO[2], listResult[2]);
        }
    }
}
