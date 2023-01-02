using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICoordinateDal : IEntityRepository<Coordinate>
    {
        List<CoordinateDetailDto> GetDetails(Expression<Func<Coordinate, bool>> filter = null);
        List<CoordinateDetailDto> GetChartByStatusName(Expression<Func<Coordinate, bool>> filter = null);

        List<CoordinateDetailDto> GetChartByTownName(Expression<Func<Coordinate, bool>> filter = null);
    }
}
