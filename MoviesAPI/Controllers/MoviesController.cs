namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private List<string> _allowedExtensions = new List<string> { ".png", ".jpg", "jpeg" };
        private long _maxAllowedPosterSize = 1048576;

        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;
        public MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _moviesService.GetAllMovies();

            var data=_mapper.Map<IEnumerable<DetailsMovieDto>>(movies);
            return Ok(data);
        }

        [HttpGet("{MovieId}")]
        public async Task<IActionResult> GetMovieByMovieIdAsync(int MovieId)
        {
            var movie = await _moviesService.GetMovieById(MovieId);
            if (movie == null) return NotFound("No Movie is been Founded !!");
            var data = _mapper.Map<DetailsMovieDto>(movie);
            return Ok(data);
        }

        [HttpGet("GetMoviesByGenreId/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenreIdAsync(int genreId)
        {
            var isVaild = await _genresService.IsVaildGenre(genreId);
            if (!isVaild)
                return NotFound("No Found it!!");
            var movies = await _moviesService.GetAllMovies();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieAsync([FromForm] AddMoviesDto addMoviesDto)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(addMoviesDto.Poster.FileName).ToLower()))
                return BadRequest("ony allowed .png ,.jpg ,.jpeg");

            if (addMoviesDto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allow size is 1MB");

            //to check on Genre Id vaildation
            var isVaildGenreId = await _genresService.IsVaildGenre(addMoviesDto.GenreId);
            if (!isVaildGenreId)
                return BadRequest($"this GenreId:{addMoviesDto.GenreId} is invaild");

            using var datastream = new MemoryStream();
            await addMoviesDto.Poster.CopyToAsync(datastream);
            /* var movie = new Movie
             {
                 GenreId = dto.GenreId,
                 Name = dto.Name,
                 Poster = datastream.ToArray(),
                 Rate = dto.Rate,
                 Year = dto.Year,
                 StoreLine = dto.StoreLine,
             };*/
            //this part add items using autoMaper
            var movie = _mapper.Map<Movie>(addMoviesDto);
            movie.Poster = datastream.ToArray(); //this part add Munaly without autoMapper 
            await _moviesService.AddMovie(movie);
            //this part select item using autoMaper
            var data = _mapper.Map<DetailsMovieDto>(movie);
            return Ok(data);
        }

        [HttpPut("{MovieId}")]
        public async Task<IActionResult> UpdateMoiveAsync(int MovieId, [FromForm] UpdateMoviesDto updateMoviesDto)
        {
            var movie = await _moviesService.GetMovieById(MovieId);

            if (movie == null)
                return NotFound($"No Movie is been founded to this id {MovieId}");

            if (updateMoviesDto == null)
                return BadRequest();

            //to check on Genre Id vaildation
            var isVaildGenreId = await _genresService.IsVaildGenre(updateMoviesDto.GenreId);
            if (!isVaildGenreId)
                return BadRequest($"this GenreId:{updateMoviesDto.GenreId} is invaild");
            //check if user add new Poster
            if (updateMoviesDto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(updateMoviesDto.Poster.FileName).ToLower()))
                    return BadRequest("ony allowed .png ,.jpg ,.jpeg");
                if (updateMoviesDto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allow size is 1MB");
                using var datastream = new MemoryStream();
                await updateMoviesDto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();
            }
            /*
            movie.Name = dto.Name;
            movie.Rate = dto.Rate;
            movie.Year = dto.Year;
            movie.StoreLine = dto.StoreLine;
            movie.GenreId = dto.GenreId;
            */
            _mapper.Map(updateMoviesDto, movie);
            await _moviesService.UpdateMovie(movie);
           var data = _mapper.Map<DetailsMovieDto>(movie);
            return Ok(data);
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> DeleteMovieAsync(int movieId)
        {
            var movie = await _moviesService.GetMovieById(movieId);
            if (movie == null) return NotFound("No Movie is been Founded!");
           await _moviesService.DeleteMovie(movie);

            var data=_mapper.Map<DetailsMovieDto>(movie);
            return Ok(data);
        }
    }
}
