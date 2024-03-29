﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagerDal.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<User> Users { get; set; } = new();
        public List<Permission> Permissions { get; set; } = new();
    }
}
