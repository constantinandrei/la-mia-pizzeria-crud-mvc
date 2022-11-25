﻿using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbCategoryRepository
    {
        private PizzaDbContext db;

        public DbCategoryRepository()
        {
            db = new PizzaDbContext();
        }

        public List<Category> Get()
        {
            return db.Categories.ToList();
        }
    }
}