﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public User? UserCreated { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ProjectsToUsers>? UsersHasAccess { get; set; } = new List<ProjectsToUsers> { };
        public List<Note>? Notes { get; set; } = new List<Note> { };
        public List<Version>? Versions { get; set; } = new List<Version> { };
        public List<Bug>? Bugs { get; set; } = new List<Bug>();
        public List<Feature>? Features { get; set; } = new List<Feature>();
        public bool? IsDeleted { get; set; }
        //public List<UploadedFile> Files { get; set; } = new List<UploadedFile>();
    }
}
