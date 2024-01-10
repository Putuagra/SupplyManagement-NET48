using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Data;
using SupplyManagement_NET48.DataTransferObjects.Accounts;
using SupplyManagement_NET48.Models;
using SupplyManagement_NET48.Utilities.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly SupplyManagementDbContext _supplyManagementDbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, SupplyManagementDbContext supplyManagementDbContext)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _supplyManagementDbContext = supplyManagementDbContext;
        }

        public IEnumerable<Account> Get()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any()) return null;
            return accounts;
        }

        public Account Get(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null) return null;
            return account;
        }

        public Account Create(Account accountCreate)
        {
            var account = new Account
            {
                Guid = Guid.NewGuid(),
                Password = accountCreate.Password,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdAccount = _accountRepository.Create(account);

            return createdAccount;
        }

        public int Update(Account accountUpdate)
        {
            var getAccount = _accountRepository.GetByGuid(accountUpdate.Guid);
            if (getAccount == null) return 0;

            getAccount.Guid = accountUpdate.Guid;
            getAccount.Password = accountUpdate.Password;
            getAccount.ModifiedDate = DateTime.Now;

            var isUpdate = _accountRepository.Update(getAccount);

            return isUpdate ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account == null) return 0;
            var isDelete = _accountRepository.Delete(account);
            return isDelete ? 1 : 0;
        }

        public bool Register(AccountDtoRegister registerDto)
        {
            var transaction = _supplyManagementDbContext.Database.BeginTransaction();
            try
            {
                var employee = new Employee
                {
                    Guid = Guid.NewGuid(),
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    Email = registerDto.Email,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                _employeeRepository.Create(employee);

                var account = new Account
                {
                    Guid = employee.Guid,
                    Password = HashingHandler.Hash(registerDto.Password),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                _accountRepository.Create(account);

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}