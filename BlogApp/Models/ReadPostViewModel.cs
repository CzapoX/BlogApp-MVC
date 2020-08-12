using DataAccessLibrary.Models;
using System.ComponentModel;

namespace BlogApp.Models
{
    public class ReadPostViewModel
    {
        [DisplayName("Tytuł")]
        public string Title { get; set; }

        [DisplayName("Zawartość wpisu")]
        public string Content { get; set; }

        [DisplayName("Autor wpisu")]
        public BlogAppUser Author { get; set; }
    }
}
