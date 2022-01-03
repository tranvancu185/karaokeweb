using Karaoke_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karaoke_project.Services
{
    public class FoodsService
    {
        private readonly web_karaokeContext _context;
        public FoodsService(web_karaokeContext context)
        {
            _context = context;
        }

        public List<Food> getListFood()
        {
            List<Food> foodList = new List<Food>();
            foodList = _context.Foods.AsNoTracking().Include(f => f.IdCategoryNavigation).OrderByDescending(x => x.IdCategory).ToList();
            return foodList;
        }

        public List<Food> getListFoodByCat(int catId = 0)
        {
            List<Food> foodList = new List<Food>();
            foodList = _context.Foods.AsNoTracking().Where(x => x.IdCategory == catId).Include(f => f.IdCategoryNavigation).OrderByDescending(x => x.Id).ToList();
            return foodList;
        }

        public Food getListFoodById(int id)
        {
            Food foodList = _context.Foods.Include(f => f.IdCategoryNavigation).FirstOrDefault(m => m.Id == id);
            return foodList;
        }

        
    }
}
