using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface IDocumentRepository
    {
        Document GetDocument(int id);
        void DeleteDocument(int id);
        Document AddDocument(Document document);
        IQueryable<Document> GetDocuments();

        void ChangeDocumentStatus(Document document);
        

    }
}