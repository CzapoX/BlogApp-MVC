using DataAccessLibrary.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BlogApp.Models
{
    public partial class AddPostViewModel
    {

        [DisplayName("Tytuł")]
        [MinLength(2, ErrorMessage = "Minimalna liczba znaków wynosi 2")]
        [MaxLength(12, ErrorMessage = "Maksymalna liczba znaków wynosi 12")]
        public string Title { get; set; }


        [DisplayName("Zawartość wpisu")]
        [MinLength(6, ErrorMessage = "Minimalna liczba znaków wynosi 6")]
        [MaxLength(128, ErrorMessage = "Post przekracza maksymalną liczbę znaków")]
        public string Content { get; set; }
    }
}
