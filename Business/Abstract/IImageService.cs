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
   public interface IImageService
   {
       IResult Add(IFormFile file,Image criminalImage);
       IResult Update(IFormFile file, Image criminalImage);
       IResult Delete(Image criminalImage);
       IDataResult<List<Image>> GetAll();
       IDataResult<Image> GetById(int id);
       IDataResult<List<Image>> GetBycriminalId(int criminalId);
   }
}
