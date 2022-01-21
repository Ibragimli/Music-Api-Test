using Microsoft.EntityFrameworkCore;
using Music.Core.Entities;
using Music.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Musics> Musics { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new MusicConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

}
