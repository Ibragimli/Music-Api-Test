using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Music.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.Data.Configurations
{
    public class MusicConfiguration : IEntityTypeConfiguration<Musics>
    {
        public void Configure(EntityTypeBuilder<Musics> builder)
        {
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.AlbumId).IsRequired(true);
            builder.Property(x => x.Storage).IsRequired(true);
        }
    }
}
