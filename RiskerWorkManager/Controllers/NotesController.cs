using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.PermissionValidators;
using RiskerWorkManager.Services;
using WorkManagerDal.Models;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NotesController : ControllerBase, IDisposable
    {
        private readonly INotesService _notesService;
        private readonly IProjectsService _projectsService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IFilesService _filesService;

        public NotesController(INotesService notesService, 
            IUserIdentityService userIdentityService, 
            IProjectsService projectsService,
            IFilesService filesService)
        {
            _notesService = notesService;
            _userIdentityService = userIdentityService;
            _projectsService = projectsService;
            _filesService = filesService;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<Note> CreateOrUpdateNote(Note note, long projectId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var editedProject = await _projectsService.GetProjectByIdAsync(projectId);

            if (!editedProject.ValidateUserViewPermission(user))
            {
                return null;
            }
            await _notesService.CreateOrUpdateNoteAsync(note, user.Id, editedProject.Id);
            return note;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<List<Note>> GetNotesByProject(long projectId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var project = await _projectsService.GetProjectByIdAsync(projectId);

            if (!project.ValidateUserViewPermission(user))
            {
                return null;
            }

            var notes = await _notesService.GetNotesByProjectAsync(projectId);
            return notes;
        }

        [HttpPost]
        [AuthorizePermission]
        public async Task<bool> DeleteNote(long noteId)
        {
            var user = _userIdentityService.GetCurrentUser(HttpContext);
            var note = await _notesService.FindAsync(noteId);
            var project = await _projectsService.GetProjectByIdAsync(note.Project.Id);

            if (!project.ValidateUserViewPermission(user))
            {
                return false;
            }
            _filesService.DeleteAllFilesFromNote(note.Id);
            await _notesService.DeleteAsync(note);
            
            return true;
        }
        public void Dispose()
        {
            _notesService.Dispose();
            _projectsService.Dispose();
        }
    }
}
