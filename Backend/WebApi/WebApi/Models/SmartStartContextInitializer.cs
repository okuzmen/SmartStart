using System.Data.Entity;
using System.Data.Spatial;

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
                        Location = DbGeography.FromText(string.Format("POINT({0} 35.046193)", 48.464764+i)),
                        Image = "img/hole.png"
                    };
                context.Holes.Add(newHole);
            }
            context.SaveChanges();
        }
         
    }
}