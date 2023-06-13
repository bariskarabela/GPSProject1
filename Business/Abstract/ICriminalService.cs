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
    public interface ICriminalService
    {
        IDataResult<List<Criminal>> GetAll();
        IDataResult<List<CriminalDetailDto>> GetDetails();
        IDataResult<Criminal> GetById(int id);
        IResult Add(Criminal criminal);
        IResult AddRange(List<Criminal> criminals);
        IDataResult<List<CriminalPaginationResult>> GetAllPages(int page, int pageSize, string param = null);
        IResult Delete(Criminal criminal);
        IResult Update(Criminal criminal);
        IDataResult<List<CriminalDetailDto>> GetByTownName(string name);
        IDataResult<List<Criminal>> GetByDate(DateTime dateTimeStart, DateTime dateTimeFinish);
        IDataResult<List<Criminal>> GetByCategoryAndDate(DateTime dateTimeStart, DateTime dateTimeFinish, int[] categoryIds);

     


    }
}
