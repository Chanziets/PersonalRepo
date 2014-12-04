using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;

namespace CTPWebApi.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private CtpWebContext db = new CtpWebContext();

        private int nextCategoryId = 1;
        private DateTime defaultdate = DateTime.Now;

        private IQueryable<CategoryDto> MapCategories()
        {
            return from c in db.Category
                select new CategoryDto() { CategoryId = c.CategoryId, CategoryName = c.CategoryName, DateAdded = c.DateAdded};
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return MapCategories().AsEnumerable().OrderByDescending(c => c.CategoryName);
        }

        public IEnumerable<CategoryDto> GetPagingListCategories(int page, int itemsPerPage)
        {
            return MapCategories().AsEnumerable().OrderBy(c => c.CategoryName).Skip((page * itemsPerPage)).Take(itemsPerPage);
        }

        public Category Get(Guid categoryId)
        {
            Category category = db.Category.FirstOrDefault(c => c.CategoryId == categoryId);
            return category;
        }

        public CategoryDto Get(string categoryName)
        {
            CategoryDto category = (from c in MapCategories() where c.CategoryName == categoryName select c).FirstOrDefault();
            return category;
        }

        public CategoryDto Add(CategoryDto category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            CategoryDto categoryDto = Get(category.CategoryName);
            Category newCategory = new Category();

            if (categoryDto == null)
            {
                newCategory.CategoryId = Guid.NewGuid();
                newCategory.DateAdded = defaultdate;
                newCategory.CategoryName = category.CategoryName;
            }
            else
            {
                throw new Exception("Category already exists.");
            }

            db.Category.Add(newCategory);
            db.SaveChanges();
            category.CategoryId = newCategory.CategoryId;

            TrainingHistory trainingHistoryRecord = new TrainingHistory();
            trainingHistoryRecord.EntityDetails = "CategoryId" + category.CategoryId;
            trainingHistoryRecord.Action = "CategoryAdd";

            Receiver receiver = new Receiver();
            Command command = new LogTrainingHistoryCommand(receiver);
            Invoker invoker = new Invoker();

            invoker.SetCommand(command);
            invoker.ExecuteCommand(trainingHistoryRecord);

           
            return category;
        }

        public void Remove(Guid categoryId)
        {
            
            Category category = (from c in db.Category where c.CategoryId == categoryId select c).FirstOrDefault();

            List<Topic> categoryTopics = db.Topic.Where(t => t.CategoryId == categoryId).ToList();
            categoryTopics.ForEach(t => db.Topic.Remove(t));
            db.SaveChanges();
            db.Category.Remove(category);
            db.SaveChanges();

        }

        public bool Update(CategoryDto category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            CategoryDto categoryDto = Get(category.CategoryName);
            Category editedCategory = db.Category.Find(category.CategoryId);
            if (editedCategory != null)
            {
                if (categoryDto == null || categoryDto.CategoryName == editedCategory.CategoryName)
                {
                    editedCategory.CategoryName = category.CategoryName;
                }
                else
                {
                    throw new Exception("Topic with submitted new topic name already exists.");
                }

            }
            else
            {
                throw new Exception("Category not found.");
            }

            db.SaveChanges();
            return true;

        }
    }
}