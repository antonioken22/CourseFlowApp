using CourseFlow.Models;
using System.Collections.Generic;

namespace CourseFlow.Repositories
{
    public interface IYearLevelRepository
    {
        void Add(YearLevelModel yearLevelModel);
        void Edit(YearLevelModel yearLevelModel);
        void Remove(int id);
        YearLevelModel GetById(int id);
        IEnumerable<YearLevelModel> GetAll();
    }
}
