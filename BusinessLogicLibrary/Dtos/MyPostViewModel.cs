using System.ComponentModel;

namespace BusinessLogicLibrary.Dtos
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
