using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICoordinateService
    {
        IDataResult<List<Coordinate>> GetAll();
        IDataResult<List<CoordinateDetailDto>> GetDetails();
        IDataResult<Coordinate> GetById(int id);
        IResult Add(Coordinate coordinate);
        IResult Delete(Coordinate coordinate);
        IResult Update(Coordinate coordinate);
        IDataResult<List<CoordinateDetailDto>> GetByTownName(string name);
        IDataResult<ChartDetailDto> GetChartByTownName(string townName);
        IDataResult<ChartDetailDto> GetChartByStatusName();

    }
}
