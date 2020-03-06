using CommonLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class AuthenticationContext:IdentityDbContext
    {
       
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ApplicationModel> UserDataTable { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public DbSet<NoteModel> Note { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public DbSet<LabelModel> Label { get; set; }

        /// <summary>
        /// Gets or sets the note label.
        /// </summary>
        /// <value>
        /// The note label.
        /// </value>
        public DbSet<NoteLabelModel> NoteLabel { get; set; }
    }
}
