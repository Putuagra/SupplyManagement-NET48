using SupplyManagement_NET48.Contracts;
using SupplyManagement_NET48.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyManagement_NET48.Services
{
    public class VendorService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public IEnumerable<Vendor> Get()
        {
            var vendors = _vendorRepository.GetAll();
            if (!vendors.Any()) return null;
            return vendors;
        }

        public Vendor Get(Guid guid)
        {
            var role = _vendorRepository.GetByGuid(guid);
            if (role is null) return null;
            return role;
        }

        public Vendor Create(Vendor vendorCreate)
        {
            var vendor = new Vendor
            {
                Guid = Guid.NewGuid(),
                Name = vendorCreate.Name,
                Email = vendorCreate.Email,
                PhoneNumber = vendorCreate.PhoneNumber,
                PhotoProfile = vendorCreate.PhotoProfile,
                Sector = "a",
                Type = "a",
                IsAdminApprove = false,
                IsManagerApprove = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdVendor = _vendorRepository.Create(vendor);

            return createdVendor;
        }

        public int Update(Vendor vendorUpdate)
        {
            var getVendor = _vendorRepository.GetByGuid(vendorUpdate.Guid);
            if (getVendor == null) return 0;

            getVendor.Name = vendorUpdate.Name;
            getVendor.ModifiedDate = DateTime.Now;
            getVendor.Email = vendorUpdate.Email;
            getVendor.PhoneNumber = vendorUpdate.PhoneNumber;
            getVendor.PhotoProfile = vendorUpdate.PhotoProfile;
            getVendor.Sector = vendorUpdate.Sector;
            getVendor.Type = vendorUpdate.Type;
            getVendor.IsManagerApprove = vendorUpdate.IsManagerApprove;
            getVendor.IsAdminApprove = vendorUpdate.IsAdminApprove;

            var isUpdate = _vendorRepository.Update(getVendor);

            return isUpdate ? 1 : 0;
        }

        public int Delete(Guid guid)
        {
            var vendor = _vendorRepository.GetByGuid(guid);
            if (vendor == null) return 0;
            var isDelete = _vendorRepository.Delete(vendor);
            return isDelete ? 1 : 0;
        }
    }
}