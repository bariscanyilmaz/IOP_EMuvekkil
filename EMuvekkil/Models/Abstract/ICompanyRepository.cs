using System.Linq;

namespace EMuvekkil.Models.Abstract
{
    public interface ICompanyRepository
    {
        void DeleteCompany(int id);
        IQueryable<Company> GetCompanies();

        Company AddCompany(Company company);

        Company GetCompany(int id);
    }
    
}