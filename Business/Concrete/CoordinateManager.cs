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


        //[SecuredOperation("iladmin,ilceadmin")]
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
            return new SuccessDataResult<List<CoordinateDetailDto>>(_coordinateDal.GetDetails(c=>c.Town==name), "İlçeye göre getirildi.");
        }

        public IDataResult<List<Coordinate>> GetAll()
        {
            return new SuccessDataResult<List<Coordinate>>(_coordinateDal.GetAll());
        }

        public IDataResult<List<CoordinateDetailDto>> GetDetails()
        {
            return new SuccessDataResult<List<CoordinateDetailDto>>(_coordinateDal.GetDetails());
        }

        public IDataResult<List<CoordinateDetailDto>> GetChartByStatusName(string statusName)
        {
            return new SuccessDataResult<List<CoordinateDetailDto>>(_coordinateDal.GetChartByStatusName(c => c.Status == ));
        }

        public IDataResult<List<CoordinateDetailDto>> GetChartByTownName(string townName)
        {          
            return new SuccessDataResult<List<CoordinateDetailDto>>(_coordinateDal.GetChartByTownName(c => c.Town == townName));
        }
    }
}
