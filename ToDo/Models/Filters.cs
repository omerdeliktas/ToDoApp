namespace ToDoApp.Models;

public class Filters
{
    public Filters(string filterstring)
    {
        FilterString = filterstring ?? "all - all - all";
        string[] filters = GetFilterString().Split('-');
        CategoryId = filters[0];
        Due = filters[1];
        StatusId = filters[2];
    }

    private readonly string filterString;

    public string GetFilterString()
    {
        return FilterString;
    }

    public string CategoryId { get; }
    public string Due { get; }

    public string StatusId { get; }

    public bool HasCategory => CategoryId.ToLower() != "all";
    public bool HasDue => Due.ToLower() != "all";
    public bool HasStatus => StatusId.ToLower() != "all";

    public static Dictionary<string, string> DueFiltresValues =>
        new Dictionary<string, string> {
        { "future", "Planlanan"},
        {"past", "Geçmiş" },
        {"today","Bugün" }

    };

    public bool IsPast => Due.ToLower() == "past";
    public bool IsFuture => Due.ToLower() == "future";
    public bool IsToday => Due.ToLower() == "today";

    public string FilterString { get; }
}
