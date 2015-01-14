using System;
using System.Collections.Generic;
using System.Data.Entity;
using DynamicWebAPI.Models;

namespace DynamicWebAPI
{
    //Available Stategies: 
    //DropCreateDatabaseIfModelChanges
    //DropCreateDatabaseIfNotExists
    //DropCreateDatabaseAlways 
    public class DynamicContextInitializer : DropCreateDatabaseAlways<DynamicDbContext>
    {
        protected override void Seed(DynamicDbContext context)
        {
            //Content for class
            var categories = new List<Category>()
            {
                new Category() {CategoryName = "Web Components", DateAdded = DateTime.Now, CategoryId = Guid.NewGuid()},
                new Category() {CategoryName = "Utilities", DateAdded = DateTime.Now, CategoryId = Guid.NewGuid()},
                new Category() {CategoryName = "Database Structures", DateAdded = DateTime.Now, CategoryId = Guid.NewGuid()}
            };

            categories.ForEach(c => context.Category.Add(c));
            context.SaveChanges();
        }

    }
}