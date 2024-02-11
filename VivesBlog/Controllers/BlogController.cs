using Microsoft.AspNetCore.Mvc;
using System;
using VivesBlog.Models;
using VivesBlog.Services;

namespace VivesBlog.Cyb.Ui.Mvc.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
        }

        [HttpGet(nameof(Index))]
        public IActionResult Index()
        {
            var articles = _blogService.Find();
            return View(articles);
        }

        [HttpGet(nameof(Create))]
        public IActionResult Create()
        {
            var articleModel = _blogService.CreateArticleModel();
            return View(articleModel);
        }

        [HttpPost(nameof(Create))]
        public IActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = _blogService.CreateArticleModel(article);
                return View(articleModel);
            }

            article.CreatedDate = DateTime.Now;
            _blogService.Create(article);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var article = _blogService.Get(id);
            var articleModel = _blogService.CreateArticleModel(article);
            return View(articleModel);
        }

        [HttpPost(nameof(Edit) + "/{id}")]
        public IActionResult Edit(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = _blogService.CreateArticleModel(article);
                return View(articleModel);
            }

            _blogService.Edit(article.Id, article);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet(nameof(Delete) + "/{id}")]
        public IActionResult Delete(int id)
        {
            var article = _blogService.GetDelete(id);
            return View(article);
        }

        [HttpPost(nameof(Delete) + "/{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            _blogService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
