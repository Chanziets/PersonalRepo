using System;
using System.Collections.Generic;
using DynamicWebAPI.Models;

namespace DynamicWebAPI.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<object> GetAllCategories();
        object Get(Guid categoryId);
        CategoryDto Add(CategoryDto item);
        void Remove(Guid categoryId);
        bool Update(CategoryDto item);
    }
}
