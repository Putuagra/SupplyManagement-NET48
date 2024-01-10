using SupplyManagement_NET48.Models;
using System;
using System.Web;

namespace SupplyManagement_NET48.DataTransferObjects.Vendors
{
    public class VendorDtoGet
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoProfile { get; set; }
        public string Sector { get; set; }
        public string Type { get; set; }
        public bool IsAdminApprove { get; set; }
        public bool IsManagerApprove { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public static explicit operator VendorDtoGet(Vendor vendor)
        {
            return new VendorDtoGet
            {
                Name = vendor.Name,
                Email = vendor.Email,
                PhoneNumber = vendor.PhoneNumber,
                PhotoProfile = vendor.PhotoProfile,
                Sector = vendor.Sector,
                Type = vendor.Type,
                IsAdminApprove = vendor.IsAdminApprove,
                IsManagerApprove = vendor.IsManagerApprove
            };
        }
    }
}