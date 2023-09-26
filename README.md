# ToDoApp


Bu ASP.NET Core MVC projesi, bir görev takip uygulamasıdır

Category ve Status Sınıfları: Kategori ve durum verilerini temsil eder.

ToDo Sınıfı: Görevlerin temsilini yapar. Görevin açıklaması, son tarihi, kategorisi, durumu gibi özellikleri içerir.

Filters Sınıfı: Filtreleme seçeneklerini işlemek için kullanılır. Kategori, tarih ve durum filtreleri bulunur.

ToDoContext Sınıfı: Entity Framework kullanarak veritabanı işlemlerini gerçekleştirir. Görevler, kategoriler ve durumlar için DbSet'ler içerir.

HomeController Sınıfı: Ana kontrolör olarak görev yapar. Görevleri filtreler, ekler, işler ve siler. View'lere veri aktarımı için ViewBag kullanılır.

Views/Home/Add.cshtml ve Views/Home/Index.cshtml: HTML formları ve görevlerin listelendiği görünümleri içerir. Yeni görev ekleme ve filtreleme işlemleri bu görünümlerde yapılır.
