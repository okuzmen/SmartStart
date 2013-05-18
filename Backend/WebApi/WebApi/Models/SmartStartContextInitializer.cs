using System.Data.Entity;

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
                        Location = "Some location #" + i,
                        Image = "Content\\Image" + i
                    };
                context.Holes.Add(newHole);
            }
            context.SaveChanges();
        }
         
    }
}