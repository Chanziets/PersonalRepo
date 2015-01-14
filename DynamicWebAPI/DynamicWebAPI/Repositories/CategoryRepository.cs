using System;
using System.Collections.Generic;
using System.Linq;
using DynamicWebAPI.Models;

namespace DynamicWebAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DynamicDbContext dynamicDbContext = new DynamicDbContext();
        private DateTime defaultDate = DateTime.Now;

        public IEnumerable<object> GetAllCategories()
        {
            return dynamicDbContext.Category.AsEnumerable().OrderByDescending(c => c.CategoryName);
        }

        public object Get(Guid categoryId)
        {
            object category = dynamicDbContext.Category.FirstOrDefault(c => c.CategoryId == categoryId);
            return category;
        }
 
        public CategoryDto Add(CategoryDto category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                DateAdded = defaultDate,
                CategoryName = category.CategoryName
            };

            dynamicDbContext.Category.Add(newCategory);
            dynamicDbContext.SaveChanges();
            category.CategoryId = newCategory.CategoryId;

            return category;
        }

        public void Remove(Guid categoryId)
        {

            var category = (from c in dynamicDbContext.Category where c.CategoryId == categoryId select c).FirstOrDefault();
            dynamicDbContext.SaveChanges();
            dynamicDbContext.Category.Remove(category);
            dynamicDbContext.SaveChanges();

        }

        public bool Update(CategoryDto category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            Category editedCategory = dynamicDbContext.Category.Find(category.CategoryId);
            if (editedCategory != null)
            {
                editedCategory.CategoryName = category.CategoryName;
            }
            else
            {
                throw new Exception("Category not found.");
            }

            dynamicDbContext.SaveChanges();
            return true;

        }

    }
}