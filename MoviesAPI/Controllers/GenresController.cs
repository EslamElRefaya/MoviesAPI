namespace MoviesAPI.Controllers
{
    [Authorize(Roles="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;
        public GenresController(IGenresService genresService, IMapper mapper)
        {
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            var genres = await _genresService.GetAll();
            var data = _mapper.Map<IEnumerable<genreDto>>(genres);
            return Ok(data);
        }

        [HttpGet("{GenreId}")]
        public async Task<IActionResult> GetGenreByIdAsync(int GenreId)
        {
            var genres = await _genresService.GetById(GenreId);
            var data=_mapper.Map<genreDto>(genres);    
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenreAsync(CreateAndEditGenreDto createAndEditGenreDto)
        {
            //var genre = new Genre()
            //{
            //    Name = dto.Name
            //};
            var genre = _mapper.Map<Genre>(createAndEditGenreDto);
            await _genresService.AddGenre(genre);

            //this part to show items
            var data = _mapper.Map<genreDto>(genre);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditGenreAsync(int id, CreateAndEditGenreDto createAndEditGenreDto)
        {
            var genre = await _genresService.GetById(id);
            if (genre == null)
                return NotFound($"No Genre is been Founded to this id: {id}");
            if (createAndEditGenreDto == null)
                return BadRequest("Genre Is Requred!!");

            //genre.Name = dto.Name;
            //equal above //this to update
            _mapper.Map(createAndEditGenreDto, genre);
            await _genresService.UpdateGenre(genre);
            var data = _mapper.Map<genreDto>(genre);
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreAsync(int id)
        {
            var genre = await _genresService.GetById(id);
             if (genre is null)
                return NotFound($"No Genre Founded to this id:{id}");
            await _genresService.DeleteGenre(genre);
            var data = _mapper.Map<genreDto>(genre);
            return Ok(data);
        }
    }
}
