using adventure_works_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;

namespace adventure_works_project.Models
{
    public static class AddressMethods
    {
        // "/address/create"
        public static IResult Create(AdventureWorksLt2019Context db, Address address)
        {
            address.Rowguid = Guid.NewGuid();
            address.ModifiedDate= DateTime.Now;

            db.Add(address);
            db.SaveChanges();

            return Results.Created($"/address/created?id={address.AddressId}",new {
                Message = "Success! Created new address",
                Address = address });
        }

        //"/address/{id?}"
        public static IResult Read(int? id, AdventureWorksLt2019Context db)
        {
            if (id != null)
            {
                return Results.Ok(db.Addresses.Find(id));
            }

            return Results.Ok(db.Addresses.ToList());
        }

        ///"/address/update/{id}"
        public static IResult Update(int id, AdventureWorksLt2019Context db, Address address)
        {
            if (id == null)
            {
                return Results.NotFound();
            }

            // search for address
            Address editingAddress = db.Addresses.Find(id);
            
            if (editingAddress == null)
            {
                address.Rowguid = Guid.NewGuid();
                address.ModifiedDate= DateTime.Now;

                db.Addresses.Add(address);
                db.SaveChanges();

                return Results.Created($"/address/update/?id={address.AddressId}", new
                {
                    message = "Address did not exist, new Address created.",
                    Address = address
                });
            }

            //maintain Guid 
            Guid maintainGuid = editingAddress.Rowguid;

            editingAddress = address;

            editingAddress.AddressId = id;
            editingAddress.Rowguid= maintainGuid;
            editingAddress.ModifiedDate= DateTime.Now;

            return Results.Ok(editingAddress);
            
        }

        //"/address/delete/{id}"
        public static IResult Delete(int id, AdventureWorksLt2019Context db)
        {
            if (id == null)
            {
                return Results.NotFound();
            }

            Address addressToDelete = db.Addresses.Find(id);

            if (addressToDelete != null)
            {
                db.Remove(addressToDelete); 
                db.SaveChanges();

                return Results.Created($"/address/delete/?id={addressToDelete.AddressId}", new
                {
                    Message = $"Successfully deleted Address: {addressToDelete.AddressLine1}",
                    Address = addressToDelete
                });
            }

            return Results.BadRequest();
        }

        public static IResult Details(int id, AdventureWorksLt2019Context db)
        {
            
            var address = db.Addresses
                .Select(ad => new
                {
                    ad.AddressId,
                    ad.AddressLine1,
                    ad.AddressLine2,
                    ad.City,
                    ad.StateProvince,
                    ad.CountryRegion,
                    ad.PostalCode,

                    Customers = ad.CustomerAddresses.Select(ca => new
                    {
                        ca.Customer
                    })
                }).FirstOrDefault(ad => ad.AddressId == id);

            return address != null ? Results.Ok(address) : Results.NotFound();

        }
    }
}



   

