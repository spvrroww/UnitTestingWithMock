using MockPractice.Domain;
using MockPractice.Repository.IRepository;
using MockPracticeBankApi.Models;
using MockPracticeBankApi.Services;
using MockPracticeBankApi.Services.IService;
using Moq;
using System;

namespace MockPracticeBankApiTest
{
    public class DepositServiceTest
    {
        private readonly DepositService _depositService;
        private readonly Mock<IRepositoryManager> _repositoryManagerMock = new();
        private Guid customerId = new Guid();

     
        public DepositServiceTest()
        {
            _depositService = new DepositService(_repositoryManagerMock.Object);
        }

       
        [Test]
        public void MakeDeposit_UnSuccessfulDeposit_WhenAmoutIsLessThanZero()
        {
            //Arrange

            //Act
             var response = _depositService.MakeDeposit(customerId, -1).GetAwaiter().GetResult();
            var expected = new DepositResponse(false, "Invalid input amount", null);
            //Assert
            Assert.That(response.Equals(expected));
        }

        [Test]
        public void MakeDeposit_UnSuccessfulDeposit_WhenBackAccountDoesNotExist()
        {
            //arrange
            List<BankAccount> bankAccounts = new();
            _repositoryManagerMock.Setup(rm => rm.BankAccountRepository.GetBy(x => x.CustomerId == It.IsAny<Guid>())).ReturnsAsync(bankAccounts);
            var expected = new DepositResponse(false, "Customer does not have an active bank account");
            //act
            var response = _depositService.MakeDeposit(new Guid(), 400).GetAwaiter().GetResult();

            //assert
            Assert.That(response.Equals(expected));
            
        }
    }
}