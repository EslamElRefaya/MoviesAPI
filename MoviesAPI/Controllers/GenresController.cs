namespace MoviesAPI.Controllers
{
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
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genresService.GetAll();
            var data = _mapper.Map<IEnumerable<genreDto>>(genres);
            return Ok(data);
        }

        [HttpGet("{GenreId}")]
        public async Task<IActionResult> GetGenreById(int GenreId)
        {
            var genres = await _genresService.GetById(GenreId);
            var data=_mapper.Map<genreDto>(genres);    
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(CreateAndEditGenreDto dto)
        {
            //var genre = new Genre()
            //{
            //    Name = dto.Name
            //};
            var genre = _mapper.Map<Genre>(dto);
            await _genresService.AddGenre(genre);

            //this part to show items
            var data = _mapper.Map<genreDto>(genre);
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditGenre(int id, CreateAndEditGenreDto dto)
        {
            var genre = await _genresService.GetById(id);
            if (genre == null)
                return NotFound($"No Genre is been Founded to this id: {id}");
            if (dto == null)
                return BadRequest("Genre Is Requred!!");

            //genre.Name = dto.Name;
            //equal above //this to update
            _mapper.Map(dto,genre);
            await _genresService.UpdateGenre(genre);
            var data = _mapper.Map<genreDto>(genre);
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
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
