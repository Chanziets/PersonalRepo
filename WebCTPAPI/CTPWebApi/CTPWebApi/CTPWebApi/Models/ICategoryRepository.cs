using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTPWebApi.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAllCategories();
        Category Get(Guid categoryId);
        IEnumerable<CategoryDto> GetPagingListCategories(int page, int itemsPerPage);
        CategoryDto Add(CategoryDto item);
        void Remove(Guid categoryId);
        bool Update(CategoryDto item);
    }
}