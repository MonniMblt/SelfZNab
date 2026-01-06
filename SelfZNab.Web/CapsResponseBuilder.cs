using System.Linq.Expressions;

namespace SelfZNab.Web;

public class CapsResponseBuilder
    : ICapsResponseBuilder.IStart,
        ICapsResponseBuilder.ICategory,
        ICapsResponseBuilder.IEnd
{
    private string _url;
    private int _limitsMax;
    private List<string> _featureName = new List<string>();
    private Searching _searching = new Searching();
    private List<Category> _categories = new List<Category>();

    private CapsResponseBuilder() { }

    public ICapsResponseBuilder.IEnd SetUrl(string url)
    {
        _url = url;
        return this;
    }

    public ICapsResponseBuilder.IEnd SetLimitsMax(int limitsMax)
    {
        _limitsMax = limitsMax;
        return this;
    }

    public ICapsResponseBuilder.IEnd AddFeature(string name)
    {
        _featureName.Add(name);
        return this;
    }

    public ICapsResponseBuilder.IEnd SetSearching(Action<Searching> configure)
    {
        configure(_searching);
        return this;
    }

    public ICapsResponseBuilder.IEnd AddSearchType(
        Expression<Func<Searching, SearchType>> selector,
        string value
    )
    {
        var name = ((MemberExpression)selector.Body).Member.Name;
        var type = typeof(Searching);
        type.GetProperty(name)
            .SetValue(_searching, new SearchType() { Available = "yes", SupportedParams = value });

        return this;
    }

    public ICapsResponseBuilder.ICategory AddCategory(int id, string name)
    {
        _categories.Add(new Category() { Id = id, Name = name });
        return this;
    }

    public ICapsResponseBuilder.ICategory AddSubCategory(int id, string name)
    {
        _categories.Last().SubCategories.Add(new SubCategory() { Id = id, Name = name });
        return this;
    }

    public static ICapsResponseBuilder.IStart Create() => new CapsResponseBuilder();

    public CapsResponse Build() =>
        new CapsResponse()
        {
            Server = new ServerInfo
            {
                Title = "SelZNab",
                Version = "1.0",
                Url = _url,
            },
            Limits = new Limits { Max = _limitsMax, Default = _limitsMax / 2 },
            Registration = new Registration { Available = "no", Open = "no" },
            Features = _featureName
                .Select(f => new Feature { Name = f, Available = "yes" })
                .ToList(),
            Searching = _searching,
            Categories = _categories,
        };
}
