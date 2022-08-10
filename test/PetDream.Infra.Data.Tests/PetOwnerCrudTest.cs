using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PetDream.Domain.Entities;
using PetDream.Infra.Data.Context;
using PetDream.Infra.Data.Repositories;
using Xunit;
using Xunit.Priority;

namespace PetDream.Infra.Data.Tests
{    
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class PetOwnerCrudTest : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public PetOwnerCrudTest(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }
      
        [Fact, Priority(1)]
        public async Task GetPetOwnerList()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                PetOwnerRepository _petOwnerRepositoryTest = new PetOwnerRepository(context);
                PetOwner _petOwner = new PetOwner(6, "Carlos Costa", "09724382460", "carloscosta@gft.com", "Gft@123456", "11994554349");
                PetOwner _petOwner2 = new PetOwner(7, "Maria Eduarda", "47720328434", "mariaeduarda@gft.com", "Gft@123456", "11994554349");
                await _petOwnerRepositoryTest.AddAsync(_petOwner);
                await _petOwnerRepositoryTest.AddAsync(_petOwner2);
                var petOwners = await _petOwnerRepositoryTest.GetAsync();
                var petOwnerAt3 = petOwners.ElementAt(3);
                Assert.Equal(_petOwner2.Name, petOwnerAt3.Name);
                Assert.IsType<PetOwner>(petOwners.First());
                Assert.NotNull(petOwners);
                Assert.True(petOwners.Any());
                Assert.True(petOwners.Count() == 4);
                context.Dispose();
            }
        }
        
        [Fact, Priority(2)]
        public async Task CreateNewPetOwner()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                PetOwnerRepository _petOwnerRepositoryTest = new PetOwnerRepository(context);
                PetOwner _petOwner = new PetOwner(3, "José da Silva", "09724382460", "josedasilva@gft.com", "Gft@123456", "11994554349");
                var _createdRegister = await _petOwnerRepositoryTest.AddAsync(_petOwner);
                Assert.NotNull(_createdRegister);
                Assert.Equal(_petOwner.Email, _createdRegister.Email);
                context.Dispose();
            }
        }

        [Fact, Priority(3)]
        public async Task UpdatePetOwner()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                PetOwnerRepository _petOwnerRepositoryTest = new PetOwnerRepository(context);
                PetOwner _petOwner = new PetOwner(4, "Roberta Simões", "09724382460", "robertasimoes@gft.com", "Gft@123456", "11994554349");
                var _createdRegister = await _petOwnerRepositoryTest.AddAsync(_petOwner);
                _petOwner.Update("Roberta Simões", "09724382460", "robertasimoesandrade@gft.com", "Gft@123456", "11994554349", true);
                var _updatedRegister = await _petOwnerRepositoryTest.UpdateAsync(_petOwner);
                Assert.NotNull(_updatedRegister);
                Assert.Equal(_petOwner.Email, _updatedRegister.Email);
                context.Dispose();
            }
        }

        [Fact, Priority(4)]
        public async Task DeletePetOwner()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                PetOwnerRepository _petOwnerRepositoryTest = new PetOwnerRepository(context);
                PetOwner _petOwner = new PetOwner(5, "Carlos Costa", "09724382460", "carloscosta@gft.com", "Gft@123456", "11994554349");
                await _petOwnerRepositoryTest.AddAsync(_petOwner);
                Assert.True(_petOwner.Status);
                await _petOwnerRepositoryTest.DeleteAsync(_petOwner);
                Assert.NotNull(_petOwner);
                Assert.Equal("Carlos Costa", _petOwner.Name);
                Assert.False(_petOwner.Status);
                context.Dispose();
            }
        }

        [Fact, Priority(5)]
        public async Task GetPetOwnerById()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                PetOwnerRepository _petOwnerRepositoryTest = new PetOwnerRepository(context);
                var petOwner = await _petOwnerRepositoryTest.GetByIdAsync(1);
                Assert.NotNull(petOwner);
                Assert.Equal("Fernanda Pessoa", petOwner.Name);
                context.Dispose();
            }
        }

        [Fact, Priority(6)]
        public async Task GetPetOwnerByCpf()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                PetOwnerRepository _petOwnerRepositoryTest = new PetOwnerRepository(context);
                var fernandaPessoa = await _petOwnerRepositoryTest.GetByCpfAsync("63997600031");
                Assert.Equal("63997600031", fernandaPessoa.Cpf);
                context.Dispose();
            }
        }

        [Fact, Priority(7)]
        public async Task GetPetOwnerByEmail()
        {
            using (var context = _serviceProvider.GetService<ApplicationDbContext>())
            {
                PetOwnerRepository _petOwnerRepositoryTest = new PetOwnerRepository(context);
                PetOwner _petOwner = new PetOwner(10, "Tainá Bandeiras", "09724382460", "tainabandeiras@gft.com", "Gft@123456", "11994554349");
                await _petOwnerRepositoryTest.AddAsync(_petOwner);
                var petOwner = await _petOwnerRepositoryTest.GetByEmailAsync("tainabandeiras@gft.com");
                Assert.Equal(_petOwner, petOwner);
                context.Dispose();
            }
        }
    }
}