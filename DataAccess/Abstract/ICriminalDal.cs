using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICriminalDal : IEntityRepository<Criminal>
    {
        List<CriminalDetailDto> GetDetails(Expression<Func<Criminal, bool>> filter = null);
        List<Criminal> GetByDate(DateTime dateTimeStart, DateTime dateTimeFinish);
        List<Criminal> GetByCategoryAndDate(DateTime dateTimeStart, DateTime dateTimeFinish, int[] categoryIds);

    }
}
