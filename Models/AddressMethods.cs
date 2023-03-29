using adventure_works_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace adventure_works_project.Models
{
    public static class AddressMethods
    {
        // "/address/create"
        public static IResult Create(AdventureWorksLt2019Context db, Address address)
        {
            address.Rowguid = Guid.NewGuid();

            db.Add(address);
            db.SaveChanges();

            return Results.Created($"/address/created?id={address.AddressId}", address);
        }

        //"/address/{id?}"
        public static IResult Read(int? id, AdventureWorksLt2019Context db)
        {
            if (id != null)
            {
                return Results.Ok(db.Addresses.FirstOrDefault(a => a.AddressId == id));
            }

            return Results.Ok(db.Addresses.ToList());
        }

        //"/address/update/{id}"
        public static IResult Update(int id, AdventureWorksLt2019Context db, Address address)
        {
            // search for address
            Address editingAddress = db.Addresses.Find(id);

            if (editingAddress == null)
            {
                Address newAddress = address;
                newAddress.Rowguid = Guid.NewGuid();

                db.Addresses.Add(newAddress);
                db.SaveChanges();

                return Results.Ok(newAddress);
            }
            else
            {
                editingAddress = address;
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



   

