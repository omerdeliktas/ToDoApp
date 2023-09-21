using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Controllers;

public class HomeController : Controller
{
    private ToDoContext context;
    private int today;
    private string? tasks;

    public HomeController(ToDoContext _context) => context = _context;

    



    public IActionResult Index(string id)
    {
        var filters = new Filters(id);
        ViewBag.Filters = filters;  
        ViewBag.Categories = context.Categories.ToList();    
        ViewBag.Statuses=context.Statuss.ToList();
        ViewBag.DueFilters = Filters.DueFiltresValues;

        IQueryable<ToDo> query = context.ToDos
            .Include(t => t.Category)
            .Include(t => t.Status);

        if(filters.HasCategory)
        {
            query = query.Where(t =>t. CategoryId == filters.CategoryId);

        }

        if (filters.HasStatus)
        {
            query = query.Where(t => t.StatusId == filters.StatusId);

        }

        if (filters.HasDue)
        {
            var today = DateTime.Today;
            if(filters.IsPast)
            {

                query = query.Where(t => t.DueDate < today);
            }

            else if (filters.IsFuture)
            {
                query = query.Where(t => t.DueDate > today);
            }
            else if (filters.IsToday)
            {
                query =query.Where(t => t.DueDate == today); 
            }


        }
        var tasks = query.OrderBy(t => t.DueDate).ToList();

        return View(tasks);
    }

    [HttpGet]

    public IActionResult Add()
    {
        ViewBag.Categories = context.Categories.ToList();
        ViewBag.Statuses = context.Statuss.ToList();
        var task = new ToDo { StatusId = "Open" };
        return View(task);  

    }
    [HttpPost]
    public IActionResult Add(ToDo task)
    {
        if(ModelState.IsValid)
        {
            context.ToDos.Add(task);
            context.SaveChanges();
            return RedirectToAction("Index");   
        }
        else
        {
            ViewBag.Categories= context.Categories.ToList();    
            ViewBag.Statuss= context.Statuss.ToList();
            return View(task);
        }
    }

    [HttpPost]
     public IActionResult Filter(string[] filter)
    {
        string id = string.Join('-', filter);
        return RedirectToAction("Index", new {ID = id});
    }
    [HttpPost]  
    public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
    {
        selected = context.ToDos.Find(selected.Id)!;

        if(selected  != null)
        {
            selected.StatusId = "Close";
            context.SaveChanges();
        }
        return RedirectToAction("Index", new { ID = id });

    }
    [HttpPost]
    public IActionResult DeleteComplete(string id)
    {
        var toDelete = context.ToDos.Where(t => t.StatusId == "Close").ToList();
        
        foreach(var task in toDelete)
        {
            context.ToDos.Remove(task); 
        }  
        context.SaveChanges();

        return RedirectToAction("Index", new {ID = id});
    }
}