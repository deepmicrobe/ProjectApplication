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
    }
}
