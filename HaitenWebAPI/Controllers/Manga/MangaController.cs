using AutoMapper;
using HaitenWebAPI.DTOs.Chapter;
using HaitenWebAPI.DTOs.Manga;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository;

namespace HaitenWebAPI.Controllers.Manga
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMangaRepository _mangaRepository;  // Assume you have a MangaRepository to fetch manga data
        private readonly IMapper _mapper;
        public MangaController(IMangaRepository mangaRepository, IUserRepository userRepository,IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IQueryable<MangaListDTO>>> GetAll()
        {
            var ListManga = await _mangaRepository.GetAll();
            var ListMangaDto = ListManga.Select(manga => _mapper.Map<MangaListDTO>(manga));
            return Ok(ListMangaDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MangaDTO>> GetDetailAndChapterList(int id)
        {
            // Fetch the manga with chapters by id
            var manga = await _mangaRepository.GetById(id);  // Assuming GetById fetches the manga and its chapters

            if (manga == null)
            {
                return NotFound("Manga not found.");
            }

            // Map the manga details to MangaDTO using AutoMapper
            var mangaDto = _mapper.Map<MangaDTO>(manga);

            // Map the chapters to ChapterListDTO and assign it to the MangaDTO's Chapters list
            mangaDto.Chapters = manga.Chapters.Select(chapter => new ChapterListDTO
            {
                Id = chapter.Id,
                Name = chapter.Name,
                ViewCount = chapter.ViewCount
            }).ToList();

            // Return the mapped MangaDTO along with its chapters
            return Ok(mangaDto);
        }


    }
}
