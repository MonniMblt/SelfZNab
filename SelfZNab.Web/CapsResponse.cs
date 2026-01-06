using System.Xml.Serialization;

[XmlRoot("caps")]
public class CapsResponse
{
    [XmlElement("server")]
    public ServerInfo Server { get; set; }

    [XmlElement("limits")]
    public Limits Limits { get; set; }

    [XmlElement("registration")]
    public Registration Registration { get; set; }

    [XmlArray("features")]
    [XmlArrayItem("feature")]
    public List<Feature> Features { get; set; } = new();

    [XmlElement("searching")]
    public Searching Searching { get; set; }

    [XmlArray("categories")]
    [XmlArrayItem("category")]
    public List<Category> Categories { get; set; } = new();
}

public class ServerInfo
{
    [XmlAttribute("title")]
    public string Title { get; set; }

    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("url")]
    public string Url { get; set; }
}

public class Limits
{
    [XmlAttribute("max")]
    public int Max { get; set; }

    [XmlAttribute("default")]
    public int Default { get; set; }
}

public class Registration
{
    [XmlAttribute("available")]
    public string Available { get; set; }

    [XmlAttribute("open")]
    public string Open { get; set; }
}

public class Feature
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("available")]
    public string Available { get; set; }
}

public class Searching
{
    [XmlElement("search")]
    public SearchType Search { get; set; }

    [XmlElement("tv-search")]
    public SearchType TvSearch { get; set; }

    [XmlElement("movie-search")]
    public SearchType MovieSearch { get; set; }

    [XmlElement("anime-search")]
    public SearchType AnimeSearch { get; set; }
}

public class SearchType
{
    [XmlAttribute("available")]
    public string Available { get; set; }

    [XmlAttribute("supportedParams")]
    public string SupportedParams { get; set; }
}

public class Category : SubCategory
{
    [XmlElement("subcat")]
    public List<SubCategory> SubCategories { get; set; } = new();
}

public class SubCategory
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }
}
