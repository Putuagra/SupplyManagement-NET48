using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Data;
using SupplyManagement_NET48.DataTransferObjects.AccountVendors;
using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Services
{
    public class AccountVendorService
    {
        private readonly IAccountVendorRepository _accountVendorRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly SupplyManagementDbContext _context;

        public AccountVendorService(IAccountVendorRepository accountVendorRepository, SupplyManagementDbContext context, IVendorRepository vendorRepository)
        {
            _accountVendorRepository = accountVendorRepository;
            _context = context;
            _vendorRepository = vendorRepository;
        }

        public IEnumerable<AccountVendor> Get()
        {
            var accountVendors = _accountVendorRepository.GetAll();
            if (!accountVendors.Any()) return null;
            return accountVendors;
        }

        public AccountVendor Get(Guid guid)
        {
            var accountVendor = _accountVendorRepository.GetByGuid(guid);
            if (accountVendor is null) return null;
            return accountVendor;
        }

        public AccountVendor Create(AccountVendor accountVendorCreate)
        {
            var accountVendor = new AccountVendor
            {
                Guid = accountVendorCreate.Guid,
                Password = accountVendorCreate.Password,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdAccountVendor = _accountVendorRepository.Create(accountVendor);

            return createdAccountVendor;
        }

        public int Update(AccountVendor accountVendorUpdate)
        {
            var getAccountVendor = _accountVendorRepository.GetByGuid(accountVendorUpdate.Guid);
            if (getAccountVendor == null) return 0;

            getAccountVendor.Guid = accountVendorUpdate.Guid;
            getAccountVendor.Password = accountVendorUpdate.Password;
            getAccountVendor.ModifiedDate = DateTime.Now;

            var isUpdate = _accountVendorRepository.Update(getAccountVendor);

            return isUpdate ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var accountVendor = _accountVendorRepository.GetByGuid(guid);
            if (accountVendor == null) return 0;
            var isDelete = _accountVendorRepository.Delete(accountVendor);
            return isDelete ? 1 : 0;
        }

        public bool Register(AccountVendorDtoRegister registerDto)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var vendor = new Vendor
                {
                    Guid = Guid.NewGuid(),
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber,
                    PhotoProfile = registerDto.PhotoProfile,
                    Sector = "a",
                    Type = "a",
                    IsAdminApprove = false,
                    IsManagerApprove = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                _vendorRepository.Create(vendor);

                var accountVendor = new AccountVendor
                {
                    Guid = vendor.Guid,
                    Password = registerDto.Password,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                _accountVendorRepository.Create(accountVendor);

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