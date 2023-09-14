    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;

    namespace ToDo.Models;

    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Lütfen bir anahtar kelime giriniz")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lütfen tarih  giriniz")]

        public DateTime? DueDate {  get; set; }
    
        [Required(ErrorMessage ="Lütfen bir Kategori Seçiniz")]
        public string CategoryId { get; set; }=string.Empty;

        [ValidateNever]
        public Category Category { get; set; } = null!;
    
        [Required(ErrorMessage ="Lütfen bir Durum seçiniz ")] 
        public string StatusId {  get; set; } = string.Empty;
        [ValidateNever]

        public Status Status { get; set; } = null!;

        public bool Overdue => StatusId == "open" && DueDate < DateTime.Today; 
           
    }
