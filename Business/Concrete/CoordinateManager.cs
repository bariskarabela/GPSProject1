using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.NLog;
using Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Entites.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NLog.Filters;
using Twilio.TwiML.Messaging;

namespace Business.Concrete
{
    public class CoordinateManager : ICoordinateService
    {
        private ICoordinateDal _coordinateDal;



        public CoordinateManager(ICoordinateDal coordinateDal)
        {
            _coordinateDal = coordinateDal;
        }
        //[CacheAspect]
        public IDataResult<Coordinate> GetById(int id)
        {
            return new SuccessDataResult<Coordinate>(_coordinateDal.Get(c => c.Id == id), CoordinateConstants.CoordinateGettedById);
        }


        [SecuredOperation("iladmin,ilceadmin")]
        public IResult Add(Coordinate coordinate)

        {
            coordinate.CreatedDate = DateTime.Now;
            _coordinateDal.Add(coordinate);

            return new SuccessResult(CoordinateConstants.CoordinateAdded);
        }

        [SecuredOperation("iladmin,ilceadmin")]
        public IResult Delete(Coordinate coordinate)
        {
            _coordinateDal.Delete(coordinate);
            return new SuccessResult(CoordinateConstants.CoordinateDeleted);
        }

        [SecuredOperation("iladmin,ilceadmin")]
        public IResult Update(Coordinate coordinate)
        {
            coordinate.UpdatedDate = DateTime.Now;
            _coordinateDal.Update(coordinate);
            return new SuccessResult(CoordinateConstants.CoordinateUpdated);
        }

        public IDataResult<List<CoordinateDetailDto>> GetByTownName(string name)
        {
            return new SuccessDataResult<List<CoordinateDetailDto>>(_coordinateDal.GetDetails(c => c.Town == name), "İlçeye göre getirildi.");
        }

        public IDataResult<List<Coordinate>> GetAll()
        {
            return new SuccessDataResult<List<Coordinate>>(_coordinateDal.GetAll());
        }

        public IDataResult<List<CoordinateDetailDto>> GetDetails()
        {
            return new SuccessDataResult<List<CoordinateDetailDto>>(_coordinateDal.GetDetails());
        }

        public IDataResult<ChartDetailDto> GetChartByTownName(string townName)
        {
            var result1 = _coordinateDal.GetAll(c => c.Status == "Fiber Hattı Kurulum Aşamasında" && c.Town == townName).Count().ToString();
            var result2 = _coordinateDal.GetAll(c => c.Status == "Ruhsatlandırma Aşamasında" && c.Town == townName).Count().ToString();
            var result3 = _coordinateDal.GetAll(c => c.Status == "Onaylandı" && c.Town == townName).Count().ToString();
            var result4 = _coordinateDal.GetAll(c => c.Status == "Enerji Hattı Kurulum Aşamasında" && c.Town == townName).Count().ToString();
            var result5 = _coordinateDal.GetAll(c => c.Status == "Fiziksel Kurulum Aşamasında" && c.Town == townName).Count().ToString();
            var result6 = _coordinateDal.GetAll(c => c.Status == "Kurulum Tamamlandı" && c.Town == townName).Count().ToString();

            var result = new ChartDetailDto
            {
                Fiber = result1,
                Ruhsatlandırma = result2,
                Onay = result3,
                Enerji = result4,
                Fiziksel = result5,
                Kurulumu = result6
            };

            return new SuccessDataResult<ChartDetailDto>(result, "İstatistikler getirildi.");
        }


        public IDataResult<ChartDetailDto>GetChartByStatusName()
        {
            var result1 = _coordinateDal.GetAll(c => c.Status == "Fiber Hattı Kurulum Aşamasında").Count().ToString();
            var result2 = _coordinateDal.GetAll(c => c.Status == "Ruhsatlandırma Aşamasında").Count().ToString();
            var result3 = _coordinateDal.GetAll(c => c.Status == "Onaylandı").Count().ToString();
            var result4 = _coordinateDal.GetAll(c => c.Status == "Enerji Hattı Kurulum Aşamasında").Count().ToString();
            var result5 = _coordinateDal.GetAll(c => c.Status == "Fiziksel Kurulum Aşamasında").Count().ToString();
            var result6 = _coordinateDal.GetAll(c => c.Status == "Kurulum Tamamlandı").Count().ToString();
            

            var result =new ChartDetailDto { 
                Fiber=result1,
                Ruhsatlandırma=result2,
                Onay=result3,
                Enerji=result4,
                Fiziksel=result5,
                Kurulumu=result6
            } ;

            return new SuccessDataResult<ChartDetailDto>(result,"İstatistikler getirildi.");
        }
    }
}
