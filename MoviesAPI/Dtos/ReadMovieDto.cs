using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Dtos
{
    public class ReadMovieDto
    {
        public string Title { get; set; }
        public string Gender { get; set; }
        public int Duration { get; set; }

        public DateTime ConsultMoment { get; set; } = DateTime.Now;
    }
}
