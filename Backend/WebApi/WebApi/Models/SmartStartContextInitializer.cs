using System.Data.Entity;
using System.Data.Spatial;

namespace WebApi.Models
{
    public class SmartStartContextInitializer : DropCreateDatabaseAlways<SmartStartDbContext>
    {
        protected override void Seed(SmartStartDbContext context)
        {
            for (int i = 0; i < 10; i ++)
            {
                var newHole = new Hole
                    {
                        Description = "Hole #" + i,
                        Status = HoleStatus.New,
                        Location = DbGeography.FromText("POINT(-122.336106 47.605049)"),
                        Image = "Content\\Image" + i
                    };
                context.Holes.Add(newHole);
            }
            context.SaveChanges();
        }
         
    }
}