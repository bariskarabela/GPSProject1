using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(User user);
        User GetByMail(string email);
        User GetById(int id);
        IDataResult<List<User>> GetAll();
        IResult Update(User user);
        IResult Delete(User user);
        List<OperationClaim> GetClaims(User user);
    }
}
