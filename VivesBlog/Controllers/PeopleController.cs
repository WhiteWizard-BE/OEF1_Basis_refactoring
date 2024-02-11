using Microsoft.AspNetCore.Mvc;
using VivesBlog.Models;
using VivesBlog.Services;

namespace VivesBlog.Cyb.Ui.Mvc.Controllers
{
    [Route("People")]
    public class PeopleController : Controller
    {
        private readonly PeopleService _peopleService;

        public PeopleController(PeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet(nameof(Index))]
        public IActionResult Index()
        {
            var people = _peopleService.Find();
            return View(people);
        }

        [HttpGet(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost(nameof(Create))]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _peopleService.Create(person);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var person = _peopleService.Get(id);
            return View(person);
        }

        [HttpPost(nameof(Edit) + "/{id}")]
        public IActionResult Edit(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            _peopleService.Edit(person.Id, person);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet(nameof(Delete) + "/{id}")]
        public IActionResult Delete(int id)
        {
            var person = _peopleService.GetSingle(id);
            return View(person);
        }

        [HttpPost(nameof(Delete) + "/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            _peopleService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
