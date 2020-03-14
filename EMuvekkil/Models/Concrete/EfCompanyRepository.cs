using System.Linq;
using EMuvekkil.Models.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EMuvekkil.Models.Concrete
{

    public class EfCompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EfCompanyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Company AddCompany(Company company)
        {
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();
            return company;
        }

        public void DeleteCompany(int id)
        {
            var dava = _dbContext.Companies.FirstOrDefault(c => c.Id == id);
            if (dava != null)
            {
                _dbContext.Companies.Remove(dava);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Company> GetCompanies()
        {
            return _dbContext.Companies.Include(c=>c.Muvekkils);
        }

        public Company GetCompany(int id)
        {
            return _dbContext.Companies.FirstOrDefault(s=>s.Id==id);
        }
    }
}