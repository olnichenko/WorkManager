using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public class NotesService : BaseService, INotesService
    {
        public NotesService(IWorkManagerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task CreateOrUpdateNoteAsync(Note note, long userId, long projectId)
        {
            if (note.Id == 0)
            {
                note.DateCreated = DateTime.Now;
                _unitOfWork.Notes.Create(note);
                var tUser = await _unitOfWork.Users.FindByConditionWithTracking(x => x.Id == userId).SingleAsync();
                var tProject = await _unitOfWork.Projects.FindByConditionWithTracking(x => x.Id == projectId).SingleAsync();
                tUser.Notes.Add(note);
                tProject.Notes.Add(note);
            }
            else
            {
                var editedNote = await _unitOfWork.Notes.FindAsync(note.Id);
                if (editedNote != null)
                {
                    editedNote.Title = note.Title;
                    editedNote.Content = note.Content;
                    _unitOfWork.Notes.Update(editedNote);
                }
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Note note)
        {
            _unitOfWork.Notes.Delete(note);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Note> FindAsync(long noteId)
        {
            var note = await _unitOfWork.Notes
                .FindByConditionWithTracking(x => x.Id == noteId)
                .Include(x => x.Project)
                .SingleOrDefaultAsync();
            return note;
        }

        public async Task<List<Note>> GetNotesByProjectAsync(long projectId)
        {
            var notes = await _unitOfWork.Notes
                .FindByCondition(x => x.Project.Id == projectId)
                .Include(x => x.UserCreated)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
            return notes;
        }
    }
}
