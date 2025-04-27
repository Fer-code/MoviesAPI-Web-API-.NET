using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Dtos
{
    public class UpdateMovieDto
    {
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Gender field is required")]
        [StringLength(50, ErrorMessage = "Gender field length cant exceed 50 characters")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Duration field is required")]
        [Range(70, 600, ErrorMessage = "Duration must be between 70 to 600 minutes")]
        public int Duration { get; set; }
    }
}
