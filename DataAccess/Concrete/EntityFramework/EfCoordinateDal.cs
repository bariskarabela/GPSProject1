using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCoordinateDal : EfEntityRepositoryBase<Coordinate, Context>, ICoordinateDal
    {
        public List<Coordinate> GetAllS(Expression<Func<Coordinate, bool>> filter = null)
        {
            using (var context = new Context())
            {
                return filter == null ? context.Set<Coordinate>().Include(p => p.Category).ToList() : context.Set<Coordinate>().Where(filter)
                    .Include(p => p.Category).ToList();
            }
        }
    }
}
