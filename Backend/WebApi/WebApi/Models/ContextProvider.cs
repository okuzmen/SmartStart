using System.Data.Entity;
using Breeze.WebApi;

namespace WebApi.Models
{
    public class ContextProvider : EFContextProvider<SmartStartDbContext>
    {
        static ContextProvider()
        {
            Database.SetInitializer(new SmartStartContextInitializer());
        }
    }
}