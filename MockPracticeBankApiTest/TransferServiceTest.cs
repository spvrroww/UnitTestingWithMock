using MockPractice.Domain;
using MockPractice.Repository.IRepository;
using MockPracticeBankApi.Models;
using MockPracticeBankApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockPracticeBankApiTest
{
    public class TransferServiceTest
    {
        private Guid CustomerId = new();
        private readonly Mock<IRepositoryManager> _repositoryManagerMock = new Mock<IRepositoryManager>();
        private readonly TransferService sut;
        public TransferServiceTest()
        {
             sut = new TransferService(_repositoryManagerMock.Object);
        }

        [Test]
        public async Task MakeTransfer_ReturnFailedTransfer_WhenInputAmountIsLessThanZero()
        {
            //act
            var response = await sut.MakeTransfer(CustomerId, It.IsAny<int>(), -1);

            //assert
            Assert.That(response.Equals(new TransferResponse(false, "Invalid Input Amount")));
        }

        [Test]
        public async Task MakeTransfer_ReturnFailedTransfer_WhenAccountNumberIsInvalid()
        {
            //arrange
            Random random = new Random();
            int accountNumber = random.Next();
            while(accountNumber >= 100000000 && accountNumber <= 999999999)
            {
                accountNumber = random.Next();
            }

            //act
             var response = await sut.MakeTransfer(CustomerId, accountNumber, 2);

            //assert
            Assert.That(response.Equals(new TransferResponse(false, "Invalid Account Number")));
        }

        [Test]
        public async Task MakeTransfer_ReturnFailedTransfer_WhenRecipientsAccountDoesNotExist()
        {
            //arrange
            int accountNumber = 200000000;
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.GetByAccountNo(accountNumber)).ReturnsAsync(()=> null);

            //act

            var response= await sut.MakeTransfer(CustomerId, accountNumber, 20);

            //assert
            Assert.That(response.Equals(new TransferResponse(false, "reciepient account number is invalid, confirm and try again")));
        }

        [Test]
        public async Task MakeTransfer_ReturnFailedTransfer_WhenSendersAccountDoesNotExist()
        {
            //arrange
            int accountNumber = 200000000;
            BankAccount bankAccount = new()
            {
                Id = new Guid(),
                AccountNo = accountNumber.ToString().ToCharArray(),
                Balance = 200,
                CustomerId = new Guid()
            };
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.GetByAccountNo(accountNumber)).ReturnsAsync(bankAccount);
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.Get(CustomerId)).ReturnsAsync(() => null);

            //act

            var response = await sut.MakeTransfer(CustomerId, accountNumber, 20);

            //assert
            Assert.That(response.Equals(new TransferResponse(false, "Customer does not have an active bank account")));
        }

        [Test]
        public async Task MakeTransfer_ReturnFailedTransfer_WhenSendersAccountBalanceIsLessThanTransferAmount()
        {
            //arrange
            int reciepientAccountNumber = 200000000;
            int senderAccountNumber = 300000000;
            BankAccount recipientbankAccount = new()
            {
                Id = new Guid(),
                AccountNo = reciepientAccountNumber.ToString().ToCharArray(),
                Balance = 200,
                CustomerId = new Guid()
            };

            BankAccount senderBankAccount = new()
            {
                Id = CustomerId,
                AccountNo = senderAccountNumber.ToString().ToCharArray(),
                Balance = 200,
                CustomerId = new Guid()
            };
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.GetByAccountNo(reciepientAccountNumber)).ReturnsAsync(recipientbankAccount);
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.Get(CustomerId)).ReturnsAsync(senderBankAccount);

            //act

            var response = await sut.MakeTransfer(CustomerId, reciepientAccountNumber, 300);

            //assert
            Assert.That(response.Equals(new TransferResponse(false, "Transfer failed due to insufficient funds")));
        }

        [Test]
        public async Task MakeTransfer_ReturnArguementNullExceptionError_WhenRecipientsAccountFailsToUpdate()
        {
            //arrange
            int reciepientAccountNumber = 200000000;
            int senderAccountNumber = 300000000;

            BankAccount recipientbankAccount = new()
            {
                Id = new Guid(),
                AccountNo = reciepientAccountNumber.ToString().ToCharArray(),
                Balance = 200,
                CustomerId = new Guid()
            };
            BankAccount senderBankAccount = new()
            {
                Id = CustomerId,
                AccountNo = senderAccountNumber.ToString().ToCharArray(),
                Balance = 200,
                CustomerId = new Guid()
            };

            _repositoryManagerMock.Setup(x => x.BankAccountRepository.GetByAccountNo(reciepientAccountNumber)).ReturnsAsync(recipientbankAccount);
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.Get(CustomerId)).ReturnsAsync(senderBankAccount);
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.Update(recipientbankAccount)).ReturnsAsync(()=>null);

            //act + assert
           Assert.ThrowsAsync(typeof(ArgumentNullException),()=> sut.MakeTransfer(CustomerId, reciepientAccountNumber, 100));
           
        }

        [Test]
        public async Task MakeTransfer_ReturnArguementNullExceptionError_WhenSendersAccountFailsToUpdate()
        {
            //arrange
            int reciepientAccountNumber = 200000000;
            int senderAccountNumber = 300000000;

            BankAccount recipientbankAccount = new()
            {
                Id = new Guid(),
                AccountNo = reciepientAccountNumber.ToString().ToCharArray(),
                Balance = 200,
                CustomerId = new Guid()
            };
            BankAccount senderBankAccount = new()
            {
                Id = CustomerId,
                AccountNo = senderAccountNumber.ToString().ToCharArray(),
                Balance = 200,
                CustomerId = new Guid()
            };

            _repositoryManagerMock.Setup(x => x.BankAccountRepository.GetByAccountNo(reciepientAccountNumber)).ReturnsAsync(recipientbankAccount);
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.Get(CustomerId)).ReturnsAsync(senderBankAccount);
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.Update(recipientbankAccount)).ReturnsAsync(recipientbankAccount);
            _repositoryManagerMock.Setup(x => x.BankAccountRepository.Update(senderBankAccount)).ReturnsAsync(() => null);

            //act + assert
            Assert.ThrowsAsync(typeof(ArgumentNullException), () => sut.MakeTransfer(CustomerId, reciepientAccountNumber, 100));

        }



    }
}
