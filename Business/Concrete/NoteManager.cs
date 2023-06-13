using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NoteManager : INoteService
    {
        private INoteDal _noteDal;
        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        [SecuredOperation("iladmin")]
        public IResult Add(Note note)
        {
            note.CreatedDate = DateTime.Now;
            _noteDal.Add(note);

            return new SuccessResult("Not Eklendi.");
        }

        [SecuredOperation("iladmin")]
        public IDataResult<List<Note>> GetNoteBycriminalId(int criminalId)
        {
            return new SuccessDataResult<List<Note>>(_noteDal.GetAll(c=> c.CriminalId==criminalId), "Suç tarihçesi açıldı.");
        }
    }
}
