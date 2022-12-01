using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Entites.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }
        [SecuredOperation("iladmin")]
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            var userToCheck = _userOperationClaimDal.GetAll(c => c.UserId == userOperationClaim.UserId).Count();
            if (userToCheck > 0)
            {
                return new SuccessResult("Kullanıcının zaten mevcut rolü bulunmakta.");
            }
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult("Rol Eklendi");
        }

        public IDataResult<UserOperationClaim> GetByUserId(int id)
        {
            return new SuccessDataResult<UserOperationClaim> (_userOperationClaimDal.Get(c => c.UserId == id), "UserId'ye göre getirildi.");
            
        }
 
        [SecuredOperation("iladmin")]
        public IResult Update(UpdateClaimDto updateClaimDto)
        {
            var result = _userOperationClaimDal.Get(u => u.Id == updateClaimDto.UserId);
            result.UserId = updateClaimDto.UserId;
            result.OperationClaimId = updateClaimDto.OperationClaimId;
            _userOperationClaimDal.Update(result);
            return new SuccessResult("Kulanıcı Güncellendi");
        }
    }
}
