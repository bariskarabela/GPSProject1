﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICoordinateDal : IEntityRepository<Coordinate>
    {
        List<Coordinate> GetAllS(Expression<Func<Coordinate, bool>> filter = null);

    }
}
