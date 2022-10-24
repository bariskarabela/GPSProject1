﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entites.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [SecuredOperation("iladmin")]
        public void Add(User user)
        {
            _userDal.Add(user);
        }

        [SecuredOperation("iladmin")]
        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        [SecuredOperation("iladmin")]
        public User GetById(int id)
        {
            return _userDal.Get(u => u.Id == id);
        }

        [SecuredOperation("iladmin")]
        public IResult Update(User user)
        {
              _userDal.Update(user);
            return new SuccessResult("kullanıcı güncellendi");
         
        }

        [SecuredOperation("iladmin")]
        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult("kullanıcı silindi");
        }

        [SecuredOperation("iladmin")]
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }
    }
}
