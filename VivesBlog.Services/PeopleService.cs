using VivesBlog.Core;
using VivesBlog.Models;

namespace VivesBlog.Services
{
    public class PeopleService(VivesBlogDbContext dbContext)
    {
        public IList<Person> Find()
        {
            var people = dbContext.People.ToList();
            return people;
        }

        public Person? Get(int id)
        {
            return dbContext.People.SingleOrDefault(p => p.Id == id);
        }

        public Person? Create(Person person)
        {
            dbContext.People.Add(person);
            dbContext.SaveChanges();

            return person;
        }

        public Person? Edit(int id, Person person)
        {
            var dbPerson = Get(id);

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;

            dbContext.SaveChanges();

            return person;
        }

        public Person? GetSingle(int id)
        {
            var person = dbContext.People.Single(p => p.Id == id);
            return person;
        }

        public void Delete(int id)
        {
            var dbPerson = GetSingle(id);
            dbContext.People.Remove(dbPerson);
            dbContext.SaveChanges();
        }
    }
}
