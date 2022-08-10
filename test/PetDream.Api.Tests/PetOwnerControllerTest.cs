using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetDream.Api.Controllers;
using PetDream.Application.DTOs;
using Xunit;

namespace PetDream.Api.Tests
{
    public class PetOwnerControllerTest : BaseTest
    {
        PetOwnersController _controller; 
        [Fact]
        public async Task Get_MustReturnTheListOfPetOwners()
        {
            _controller = new PetOwnersController(authenticate, petOwnerService);          
            var result = await _controller.Get();
            var petOwnersList = result.Value.ToList();
            Assert.IsType<List<PetOwnerDTO>>(petOwnersList);
            Assert.True(petOwnersList[0].Name == "Fernanda Pessoa");
        }
               
        [Theory]
        [InlineData(1)]
        public async Task GetById_MustReturnAPetOwner(int id)
        {
            _controller = new PetOwnersController(authenticate, petOwnerService);          
            var result = await _controller.GetById(id);
            Assert.IsType<PetOwnerDTO>(result.Value);
            Assert.True(result.Value.Name == "Fernanda Pessoa");
        }

        [Theory]
        [InlineData(0)]
        public async Task GetById_WithInvalidParameters_MustReturnNotFound(int id)
        {
            _controller = new PetOwnersController(authenticate, petOwnerService);          
            var result = await _controller.GetById(id);
            var actionResult = result.Result;
            Assert.True(actionResult is NotFoundObjectResult);
        }

        [Theory]
        [InlineData("63997600031")]
        public async Task GetByCpf_MustReturnAPetOwner(string cpf)
        {
            _controller = new PetOwnersController(authenticate, petOwnerService);          
            var result = await _controller.GetByCpf(cpf);
            Assert.IsType<PetOwnerDTO>(result.Value);
            Assert.True(result.Value.Name == "Fernanda Pessoa");
        }

        [Theory]
        [InlineData("11111111111")]
        public async Task GetByCpf_WithInvalidParameters_MustReturnNotFound(string cpf)
        {
            _controller = new PetOwnersController(authenticate, petOwnerService);          
            var result = await _controller.GetByCpf(cpf);
            var actionResult = result.Result;
            Assert.True(actionResult is NotFoundObjectResult);
        }

        [Theory]
        [InlineData("fernandapessoa@gft.com")]
        public async Task GetByEmail_MustReturnAPetOwner(string email)
        {
            _controller = new PetOwnersController(authenticate, petOwnerService);          
            var result = await _controller.GetByEmail(email);
            Assert.IsType<PetOwnerDTO>(result.Value);
            Assert.True(result.Value.Name == "Fernanda Pessoa");
        }

        [Theory]
        [InlineData("@gft.com")]
        public async Task GetByEmail_WithInvalidParameters_MustReturnNotFound(string email)
        {
            _controller = new PetOwnersController(authenticate, petOwnerService);          
            var result = await _controller.GetByEmail(email);
            var actionResult = result.Result;
            Assert.True(actionResult is NotFoundObjectResult);
        }

        [Theory]
        [InlineData(2)]
        public async Task UpdateAPetOwner_MustReturnThePetOwnerUpdated(int id )
        {
            _controller = new PetOwnersController(authenticate, petOwnerService); 
            PetOwnerDTO petOwner = new PetOwnerDTO
            {
                Id = 2,
                Name = "Carlos Costa",
                Cpf = "91014153018",
                Email = "carlosdiego@gft.com",
                Password = "Gft@123456",
                Phone = "81994583345",
                Status = true
            };

            await _controller.Put(id, petOwner);
            Assert.True(petOwner.Name == "Carlos Costa");
        }

        [Theory]
        [InlineData(2)]
        public async Task UpdateAPetOwner_WithInvalidParameters_MustReturnBadRequest(int id )
        {
            _controller = new PetOwnersController(authenticate, petOwnerService); 
            PetOwnerDTO petOwner = new PetOwnerDTO
            {
                Id = 3,
                Name = "Carlos Costa",
                Cpf = "91014153018",
                Email = "carlosdiego@gft.com",
                Password = "Gft@123456",
                Phone = "81994583345",
                Status = true
            };

            var result = await _controller.Put(id, petOwner);
            Assert.True(result is BadRequestObjectResult);
        }

        [Theory]
        [InlineData(2)]
        public async Task UpdateAPetOwner_WithInvalidParameters_MustReturnNotFound(int id )
        {
            _controller = new PetOwnersController(authenticate, petOwnerService); 
            PetOwnerDTO petOwnerDto = new PetOwnerDTO
            {
                Id = 3,
                Name = "Carlos Costa",
                Cpf = "91014153018",
                Email = "carlosdiego@gft.com",
                Password = "Gft@123456",
                Phone = "81994583345",
                Status = true
            };
            var result = await _controller.Put(id, petOwnerDto);
            Assert.True(result is BadRequestObjectResult);
        }

        [Theory]
        [InlineData(2)]
        public async Task DeleteAPetOwner_MustReturnOk(int id )
        {
            _controller = new PetOwnersController(authenticate, petOwnerService); 
            var result = await _controller.Delete(id);
            Assert.True(result is OkObjectResult);

        }
    }
}