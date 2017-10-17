using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private IItemRepository itemRepo;

        public ItemsController(IItemRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.itemRepo = new EFItemRepository;
            }
            else
            {
                this.itemRepo = thisRepo;
            }
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(itemRepo.Items.Include(items => items.Category).ToList());
        }
        public IActionResult Details(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(itemRepo.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            itemRepo.Save(item);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            ViewBag.CategoryId = new SelectList(itemRepo.Categories, "CategoryId", "Name");
            return View(thisItem);
        }
        [HttpPost]
        public IActionResult Edit(Item item)
        {
            itemRepo.Edit(item);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            itemRepo.Remove(thisItem);
            return RedirectToAction("Index");
        }
        public IActionResult Done(int id)
        {
			var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
			return View(thisItem);
        }
        [HttpPost]
		public IActionResult Done(Item item)
		{
            itemRepo.Edit(item);
			return RedirectToAction("Index");
		}
    }
}