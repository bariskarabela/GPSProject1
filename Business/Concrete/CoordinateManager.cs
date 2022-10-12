using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.Extensions.Logging;

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

 

        public IResult Add(Coordinate coordinate)

        {
            coordinate.CreatedDate = DateTime.Now;
            _coordinateDal.Add(coordinate);

            return new SuccessResult(CoordinateConstants.CoordinateAdded);
        }

        public IResult Delete(Coordinate coordinate)
        {
            _coordinateDal.Delete(coordinate);
            return new SuccessResult(CoordinateConstants.CoordinateDeleted);
        }

        public IResult Update(Coordinate coordinate)
        {
            coordinate.UpdatedDate = DateTime.Now;
            _coordinateDal.Update(coordinate);
            return new SuccessResult(CoordinateConstants.CoordinateUpdated);
        }



        public IDataResult<List<Coordinate>> GetAll()
        {
            return new SuccessDataResult<List<Coordinate>>(_coordinateDal.GetAll(), CoordinateConstants.AllCoordinateGetted);
        }
        public IDataResult<List<Coordinate>> GetByTownName(string name)
        {
            return new SuccessDataResult<List<Coordinate>>(_coordinateDal.GetAll(c=>c.Town==name), "İlçeye göre getirildi.");
        }

    }
}
