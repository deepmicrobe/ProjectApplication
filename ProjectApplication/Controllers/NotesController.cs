using Microsoft.AspNetCore.Mvc;
using ProjectApplication.Models;
using ProjectApplication.Services;

namespace ProjectApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController
    {
        private INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpPost]
        public void NewNote(CreateNoteRequest model)
        {
            if (model == null)
            {
                return;
            }

            _notesService.CreateNote(model);
        }

        [HttpGet]
        public List<GetNotesResponse> GetNotes(int? projectId, List<int>? attributeIds)
        {
            return _notesService.GetNotes(projectId, attributeIds);
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<Note> GetNotes()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}


    }
}
