using adventure_works_project.Models;

namespace adventure_works_project.Methods;

public static class CustomerMethods
{
    public static IResult Create(Customer customer, AdventureWorksLt2019Context db)
    {
        customer.Rowguid = new Guid();
        customer.ModifiedDate = DateTime.Today;

        db.Customers.Add(customer);
        db.SaveChanges();
        return Results.Created($"/customer/created/{customer.CustomerId}", customer);
    }

    public static IResult GetCustomers(int? id, AdventureWorksLt2019Context db)
    {
        if (id == null)
            return Results.Ok(db.Customers.ToList());

        return Results.Ok(db.Customers.Find(id));
    }

    public static IResult Details(int? id, AdventureWorksLt2019Context db)
    {
        if (id == null)
            return Results.BadRequest();

        var customer = db.Customers
            .Select(c => new
            {
                c.CustomerId,
                c.Title,
                c.FirstName,
                c.MiddleName,
                c.LastName,
                c.Suffix,
                c.CompanyName,
                c.SalesPerson,
                c.EmailAddress,
                c.Phone,
                Addresses = c.CustomerAddresses.Select(ca => new
                {
                    ca.AddressId,
                    ca.AddressType
                })
            })
            .FirstOrDefault(c => c.CustomerId == id);

        return customer != null ? Results.Ok(customer) : Results.NotFound();
    }

    public static IResult Update(int? id, Customer inCustomer, AdventureWorksLt2019Context db)
    {
        Customer? customer = db.Customers.Find(id);

        if (customer == null)
        {
            inCustomer.Rowguid = new Guid();
            inCustomer.ModifiedDate = DateTime.Today;

            db.Customers.Add(inCustomer);
            db.SaveChanges();
            return Results.Created($"/customer/created/{inCustomer.CustomerId}", new
            {
                Message = "Customer did not exist, successfully created new record.",
                Customer = inCustomer
            });
        }

        customer.NameStyle = inCustomer.NameStyle;
        customer.Title = inCustomer.Title;
        customer.FirstName = inCustomer.FirstName;
        customer.MiddleName = inCustomer.MiddleName;
        customer.LastName = inCustomer.LastName;
        customer.Suffix = inCustomer.Suffix;
        customer.CompanyName = inCustomer.CompanyName;
        customer.SalesPerson = inCustomer.SalesPerson;
        customer.EmailAddress = inCustomer.EmailAddress;
        customer.ModifiedDate = DateTime.Today;

        db.SaveChanges();
        return Results.Ok(customer);
    }

    public static IResult Delete(int? id, AdventureWorksLt2019Context db)
    {
        if (id == null)
            return Results.BadRequest();

        Customer? customer = db.Customers.Find(id);

        if (customer == null)
            return Results.NotFound();

        db.Customers.Remove(customer);
        db.SaveChanges();
        return Results.Ok(customer);
    }

    public static IResult AddToAddress(int? customerId, int? addressId, AdventureWorksLt2019Context db)
    {
        if (customerId == null || addressId == null)
            return Results.BadRequest();

        Customer? customer = db.Customers.Find(customerId);

        if (customer == null)
            return Results.NotFound();

        Address? address = db.Addresses.Find(addressId);

        if (address == null)
            return Results.NotFound();

        db.CustomerAddresses.Add(new()
        {
            CustomerId = customer.CustomerId,
            AddressId = address.AddressId,
            AddressType = "Main Office",
            Rowguid = new Guid(),
            ModifiedDate = DateTime.Today,
        });

        db.SaveChanges();
        return Results.Ok();
    }
}