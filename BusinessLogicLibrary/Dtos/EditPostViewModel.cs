﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLibrary.Dtos
{
    public class EditPostViewModel
    {
        public string Id { get; set; }

        [DisplayName("Tytuł")]
        [MinLength(2, ErrorMessage = "Minimalna liczba znaków wynosi 2")]
        [MaxLength(16, ErrorMessage = "Maksymalna liczba znaków wynosi 16")]
        public string Title { get; set; }


        [DisplayName("Zawartość wpisu")]
        [MinLength(6, ErrorMessage = "Minimalna liczba znaków wynosi 6")]
        [MaxLength(200, ErrorMessage = "Post przekracza maksymalną liczbę znaków")]
        public string Content { get; set; }
    }
}
