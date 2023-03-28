using adventure_works_project.Models;

namespace adventure_works_project.Models
{
    public static class AddressMethods
    {
        public static IResult Read(int? id, AdventureWorksLt2019Context db)
        {
            if (id != null)
            {
                return Results.Ok(db.Addresses.FirstOrDefault(a => a.AddressId == id));
            }

            return Results.Ok(db.Addresses.ToList());
        }
    }
}



   

