using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IExcelCriminalService
    {
        IDataResult<List<ExcelCriminal>> GetAll();
        IDataResult<List<ExcelCriminalPaginationResult>> GetAllPages(int page,int pageSize, string param = null);
        IDataResult<ExcelCriminal> GetById(int id);
        IResult Add(ExcelCriminal excelCriminal);
        IResult Delete(ExcelCriminal excelCriminal);
        IResult Update(ExcelCriminal excelCriminal);
        IResult Add(IFormFile file);
       
        IResult MoveData(int id); 



    }
}
