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
    public class ImageManager:IImageService
    {
        private IImageDal _criminalImageDal;

        public ImageManager(IImageDal criminalImageDal)
        {
            _criminalImageDal = criminalImageDal;
        }
        //[TransactionScopeAspect]
        //[ValidationAspect(typeof(criminalImageValidator))]
        [SecuredOperation("iladmin")]
        public IResult Add( IFormFile file, Image criminalImage)
        {
            
          
            var result= BusinessRules.Run(CheckcriminalImagesLimit(criminalImage.CriminalId));

           if (result!=null)
           {
               return new ErrorResult(result.Message);
           }
           criminalImage.ImagePath = FileHelper.Upload(file, PathConstants.ImagesPath);
            _criminalImageDal.Add(criminalImage);


            return new SuccessResult(ImageConstants.criminalImagesAdded);
        }
        //[ValidationAspect(typeof(criminalImageValidator))]
         [SecuredOperation("iladmin")]
        public IResult Update(IFormFile file, Image criminalImage)
        {
            FileHelper.Update(file, PathConstants.ImagesPath + criminalImage.ImagePath, PathConstants.ImagesPath);
            _criminalImageDal.Update(criminalImage);
            return new SuccessResult(ImageConstants.criminalImagesUpdated);
        }
        [SecuredOperation("iladmin")]
        public IResult Delete(Image criminalImage)
        
        {
            FileHelper.Delete(PathConstants.ImagesPath + criminalImage.ImagePath);
            _criminalImageDal.Delete(criminalImage);
            return new SuccessResult(ImageConstants.criminalImagesDeleted);
        }
        [SecuredOperation("iladmin")]
        public IDataResult<List<Image>> GetAll()
        {
         
            return new SuccessDataResult<List<Image>>(_criminalImageDal.GetAll(),ImageConstants.AllcriminalImagesGetted);
        }
        [SecuredOperation("iladmin")]
        public IDataResult<Image> GetById(int id)
        {
            return new SuccessDataResult<Image>(_criminalImageDal.Get(c => c.Id == id),ImageConstants.criminalImagesGettedById);
        }
        [SecuredOperation("iladmin")]
        public IDataResult<List<Image>> GetBycriminalId(int criminalId)
        {
            var result = BusinessRules.Run(CheckcriminalIsHaveImage(criminalId));
            if (result!=null)
            {
                return new SuccessDataResult<List<Image>>(GetDefaultImage(), ImageConstants.criminalImagesGettedBycriminalId);
            }
            return new SuccessDataResult<List<Image>>(_criminalImageDal.GetAll(c => c.CriminalId == criminalId));
        }

        #region BusinessRules

        private List<Image> GetDefaultImage()
        {
            List<Image> criminalImage = new List<Image>();
            criminalImage.Add(new Image { ImagePath = "http://localhost:46858/Uploads/Images/DefaultImage.jpg" });
            return criminalImage;
        }

        private IResult CheckcriminalIsHaveImage(int criminalId)
        {
            if (_criminalImageDal.GetAll(c => c.CriminalId == criminalId).Count == 0)
            {
                return new ErrorResult(ImageConstants.criminalImagesNotFound);
            }

            return new SuccessResult();
        }
        private IResult CheckcriminalImagesLimit(int criminalId)
        {
            var result = _criminalImageDal.GetAll(c => c.CriminalId == criminalId).Count;
            if (result > 5)
            {
                return new ErrorResult(ImageConstants.criminalImagesLimitExceded);
            }

            return new SuccessResult();
        }

        #endregion


    }
}
