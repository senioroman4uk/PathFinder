using System;
using System.Linq;
using PathFinder.Security.DAL.Entities;

namespace PathFinder.Security.WebApi.Queries
{
    public interface IUserQuery
    {
        IOrderedQueryable<AppUser> Users { get; } 
    }
}
