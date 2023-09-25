using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext context;

        public HomeController(ToDoContext _context) => context = _context;

        // Ana sayfayı oluşturur ve görevleri filtreler.
        public IActionResult Index(string id)
        {
            // Filtreleri ayrıştırır.
            var filters = new Filters(id);
            ViewBag.Filters = filters;

            // Kategorileri ve durumları görünüme ekler.
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuss.ToList();
            ViewBag.DueFilters = Filters.DueFiltresValues;

            // Veritabanındaki görevleri alır ve filtrelerine göre sıralar.
            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status);

            if (filters.HasCategory)
            {
                query = query.Where(t => t.CategoryId == filters.CategoryId);
            }

            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }

            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueDate < today);
                }
                else if (filters.IsFuture)
                {
                    query = query.Where(t => t.DueDate > today);
                }
                else if (filters.IsToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }
            }

            // Görevleri sıralar ve görünüme aktarır..
            var tasks = query.OrderBy(t => t.DueDate).ToList();
            return View(tasks);
        }

        // Yeni görev eklemek için formu oluşturur..
        [HttpGet]
        public IActionResult Add()
        {
            // Kategorileri, durumları ve varsayılan bir görevi görünüme ekler.
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuss.ToList();
            var task = new ToDo { StatusId = "Open" };
            return View(task);
        }

        // Yeni bir görevi kaydeder.
        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                // Görevi veritabanına ekler ve ana sayfaya yönlendirir.
                context.ToDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                // Hatalı girişleri düzelten ve formu tekrar gösteren hatalı durumda görünüme döner.
                ViewBag.Categories = context.Categories.ToList();
                ViewBag.Statuses = context.Statuss.ToList();
                return View(task);
            }
        }

        // Filtreleme seçeneklerini işler ve ana sayfaya yönlendirir.
        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        //  Görevi tamamlandı olarak işaretler.
        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            selected = context.ToDos.Find(selected.Id)!;

            if (selected != null)
            {
                // Görevin durumunu "Tamamlandı" olarak günceller.
                selected.StatusId = "Closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }

        // Tamamlanan görevleri siler.
        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            // Tamamlanan görevleri veritabanından kaldırır.
            var toDelete = context.ToDos.Where(t => t.StatusId == "Closed").ToList();
            foreach (var task in toDelete)
            {
                context.ToDos.Remove(task);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
    }
}
