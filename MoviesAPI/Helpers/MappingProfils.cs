
namespace MoviesAPI.Helpers
{
    public class MappingProfils:Profile
    {
        public MappingProfils()
        {
            // select genre
            CreateMap<Genre,genreDto> ();
            CreateMap<CreateAndEditGenreDto, Genre>();

            //select movie
            CreateMap<Movie,DetailsMovieDto> ();
            CreateMap<DetailsMovieDto, Movie> ();
            CreateMap<AddMoviesDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());

            CreateMap<UpdateMoviesDto, Movie>()
                .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}
