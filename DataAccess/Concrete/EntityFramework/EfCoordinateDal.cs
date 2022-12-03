﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


        private IList<CoordinateDetailDto> CoordinateDetailList()
        {
            using (var context = new Context())
            {
                var result = from x in context.Coordinates
                             join y in context.Categories
                             on x.Id equals y.Id
                             select new CoordinateDetailDto
                             {
                                 Id = x.Id,
                                 ImagePath = x.ImagePath,
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
        public IList<CoordinateDetailDto> GetListCoordinateDetail()
        {
            var result = CoordinateDetailList();
            return result.ToList();
        }


    }
}
