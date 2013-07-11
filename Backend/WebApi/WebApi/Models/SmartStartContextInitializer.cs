using System.Data.Entity;

namespace WebApi.Models
{
    public class SmartStartContextInitializer : DropCreateDatabaseAlways<SmartStartDbContext>
    {
        protected override void Seed(SmartStartDbContext context)
        {
            for (int i = 0; i < 11; i ++)
            {
                var newHole = new Hole
                    {
                        Description = "Hole #" + i,
                        Status = HoleStatus.New,
                        Location = new Location { Latitude = 48.464764 + i, Longitude = 35.046193 },
                        ImagePath = "img/hole.png"
                    };
                context.Holes.Add(newHole);
            }
            context.SaveChanges();
        }
         
    }
}