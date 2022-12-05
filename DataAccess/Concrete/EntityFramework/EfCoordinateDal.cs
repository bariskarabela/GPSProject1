using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entites.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCoordinateDal : EfEntityRepositoryBase<Coordinate, Context>, ICoordinateDal
    {

        public CoordinateDetailDto GetDetail(Expression<Func<Coordinate, bool>> filter)
        {
            using (var context = new Context())
            {
                var result = from x in context.Coordinates.Where(filter)
                             join y in context.Categories
                             on x.Id equals y.Id
                             select new CoordinateDetailDto
                             {
                                 Id = x.Id,
                                 Title = x.Title,
                                 LocationX = x.LocationX,
                                 LocationY = x.LocationY,
                                 Address = x.Address,
                                 Active = x.Active,
                                 Description = x.Description,
                                 Contact = x.Contact,
                                 Town = x.Town,
                                 CategoryId = x.CategoryId,
                                 CreatedDate = x.CreatedDate,
                                 UpdatedDate = x.UpdatedDate,
                                 Status = x.Status,
                                 CategoryName = y.CategoryName
                             };
                return result.SingleOrDefault();
            }
        }

        public List<CoordinateDetailDto> GetDetails(Expression<Func<Coordinate, bool>> filter = null)
        {
            using (var context = new Context())
            {
                var result = from x in filter == null ? context.Coordinates : context.Coordinates.Where(filter)
                             join y in context.Categories
                             on x.CategoryId equals y.Id
                             select new CoordinateDetailDto
                             {
                                 Id = x.Id,
                                 Title = x.Title,
                                 LocationX = x.LocationX,
                                 LocationY = x.LocationY,
                                 Address = x.Address,
                                 Active = x.Active,
                                 Description = x.Description,
                                 Contact = x.Contact,
                                 Town = x.Town,
                                 CategoryId = x.CategoryId,
                                 CreatedDate = x.CreatedDate,
                                 UpdatedDate = x.UpdatedDate,
                                 Status = x.Status,
                                 CategoryName = y.CategoryName
                             };
                return result.ToList();
            }
        }
    }
}
