namespace ToDoApp.Models
{
    public class Filters
    {
        // Filtre dizesini alarak bir Filters nesnesi oluşturur.
        public Filters(string filterstring)
        {
            // filterstring null ise veya boşsa, varsayılan bir filtre dizesi atanır.
            FilterString = filterstring ?? "all - all - all";

            // Filtre dizesini '-' karakterine göre böler ve kategori tarih ve durumu alır.
            string[] filters = GetFilterString().Split('-');
            CategoryId = filters[0];
            Due = filters[1];
            StatusId = filters[2];
        }

        // Filtre dizesini döndüren metot.
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

        // Tarih filtreleri için bir sözlük. Anahtarlar ve değerler ile tarih filtrelerini eşler.
        public static Dictionary<string, string> DueFiltresValues =>
            new Dictionary<string, string> {
                {"future", "Planlanan"},
                {"past", "Geçmiş" },
                {"today", "Bugün" }
            };

       
        public bool IsPast => Due.ToLower() == "geçmiş";

        
        public bool IsFuture => Due.ToLower() == "planlanan";

      
        public bool IsToday => Due.ToLower() == "bugün";

        // Filtre dizesi.
        public string FilterString { get; }
    }
}
