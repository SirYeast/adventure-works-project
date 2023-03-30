using adventure_works_project.Models;

namespace adventure_works_project.Methods;

public static class AddressMethods
{
    //"/address/create"
    public static IResult Create(Address address, AdventureWorksLt2019Context db)
    {
        address.Rowguid = Guid.NewGuid();
        address.ModifiedDate = DateTime.Today;

        db.Addresses.Add(address);
        db.SaveChanges();
        return Results.Created($"/address/created/{address.AddressId}", address);
    }

    //"/address/{id?}"
    public static IResult GetAddresses(int? id, AdventureWorksLt2019Context db)
    {
        if (id != null)
        {
            return Results.Ok(db.Addresses.Find(id));
        }

        return Results.Ok(db.Addresses.ToList());
    }

    //"/address/update/{id}"
    public static IResult Update(int? id, Address address, AdventureWorksLt2019Context db)
    {
        if (id == null)
        {
            return Results.NotFound();
        }

        // search for address
        Address? editingAddress = db.Addresses.Find(id);

        if (editingAddress == null)
        {
            address.Rowguid = Guid.NewGuid();
            address.ModifiedDate = DateTime.Today;

            db.Addresses.Add(address);
            db.SaveChanges();

            return Results.Created($"/address/created/{address.AddressId}", new
            {
                message = "Address did not exist, new Address created.",
                Address = address
            });
        }

        //maintain Guid 
        Guid maintainGuid = editingAddress.Rowguid;

        editingAddress = address;

        editingAddress.AddressId = address.AddressId;
        editingAddress.Rowguid = maintainGuid;
        editingAddress.ModifiedDate = DateTime.Today;

        db.SaveChanges();
        return Results.Ok(editingAddress);
    }

    //"/address/delete/{id}"
    public static IResult Delete(int? id, AdventureWorksLt2019Context db)
    {
        if (id == null)
        {
            return Results.NotFound();
        }

        Address? addressToDelete = db.Addresses.Find(id);

        if (addressToDelete != null)
        {
            db.Remove(addressToDelete);
            db.SaveChanges();

            return Results.Ok(addressToDelete);
        }

        return Results.BadRequest();
    }

    public static IResult Details(int? id, AdventureWorksLt2019Context db)
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
                Customers = ad.CustomerAddresses.Select(ca => ca.Customer)
            }).FirstOrDefault(ad => ad.AddressId == id);

        return address != null ? Results.Ok(address) : Results.NotFound();
    }
}