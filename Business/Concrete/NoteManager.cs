using Business.Abstract;
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

        public IResult Add(Note note)
        {
            note.CreatedDate = DateTime.Now;
            _noteDal.Add(note);

            return new SuccessResult("Not Eklendi.");
        }

        public IDataResult<List<Note>> GetNoteByCoordinateId(int coordinateId)
        {
            return new SuccessDataResult<List<Note>>(_noteDal.GetAll(c=> c.CoordinateId==coordinateId), "Talep tarihçesi açıldı.");
        }
    }
}
