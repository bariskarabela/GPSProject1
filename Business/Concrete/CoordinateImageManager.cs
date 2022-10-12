using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CoordinateImageManager:ICoordinateImageService
    {
        private ICoordinateImageDal _coordinateImageDal;

        public CoordinateImageManager(ICoordinateImageDal coordinateImageDal)
        {
            _coordinateImageDal = coordinateImageDal;
        }
        //[TransactionScopeAspect]
        //[ValidationAspect(typeof(CoordinateImageValidator))]
     
        public IResult Add( IFormFile file, CoordinateImage coordinateImage)
        {
            
          
            var result= BusinessRules.Run(CheckCoordinateImagesLimit(coordinateImage.CoordinateId));

           if (result!=null)
           {
               return new ErrorResult(result.Message);
           }
           coordinateImage.ImagePath = FileHelper.Upload(file, PathConstants.ImagesPath);
            _coordinateImageDal.Add(coordinateImage);


            return new SuccessResult(CoordinateImageConstants.CoordinateImagesAdded);
        }
        //[ValidationAspect(typeof(CoordinateImageValidator))]
        //[SecuredOperation("admin,moderator")]
        public IResult Update(IFormFile file, CoordinateImage coordinateImage)
        {
            FileHelper.Update(file, PathConstants.ImagesPath + coordinateImage.ImagePath, PathConstants.ImagesPath);
            _coordinateImageDal.Update(coordinateImage);
            return new SuccessResult(CoordinateImageConstants.CoordinateImagesUpdated);
        }
        //[SecuredOperation("admin,moderator")]
        public IResult Delete(CoordinateImage coordinateImage)
        
        {
            FileHelper.Delete(PathConstants.ImagesPath + coordinateImage.ImagePath);
            _coordinateImageDal.Delete(coordinateImage);
            return new SuccessResult(CoordinateImageConstants.CoordinateImagesDeleted);
        }

        public IDataResult<List<CoordinateImage>> GetAll()
        {
         
            return new SuccessDataResult<List<CoordinateImage>>(_coordinateImageDal.GetAll(),CoordinateImageConstants.AllCoordinateImagesGetted);
        }

        public IDataResult<CoordinateImage> GetById(int id)
        {
            return new SuccessDataResult<CoordinateImage>(_coordinateImageDal.Get(c => c.Id == id),CoordinateImageConstants.CoordinateImagesGettedById);
        }

        public IDataResult<List<CoordinateImage>> GetByCoordinateId(int coordinateId)
        {
            var result = BusinessRules.Run(CheckCoordinateIsHaveImage(coordinateId));
            if (result!=null)
            {
                return new SuccessDataResult<List<CoordinateImage>>(GetDefaultImage(), CoordinateImageConstants.CoordinateImagesGettedByCoordinateId);
            }
            return new SuccessDataResult<List<CoordinateImage>>(_coordinateImageDal.GetAll(c => c.CoordinateId == coordinateId));
        }

        #region BusinessRules

        private List<CoordinateImage> GetDefaultImage()
        {
            List<CoordinateImage> coordinateImage = new List<CoordinateImage>();
            coordinateImage.Add(new CoordinateImage { ImagePath = "http://localhost:46858/Uploads/Images/DefaultImage.jpg" });
            return coordinateImage;
        }

        private IResult CheckCoordinateIsHaveImage(int coordinateId)
        {
            if (_coordinateImageDal.GetAll(c => c.CoordinateId == coordinateId).Count == 0)
            {
                return new ErrorResult(CoordinateImageConstants.CoordinateImagesNotFound);
            }

            return new SuccessResult();
        }
        private IResult CheckCoordinateImagesLimit(int coordinateId)
        {
            var result = _coordinateImageDal.GetAll(c => c.CoordinateId == coordinateId).Count;
            if (result > 5)
            {
                return new ErrorResult(CoordinateImageConstants.CoordinateImagesLimitExceded);
            }

            return new SuccessResult();
        }

        #endregion


    }
}
