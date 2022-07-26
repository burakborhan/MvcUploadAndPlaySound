using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcSesYüklemeGüncel.Entity.EntityModel;

namespace MvcSesYüklemeGüncel.Entity.EntityDatabase
{
    public class SesContext : DbContext
    {
        public DbSet<AudioFile> AudioFiles { get; set; }

    }
}