using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entites.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs;
using Twilio.TwiML.Messaging;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }


        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }


        public User GetById(int id)
        {
            return _userDal.Get(u => u.Id == id);
        }


        public IResult Update(User user)
        {
              _userDal.Update(user);
            return new SuccessResult("Kullanıcı güncellendi");
         
        }


        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult("Kullanıcı silindi");
        }


        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), "Bütün Kullanıcılar listelendi.");
        }

        public IResult UpdateUser(UserForUpdateDto userForUpdateDto)
        {
            var result = _userDal.Get(u => u.Id == userForUpdateDto.Id);
            result.FirstName = userForUpdateDto.FirstName;
            result.LastName = userForUpdateDto.LastName;
            result.Email = userForUpdateDto.Email;
            result.Phone = userForUpdateDto.Phone;
            result.Rank = userForUpdateDto.Rank;
            result.Town = userForUpdateDto.Town;
            _userDal.Update(result);
            return new SuccessResult("Kullanıcı Güncellendi.");
        }
    }
}

