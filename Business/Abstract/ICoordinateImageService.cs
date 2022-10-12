using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
   public interface ICoordinateImageService
   {
       IResult Add(IFormFile file,CoordinateImage coordinateImage);
       IResult Update(IFormFile file, CoordinateImage coordinateImage);
       IResult Delete(CoordinateImage coordinateImage);
       IDataResult<List<CoordinateImage>> GetAll();
       IDataResult<CoordinateImage> GetById(int id);
       IDataResult<List<CoordinateImage>> GetByCoordinateId(int coordinateId);
   }
}
