using System.Linq;
using EMuvekkil.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models.Concrete
{
    public class EfDocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EfDocumentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Document AddDocument(Document document)
        {
            document.IsActive = true;
            _dbContext.Documents.Add(document);
            _dbContext.SaveChanges();
            return document;
        }

        public void DeleteDocument(int id)
        {
            var document = _dbContext.Documents.SingleOrDefault(d => d.Id == id);
            if (document != null)
            {
                _dbContext.Documents.Remove(document);
                _dbContext.SaveChanges();
            }
        }

        public Document GetDocument(int id)
        {
            return _dbContext.Documents.Include(d => d.Dava).Include(d => d.Owner).SingleOrDefault(d => d.Id == id);
        }

        public IQueryable<Document> GetDocuments()
        {
            return _dbContext.Documents.Include(d => d.Dava).Include(d => d.Owner);
        }

        public void ChangeDocumentStatus(Document document)
        {
            var doc = _dbContext.Documents.SingleOrDefault(d => d.Id == document.Id);
            if (doc != null)
            {
                doc.IsActive = !document.IsActive;
                _dbContext.SaveChanges();
            }

        }
    }
}