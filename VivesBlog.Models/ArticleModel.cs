namespace VivesBlog.Models
{
    public class ArticleModel
    {
        public Article Article { get; set; }
        public IList<Person> Authors { get; set; }
    }
}
