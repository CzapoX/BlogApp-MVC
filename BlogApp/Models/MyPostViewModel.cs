using System.ComponentModel;

namespace BlogApp.Models
{
    public class MyPostViewModel
    {
        public string Id { get; set; }

        [DisplayName("Tytuł")]
        public string Title { get; set; }

        [DisplayName("Zawartość wpisu")]
        public string Content { get; set; }
    }
}
