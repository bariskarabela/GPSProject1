using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("iladmin")]
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(CategoryConstants.CategoryAdded);
        }

        [SecuredOperation("iladmin")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(CategoryConstants.CategoryDeleted);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll() , CategoryConstants.AllCategoryGetted);
        }

        public IDataResult<List<Category>> GetByCategoryName(string name)
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(c => c.CategoryName == name), "Kategoriye göre getirildi.");
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.Id == id), CategoryConstants.CategoryGettedById);
        }

        [SecuredOperation("iladmin")]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(CategoryConstants.CategoryUpdated);
        }
    }
}
