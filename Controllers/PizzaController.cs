using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View(context.Pizze.ToList());
        }

        public IActionResult Detail(int id)
        {
            PizzeriaContext context = new PizzeriaContext();
            Pizza pizza = context.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (pizza == null)
            {
                return NotFound("Neesun prodotto con questo id");
            }
            else
            {
                return View(pizza);
            }
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }

            PizzeriaContext context = new PizzeriaContext();
            context.Pizze.Add(pizza);
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
                return View(pizza);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View(pizza);
            }
            PizzeriaContext ctx = new PizzeriaContext();
            Pizza editPizza = (from p in ctx.Pizze where p.Id == id select p).FirstOrDefault();
            if (editPizza == null)
            {
                return NotFound();
            }

            editPizza.Name = pizza.Name;
            editPizza.Description = pizza.Description;
            editPizza.Img = pizza.Img;
            editPizza.Price = pizza.Price;
            ctx.SaveChanges();
            return RedirectToAction("Index");
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

