using adventure_works_project.Models;
using Microsoft.AspNetCore.Mvc;
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

            return Results.Created($"/address/created?id={address.AddressId}", address);
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
            // search for address
            Address editingAddress = db.Addresses.Find(id);
            
            if (editingAddress == null)
            {
                Address newAddress = address;
                newAddress.Rowguid = Guid.NewGuid();
                newAddress.ModifiedDate= DateTime.Now;

                db.Addresses.Add(newAddress);
                db.SaveChanges();

                return Results.Ok(newAddress);
            }
            else
            {
                //maintain Guid 
                Guid maintainGuid = editingAddress.Rowguid;

                editingAddress = address;

                editingAddress.AddressId = id;
                editingAddress.Rowguid= maintainGuid;
                editingAddress.ModifiedDate= DateTime.Now;

                return Results.Ok(editingAddress);
            }
        }

        //"/address/delete/{id}"
        public static IResult Delete(int id, AdventureWorksLt2019Context db)
        {
            Address addressToDelete = db.Addresses.Find(id);
            if (addressToDelete != null)
            {
                db.Remove(addressToDelete); 
                db.SaveChanges();   
            }

            return Results.Ok();
        }
    }
}



   

