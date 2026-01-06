using System.Linq.Expressions;

namespace SelfZNab.Web;

public interface ICapsResponseBuilder
{
    public interface IShared
    {
        ICategory AddCategory(int id, string name);
        IEnd SetUrl(string url);
        IEnd AddFeature(string name);
        IEnd AddSearchType(Expression<Func<Searching, SearchType>> selector, string value);
        IEnd SetLimitsMax(int limitsMax);
        IEnd SetSearching(Action<Searching> configure);
    }

    public interface IStart : IShared { }

    public interface ICategory : IShared
    {
        ICategory AddSubCategory(int id, string name);
        CapsResponse Build();
    }

    public interface IEnd : IShared
    {
        CapsResponse Build();
    }
}
