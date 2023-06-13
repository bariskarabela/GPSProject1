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
    public class CriminalManager : ICriminalService
    {
        private ICriminalDal _criminalDal;



        public CriminalManager(ICriminalDal criminalDal)
        {
            _criminalDal = criminalDal;
        }
        [SecuredOperation("iladmin")]
        public IDataResult<Criminal> GetById(int id)
        {
            return new SuccessDataResult<Criminal>(_criminalDal.Get(c => c.Id == id), CriminalConstants.criminalGettedById);
        }


        [SecuredOperation("iladmin")]
        public IResult Add(Criminal criminal)

        {
            criminal.CreatedDate = DateTime.Now;
            _criminalDal.Add(criminal);

            return new SuccessResult(CriminalConstants.criminalAdded);
        }

        [SecuredOperation("iladmin")]
        public IResult AddRange(List<Criminal> criminals)

        {
            _criminalDal.AddRangeItems(criminals);

            return new SuccessResult(CriminalConstants.criminalAdded);
        }



        [SecuredOperation("iladmin")]
        public IResult Delete(Criminal criminal)
        {
            _criminalDal.Delete(criminal);
            return new SuccessResult(CriminalConstants.criminalDeleted);
        }

        [SecuredOperation("iladmin")]
        public IResult Update(Criminal criminal)
        {
            criminal.UpdatedDate = DateTime.Now;
            _criminalDal.Update(criminal);
            return new SuccessResult(CriminalConstants.criminalUpdated);
        }

        [SecuredOperation("iladmin")]
        public IDataResult<List<CriminalDetailDto>> GetByTownName(string name)
        {
            return new SuccessDataResult<List<CriminalDetailDto>>(_criminalDal.GetDetails(c => c.Town == name), "İlçeye göre getirildi.");
        }

        [SecuredOperation("iladmin")]
        public IDataResult<List<Criminal>> GetByDate(DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            var criminals = _criminalDal.GetByDate(dateTimeStart, dateTimeFinish);

            if (criminals.Count > 0)
            {
                return new SuccessDataResult<List<Criminal>>(criminals, "Tarihe göre getirildi.");
            }
            else
            {
                return new ErrorDataResult<List<Criminal>>("Kayıt bulunamadı.");
            }
        }

        [SecuredOperation("iladmin")]
        public IDataResult<List<Criminal>> GetByCategoryAndDate(DateTime dateTimeStart, DateTime dateTimeFinish, int[] categoryIds)
        {
            var criminals = _criminalDal.GetByCategoryAndDate(dateTimeStart, dateTimeFinish, categoryIds);

            if (criminals.Count > 0)
            {
                return new SuccessDataResult<List<Criminal>>(criminals, "Tarihe ve kategoriye göre getirildi.");
            }
            else
            {
                return new ErrorDataResult<List<Criminal>>("Kayıt bulunamadı.");
            }
        }




        [SecuredOperation("iladmin")]
        public IDataResult<List<Criminal>> GetAll()
        {
            return new SuccessDataResult<List<Criminal>>(_criminalDal.GetAll());
        }

        public IDataResult<List<CriminalDetailDto>> GetDetails()
        {
            return new SuccessDataResult<List<CriminalDetailDto>>(_criminalDal.GetDetails());
        }
        [SecuredOperation("iladmin")]
        public IDataResult<List<CriminalPaginationResult>> GetAllPages(int page, int pageSize, string param = null)
        {
            var query = _criminalDal.GetAll();

            if (param != null)
            {
                query = query.Where(p => p.Description.Contains(param)
                    || p.Country.Contains(param)
                    || p.Town.Contains(param)
                    || p.District.Contains(param)
                    || p.Street.Contains(param)
                    || p.AddressDescription.Contains(param)
                    || p.LocationX.Contains(param)
                    || p.LocationY.Contains(param)).ToList();
             
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var perPage = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var paginationResult = new CriminalPaginationResult
            {
                PerPage = perPage,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            var resultList = new List<CriminalPaginationResult> { paginationResult };

            return new SuccessDataResult<List<CriminalPaginationResult>>(resultList);
        }
    }

}

