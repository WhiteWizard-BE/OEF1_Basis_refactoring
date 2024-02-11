using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Core;
using VivesBlog.Models;

namespace VivesBlog.Services
{
    public class BlogService
    {
        private readonly VivesBlogDbContext _dbContext;

        public BlogService(VivesBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Article> Find()
        {
            return _dbContext.Articles
                .Include(a => a.Author)
                .ToList();
        }

        public Article? Get(int id)
        {
            return _dbContext.Articles.SingleOrDefault(p => p.Id == id);
        }

        public Article? Create(Article article)
        {
            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();

            return article;
        }

        public Article? Edit(int id, Article updatedArticle)
        {
            var dbArticle = Get(id);

            if (dbArticle != null)
            {
                dbArticle.Title = updatedArticle.Title;
                dbArticle.Description = updatedArticle.Description;
                dbArticle.Content = updatedArticle.Content;
                dbArticle.AuthorId = updatedArticle.AuthorId;

                _dbContext.SaveChanges();
            }

            return dbArticle;
        }

        public Article GetDelete(int id)
        {
            return _dbContext.Articles
                .Include(a => a.Author)
                .Single(p => p.Id == id);
        }

        public void Delete(int id)
        {
            var dbArticle = Get(id);

            if (dbArticle != null)
            {
                _dbContext.Articles.Remove(dbArticle);
                _dbContext.SaveChanges();
            }
        }

        public ArticleModel CreateArticleModel(Article article = null)
        {
            article ??= new Article();

            var authors = _dbContext.People
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            var articleModel = new ArticleModel
            {
                Article = article,
                Authors = authors
            };

            return articleModel;
        }
    }
}
