using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PizzeriaContext context = new PizzeriaContext();
            return View(context.Pizze.Include(p => p.Category).ToList());
        }

        public IActionResult Detail(int id)
        {
            PizzeriaContext context = new PizzeriaContext();
            Pizza pizza = context.Pizze.Where(pizza => pizza.Id == id).Include(p => p.Category).FirstOrDefault();
            if (pizza == null)
            {
                return NotFound("Nesun prodotto con questo id");
            }
            else
            {
                return View(pizza);
            }
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            using(PizzeriaContext ctx = new PizzeriaContext())
            {
                List<Category> categories = ctx.Categories.ToList();

                List<SelectListItem> ingredientsList = new List<SelectListItem>();
                List<Ingredient> ingredients = ctx.Ingredients.ToList();
                foreach (Ingredient i in ingredients)
                {
                    ingredientsList.Add( new SelectListItem() { Text = i.Name, Value = i.Id.ToString() });
                }   
                
                PizzaCategories model = new PizzaCategories();
                model.Categories = categories;
                model.Ingredients = ingredientsList;
                model.Pizza = new Pizza();
                return View(model);
            }
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategories pizzaData)
        {
            if (!ModelState.IsValid)
            {
                
                pizzaData.Categories = new PizzeriaContext().Categories.ToList();

                pizzaData.Ingredients = new List<SelectListItem>();
                List<Ingredient> ingredients = new PizzeriaContext().Ingredients.ToList();
                foreach (Ingredient i in ingredients)
                {
                    pizzaData.Ingredients.Add(new SelectListItem() { Text = i.Name, Value = i.Id.ToString() });
                }

                return View("Create", pizzaData);
            }

            PizzeriaContext context = new PizzeriaContext();
            context.Pizze.Add(pizzaData.Pizza);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet] 
        public IActionResult Update(int id)
        {
            PizzeriaContext ctx = new PizzeriaContext();
            Pizza pizza = (from p in ctx.Pizze where p.Id == id select p).FirstOrDefault();
            if (pizza == null)
            {
                return NotFound();
            }
            else
            {
                PizzaCategories model = new PizzaCategories();
                model.Pizza = pizza;
                model.Categories = ctx.Categories.ToList();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaCategories pizzaData)
        {
            using (PizzeriaContext ctx = new PizzeriaContext())
            {
                if (!ModelState.IsValid)
                {
                    pizzaData.Categories = ctx.Categories.ToList();
                    return View(pizzaData);
                }

                Pizza pizza = (from p in ctx.Pizze where p.Id == id select p).FirstOrDefault();
                if (pizza == null)
                {
                    return NotFound();
                }

                pizza.Name = pizzaData.Pizza.Name;
                pizza.Description = pizzaData.Pizza.Description;
                pizza.Img = pizzaData.Pizza.Img;
                pizza.Price = pizzaData.Pizza.Price;
                pizza.CategoryId = pizzaData.Pizza.CategoryId;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            PizzeriaContext ctx = new PizzeriaContext();
            Pizza pizza = (from p in ctx.Pizze where p.Id == id select p).FirstOrDefault();

            if (pizza == null)
            {
                return NotFound();
            }
           
            
            ctx.Pizze.Remove(pizza);
            ctx.SaveChanges();
            return RedirectToAction("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

