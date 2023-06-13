using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entites.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCriminalDal : EfEntityRepositoryBase<Criminal, Context>, ICriminalDal
    {
        public List<Criminal> GetByCategoryAndDate(DateTime dateTimeStart, DateTime dateTimeFinish, int[] categoryIds)
        {
            using (var context = new Context())
            {
                var result = context.Criminals
                    .Where(x => x.CallTime >= dateTimeStart && x.CallTime <= dateTimeFinish && categoryIds.Contains(x.CategoryId))
                    .ToList();

                return result;
            }
        }

        List<Criminal> ICriminalDal.GetByDate(DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            using (var context = new Context())
            {
                var result = context.Criminals
                    .Where(x => x.CallTime >= dateTimeStart && x.CallTime <= dateTimeFinish)
                    .ToList();

                return result;
            }
        }
        public List<CriminalDetailDto> GetDetails(Expression<Func<Criminal, bool>> filter = null)
        {
            using (var context = new Context())
            {
                var result = from x in filter == null ? context.Criminals : context.Criminals.Where(filter)
                             join y in context.Categories
                             on x.CategoryId equals y.Id
                             select new CriminalDetailDto
                             {
                                 //Id = x.Id,
                                 //Title = x.Title,
                                 //LocationX = x.LocationX,
                                 //LocationY = x.LocationY,
                                 //Address = x.Address,
                                 //Active = x.Active,
                                 //Description = x.Description,
                                 //Contact = x.Contact,
                                 //Town = x.Town,
                                 //CategoryId = x.CategoryId,
                                 //CreatedDate = x.CreatedDate,
                                 //UpdatedDate = x.UpdatedDate,
                                 //Status = x.Status,
                                 //CategoryName = y.CategoryName,
                                 //SetupDate = x.SetupDate,
                                 //SetupDescription = x.SetupDescription,
                             };
                return result.ToList();
            }
        }

       
    }
}
