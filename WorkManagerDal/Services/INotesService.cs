using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkManagerDal.Models;

namespace WorkManagerDal.Services
{
    public interface INotesService : IBaseService
    {
        Task<Note> FindAsync(long noteId);
        Task DeleteAsync(Note note);
        Task<List<Note>> GetNotesByProjectAsync(long projectId);
        Task CreateOrUpdateNoteAsync(Note note, long userId, long projectId);
    }
}
