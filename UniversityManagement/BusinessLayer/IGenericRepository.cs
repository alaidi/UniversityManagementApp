using System.Collections.Generic;

namespace UniversityManagement.BusinessLayer
{
    public interface IGenericRepository<T>
    {
        List<T> GetData();
        int Add(T entity);
        int Update(T entity);
        int Delete(int id);
    }
}